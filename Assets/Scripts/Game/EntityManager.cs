using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager> {

    List<GameObject> _enemies;
    List<GameObject> _bullets;
    List<GameObject> _towers;

    void Awake() {
        _enemies = new List<GameObject>();
        _bullets = new List<GameObject>();
        _towers = new List<GameObject>();
    }

    void Start() {
        // This class will manage the lifetime of tower, bullet and enemies.
        // When we need to spawn, disable or kill one of those, we must use this class
    }

    #region Enemies

    public GameObject SpawnEnemy(EnemyData data) {
        var enemy = BoltNetwork.Instantiate(data.enemyId);
        enemy.GetComponent<EnemyBehaviour>().Init_Server(data);

        _enemies.Add(enemy);
        return enemy;
    }

    public void DestroyEnemy(GameObject enemy) {
        _enemies.Remove(enemy);
        BoltNetwork.Destroy(enemy);
    }

    public List<GameObject> GetEnemies() {
        return _enemies;
    }

    public bool AreAllEnemyDead() {
        return _enemies.Count == 0;
    }

    #endregion

    #region Bullets

    public GameObject SpawnBullet(BulletType bulletType, Bolt.NetworkId ownerId, Bolt.NetworkId targetId, bool isServer = false) {
        GameObject bullet = null;
        var owner = BoltNetwork.FindEntity(ownerId);
        var target = BoltNetwork.FindEntity(targetId);
        if (owner != null && target != null) {
            bullet = Factory.Instance.CreateBullet(bulletType);
            bullet.GetComponent<BulletBehaviour>().Owner = owner;
            bullet.GetComponent<BulletBehaviour>().Target = target;
            bullet.transform.position = owner.transform.position;

            if (isServer) {
                var evnt = FireEvent.Create();
                evnt.BulletId = (int)bulletType;
                evnt.OwnerId = ownerId;
                evnt.TargetId = targetId;
                evnt.Send();
            }
            _bullets.Add(bullet);
        }

        return bullet;
    }

    public void DestroyBullet(GameObject bullet) {
        _bullets.Remove(bullet);
        GameObject.Destroy(bullet);
    }

    #endregion

    #region Towers

    public GameObject SpawnTower(TowerType towerType, PlayerObject player, Vector3 position) {
        TowerData data = DataManager.Instance.GetTowerData(towerType);

        Vector2Int coord = GridManager.Instance.GetCoordFromPosition(position);
        if (GridManager.Instance.IsEmpty(coord.x, coord.y) && player.behavior.state.Gold >= data.cost)
        {
            var tower = BoltNetwork.Instantiate(BoltPrefabs.BaseTower);
            tower.transform.position = position;

            if (GridManager.Instance.CanPlaceObject(tower))
            {
                GridManager.Instance.SetEmpty(coord.x, coord.y, false);
                player.behavior.state.Gold -= data.cost;
                SetControl(player, tower);
                _towers.Add(tower);
                tower.GetComponent<TowerBehaviour>().Init_Server(data);
                return tower;
            }
            else
            {
                BoltNetwork.Destroy(tower);
            }
        }
        return null;
    }

    public void DestroyTower(GameObject tower) {
        _towers.Remove(tower);
        BoltNetwork.Destroy(tower);
    }

    #endregion

    #region Private Utils

    void SetControl(PlayerObject player, BoltEntity entity) {
        if (player.IsServer) {
            entity.TakeControl();
        } else {
            entity.AssignControl(player.connection);
        }
    }

    #endregion
}
