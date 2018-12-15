﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacker {
    void ApplyOnHitEffect(GameObject target);
}

public class TowerBehaviour : Bolt.EntityBehaviour<ITowerState>, ISelectable, IAttacker {

    [SerializeField]
    TowerData _data;
    float _timer = 0f;

    AttributeManager _attributeManager;
    SKU.Attribute _attackRate;
    SKU.Attribute _damage;
    SKU.Attribute _range;

    void Start() {
        state.AddCallback("AttackRate", UpdateStat);
        state.AddCallback("Damage", UpdateStat);
        state.AddCallback("Range", UpdateStat);
    }

    public override void Attached() {
        state.SetTransforms(state.Transform, transform);
    }

    void UpdateStat() {
        UIManager.Instance.GetUITower.UpdateUI(this);
    }

    #region Server Methods

    public void Init_Server() {
        if (entity.IsOwner()) {
            _attackRate = new SKU.Attribute();
            _damage = new SKU.Attribute();
            _range = new SKU.Attribute();

            _attackRate.AddOnValueChangedListener(UpdateAttackRate_Server);
            _damage.AddOnValueChangedListener(UpdateDamage_Server);
            _range.AddOnValueChangedListener(UpdateRange_Server);

            _attributeManager = gameObject.AddComponent<AttributeManager>();
            _attributeManager.Add(StatType.AttackRate, _attackRate);
            _attributeManager.Add(StatType.Damage, _damage);
            _attributeManager.Add(StatType.Range, _range);

            _attackRate.BaseValue = _data.attackRate;
            _damage.BaseValue = _data.damage;
            _range.BaseValue = _data.range;
        }
    }

    void UpdateAttackRate_Server(SKU.Attribute attribute) {
        state.AttackRate = attribute.Value;
    }

    void UpdateDamage_Server(SKU.Attribute attribute) {
        state.Damage = attribute.Value;
    }

    void UpdateRange_Server(SKU.Attribute attribute) {
        state.Range = attribute.Value;
    }

    public void ApplyOnHitEffect(GameObject target) {
        var attributes = target.GetComponent<AttributeManager>();
        attributes.Get<SKU.ResourceAttribute>(StatType.Health).Remove(_damage.Value);
        attributes.Get<SKU.Attribute>(StatType.Speed).AddRelativeModifier(Factory.CreateModifier(ModifierType.Time, 2f, -0.8f));
    }

    #endregion

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
            if (dist < _range.Value && dist < min) {
                min = dist;
                nearest = enemies[i];
            }
        }
        return nearest;
    }

    void Shoot(GameObject target) {
        EntityManager.Instance.SpawnBullet(_data.bulletId, entity.networkId, target.GetComponent<EnemyBehaviour>().entity.networkId, true);
    }

    #region ISelectable

    public void Select() {
        UIManager.Instance.GetUITower.ShowUI(this);
    }

    public void UnSelect() {
        UIManager.Instance.GetUITower.HideUI();
    }

    #endregion
}
