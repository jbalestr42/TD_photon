using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallbacks : Bolt.GlobalEventListener {

    List<string> logMessages = new List<string>();

    void Awake() {
        PlayerObjectRegistry.CreateServerPlayer();
    }

    public override void Connected(BoltConnection connection) {
        PlayerObjectRegistry.CreateClientPlayer(connection);

        GameManager.Instance.SetAllPlayerReadyState(false);

        var log = LogEvent.Create();
        log.Message = string.Format("{0} connected", connection.RemoteEndPoint);
        log.Send();
    }

    public override void Disconnected(BoltConnection connection) {
        PlayerObjectRegistry.DestroyClientPlayer(connection);

        GameManager.Instance.SetAllPlayerReadyState(false);

        var log = LogEvent.Create();
        log.Message = string.Format("{0} disconnected", connection.RemoteEndPoint);
        log.Send();
    }

    public override void SceneLoadLocalDone(string map) {
        PlayerObjectRegistry.ServerPlayer.Spawn();
        var game = BoltNetwork.Instantiate(BoltPrefabs.Game);
        GameManager.Instance.Game = game.GetComponent<GameBehaviour>();
    }

    public override void SceneLoadRemoteDone(BoltConnection connection) {
        PlayerObjectRegistry.GetPlayer(connection).Spawn();
    }

    public override void OnEvent(LogEvent evnt) {
        logMessages.Insert(0, evnt.Message);
    }

    public override void OnEvent(SetColorEvent evnt) {
        PlayerObjectRegistry.GetPlayer(evnt.RaisedBy).behavior.state.Color = evnt.Color;
    }

    public override void OnEvent(SetNameEvent evnt) {
        PlayerObjectRegistry.GetPlayer(evnt.RaisedBy).behavior.state.Name = evnt.Name;
    }

    public override void OnEvent(SpawnEvent evnt) {
        var player = PlayerObjectRegistry.GetPlayer(evnt.RaisedBy);
        var tower = BoltNetwork.Instantiate(evnt.PrefabId);

        if (player.behavior.state.Gold >= tower.GetComponent<TowerBehaviour>()._data._cost)
        {
            player.behavior.state.Gold -= tower.GetComponent<TowerBehaviour>()._data._cost;
            tower.transform.position = evnt.Position;

            if (player.IsServer) {
                tower.TakeControl();
            } else {
                tower.AssignControl(evnt.RaisedBy);
            }
        } else {
            BoltNetwork.Destroy(tower);
        }
    }

    public override void OnEvent(IsReadyEvent evnt) {
        PlayerObjectRegistry.GetPlayer(evnt.RaisedBy).behavior.state.IsReady = !PlayerObjectRegistry.GetPlayer(evnt.RaisedBy).behavior.state.IsReady;

        if (GameManager.Instance.CanStartGame()) {
            GameManager.Instance.StartGame();
        }
    }

    void OnGUI() {
        // only display max the 5 latest log messages
        int maxMessages = Mathf.Min(5, logMessages.Count);

        GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), GUI.skin.box);

        for (int i = 0; i < maxMessages; ++i) {
            GUILayout.Label(logMessages[i]);
        }

        GUILayout.EndArea();
    }
}