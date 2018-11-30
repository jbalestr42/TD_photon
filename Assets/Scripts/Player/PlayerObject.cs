using UnityEngine;

public class PlayerObject {
    public BoltEntity character;
    public BoltConnection connection;
    public PlayerBehavior behavior;

    public bool IsServer {
        get { return connection == null; }
    }

    public bool IsClient {
        get { return connection != null; }
    }

    public void Spawn() {
        if (!character) {
            character = BoltNetwork.Instantiate(BoltPrefabs.Player);

            if (IsServer) {
                character.TakeControl();
            } else {
                character.AssignControl(connection);
            }
        }

        // teleport entity to a random spawn position
        character.transform.position = RandomPosition();
        behavior = character.GetComponent<PlayerBehavior>();
    }

    Vector3 RandomPosition() {
        float x = Random.Range(-10f, +10f);
        float z = Random.Range(-10f, +10f);
        return new Vector3(x, 1f, z);
    }
}