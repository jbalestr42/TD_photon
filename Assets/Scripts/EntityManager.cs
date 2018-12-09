using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager> {

    public List<GameObject> _enemies;
    public GameObject _bullet;

    void Awake() {
        _enemies = new List<GameObject>();
    }

    void Start() {
        // Create generic Factory
        // Add Bullet factory 
        // BulletData registered with a bullet enum and add it to the Fireevent to tell which bullet is fired


        // This class will manage the lifetime of tower, bullet and enemies.
        // When we need to spawn, disable, kill one of those, we must use this class
    }

    public GameObject SpawnEnemy(EnemyData data) {
        var enemy = BoltNetwork.Instantiate(data.enemyId);
        enemy.GetComponent<EnemyController>().SetData(data);

        _enemies.Add(enemy);
        return enemy;
    }

    public void KillEnemy(GameObject enemy) {
        _enemies.Remove(enemy);
        BoltNetwork.Destroy(enemy);
    }

    public List<GameObject> GetEnemies() {
        return _enemies;
    }

    public bool AreAllEnemyDead() {
        return _enemies.Count == 0;
    }

    // TODO use a factory to create the good bullet and sent the id to clients
    public GameObject CreateBullet(Bolt.NetworkId ownerId, Bolt.NetworkId targetId) {
        var bullet = Instantiate(_bullet);
        var owner = BoltNetwork.FindEntity(ownerId);
        var target = BoltNetwork.FindEntity(targetId);
        if (owner != null && target != null) {
            bullet.GetComponent<BulletBehaviour>().Owner = owner;
            bullet.GetComponent<BulletBehaviour>().Target = target;
            bullet.transform.position = owner.transform.position;
        }
        return bullet;
    }
}
