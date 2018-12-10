using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Client)]
public class PlayerCallbacks : Bolt.GlobalEventListener {

    public override void OnEvent(FireEvent evnt) {
        EntityManager.Instance.SpawnBullet((BulletType)evnt.BulletId, evnt.OwnerId, evnt.TargetId);
    }
}