using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {

}

public class GameManager : Singleton<GameManager> {

    GameBehaviour _game;

    public GameBehaviour Game {
        get { return _game; }
        set { _game = value; }
    }

    public void LooseLife(int amount) {
        _game.state.Life -= amount;
    }

    public bool CanStartGame() {
        return AreAllPlayerReady();
    }

    public void StartGame() {
        WaveManager.Instance.StartWave();
    }

    public void SetAllPlayerReadyState(bool ready) {
        foreach (var player in PlayerObjectRegistry.GetPlayers) {
            if (player.behavior != null && player.behavior.state != null) {
                player.behavior.state.IsReady = ready;
            }
        }
    }

    public bool AreAllPlayerReady() {
        bool ready = true;
        foreach (var player in PlayerObjectRegistry.GetPlayers) {
            ready = ready && player.behavior.state.IsReady;
        }
        return ready;
    }
}
