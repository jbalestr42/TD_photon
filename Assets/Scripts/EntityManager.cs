using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager> {

    public GameObject _bullet;

    // TODO use a factory to create the good bullet and sent the id to clients
    public GameObject CreateBullet(Bolt.NetworkId ownerId, Bolt.NetworkId targetId) {
        var bullet = Instantiate(_bullet);
        var owner = BoltNetwork.FindEntity(ownerId);
        var target = BoltNetwork.FindEntity(targetId);
        bullet.GetComponent<BulletBehaviour>().Owner = owner;
        bullet.GetComponent<BulletBehaviour>().Target = target;
        bullet.transform.position = owner.transform.position;
        return bullet;
    }
}
