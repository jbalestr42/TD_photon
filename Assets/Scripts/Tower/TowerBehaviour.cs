using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : Bolt.EntityBehaviour<ITowerState>, ISelectable {

    [SerializeField]
    TowerData _data;
    float _timer = 0f;

    AttributeManager _attributeManager;
    SKU.Attribute _attackRate;
    SKU.Attribute _damage;

    void Start() {
        state.AddCallback("AttackRate", UpdateStat);
        state.AddCallback("Damage", UpdateStat);
    }

    #region Server Methods

    public void Init_Server() {
        if (entity.IsOwner()) {
            _attackRate = new SKU.Attribute();
            _damage = new SKU.Attribute();

            _attackRate.AddOnValueChangedListener(UpdateAttackRate_Server);
            _damage.AddOnValueChangedListener(UpdateDamage_Server);

            _attributeManager = gameObject.AddComponent<AttributeManager>();
            _attributeManager.Add(StatType.AttackRate, _attackRate);
            _attributeManager.Add(StatType.Damage, _damage);

            _attackRate.BaseValue = _data.attackRate;
            _damage.BaseValue = _data.damage;
        }
    }

    void UpdateAttackRate_Server(SKU.Attribute attribute) {
        state.AttackRate = attribute.Value;
    }

    void UpdateDamage_Server(SKU.Attribute attribute) {
        state.Damage = attribute.Value;
    }

    #endregion

    public override void Attached() {
        state.SetTransforms(state.Transform, transform);
    }

    void UpdateStat() {
        UIManager.Instance.GetUITower.UpdateUI(this);
    }

    // TODO replace by skill
    void Update() {
        if (entity.IsOwner()) {
            GameObject target = GetNearestEnemy();

            if (target) {
                _timer -= BoltNetwork.frameDeltaTime;
                if (_timer <= 0f) {
                    _timer += _attackRate.Value;
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
        EntityManager.Instance.SpawnBullet(_data.bulletId, entity.networkId, target.GetComponent<EnemyBehaviour>().entity.networkId, true);
    }

    public void Select() {
        UIManager.Instance.GetUITower.ShowUI(this);
    }

    public void UnSelect() {
        UIManager.Instance.GetUITower.HideUI();
    }
}
