using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : Bolt.EntityBehaviour<ITowerState>, ISelectable {

    public TowerData _data;

    void Start() {
        if (entity.IsOwner()) {
            // TODO: Init all attribute from data
            AttributeManager attributeManager = gameObject.AddComponent<AttributeManager>();
            attributeManager.AddAttribute(AttributeType.Damage, new BasicAttribute(80, 0, 1000));
            attributeManager.AddAttribute(AttributeType.AttackRate, new BasicAttribute(_data.attackRate, 0, 10));
            attributeManager.AddAttribute(AttributeType.Range, new BasicAttribute(_data.damage, 0, 1000));

            ShotProjectile shotProjectile = gameObject.AddComponent<ShotProjectile>();
            shotProjectile.BulletId = _data.bulletId;
        }
    }

    public override void Attached() {
        state.SetTransforms(state.Transform, transform);
    }

    // TODO abstract strategy pour choisir le bon enemy (plus pret, plus de vie, plus proche de la fin, boss, etc...)
    public GameObject GetNearestEnemy() {
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
        EntityManager.Instance.SpawnBullet(_data.bulletId, entity.networkId, target.GetComponent<EnemyController>().entity.networkId, true);
    }

    public void Select() {
        UIManager.Instance.GetUITower.ShowUI(this);
    }

    public void UnSelect() {
        UIManager.Instance.GetUITower.HideUI();
    }
}
