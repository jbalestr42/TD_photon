using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : Bolt.EntityBehaviour<ITowerState>, ISelectable {

    public TowerData _data;
    float _timer = 0f;
    float _fireRate = 1f;

    void Start () {

    }

    public override void Attached() {
        state.SetTransforms(state.Transform, transform);
    }

    void Update() {
        if (entity.IsOwner()) {
            GameObject target = GetNearestEnemy();

            if (target) {
                _timer -= BoltNetwork.frameDeltaTime;
                if (_timer <= 0f) {
                    _timer += _fireRate;
                    Shoot(target);
                }
            } else {
                _timer = 0f;
            }
        }
    }

    // TODO abstract strategy pour choisir le bon enemy (plus pret, plus de vie, plus proche de la fin, boss, etc...)
    GameObject GetNearestEnemy() {
        var enemies = EntityManager.Instance.GetEnemies();

        float min = Mathf.Infinity;
        GameObject nearest = null;
        for (int i = 0; i < enemies.Count; i++) {
            float dist = Vector3.Distance(enemies[i].transform.position, gameObject.transform.position);
            if (dist < min) {
                min = dist;
                nearest = enemies[i];
            }
        }
        return nearest;
    }

    void Shoot(GameObject target) {
        EntityManager.Instance.SpawnBullet(_data._bulletId, entity.networkId, target.GetComponent<EnemyController>().entity.networkId, true);
    }

    public void Select() {
        UIManager.Instance.GetUITower.ShowUI(this);
    }

    public void UnSelect() {
        UIManager.Instance.GetUITower.HideUI();
    }
}
