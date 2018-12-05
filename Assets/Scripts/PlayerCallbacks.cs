using UnityEngine;

[BoltGlobalBehaviour(BoltNetworkModes.Client)]
public class PlayerCallbacks : Bolt.GlobalEventListener {

    public override void OnEvent(FireEvent evnt) {
        EntityManager.Instance.CreateBullet(evnt.OwnerId, evnt.TargetId);
    }
}