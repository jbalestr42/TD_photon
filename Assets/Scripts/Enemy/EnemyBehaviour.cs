using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Init order:
 * Init_Server
 * Start
 * */
public class EnemyBehaviour : Bolt.EntityBehaviour<IEnemyState>, ITargetable {

    UIEnemy _enemyUI;
    EnemyData _data;
    SKU.ResourceAttribute _health = null;
    AttributeManager _attributeManager;

    void Start() {
        _enemyUI = GetComponentInChildren<UIEnemy>();
        state.AddCallback("Health", UpdateHealth);
    }

    #region Server Methods

    public void Init_Server(EnemyData data) {
        if (entity.IsOwner()) {
            _data = data;
            state.Health = data.health;
            _health = new SKU.ResourceAttribute(_data.health, _data.health, 1, 0.5f);
            _health.AddOnValueChangedListener(UpdateHealth_Server);

            _attributeManager = gameObject.AddComponent<AttributeManager>();
            _attributeManager.Add(StatType.Health, _health);

            var movement = gameObject.AddComponent<EnemyMovement>();
            movement.Init_Server(_data.speed);
        }
    }

    void UpdateHealth_Server(SKU.ResourceAttribute attribute) {
        state.Health = attribute.Value;
        state.HealthMax = attribute.Max.Value;
    }

    #endregion

    void UpdateHealth() {
        _enemyUI.SetHealthBar(state.Health / state.HealthMax);
    }

    public override void Attached() {
        state.SetTransforms(state.Transform, transform);
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (entity.IsOwner()) {
            if (hit.gameObject.tag == "SpawnEnd") {
                EntityManager.Instance.DestroyEnemy(entity.gameObject);
                GameManager.Instance.LooseLife(_data.lifeCost);
            }
        }
    }

    public void ApplyEffect(GameObject emitter) {
        if (entity.IsOwner()) {
            _health.Remove(emitter.GetComponent<TowerBehaviour>()._data.damage);
            if (_health.Value <= 0) {
                Die(emitter);
            }

            // TODO Get from emitter
            var movement = GetComponent<EnemyMovement>();
            if (movement != null) {
                _attributeManager.Get<SKU.Attribute>(StatType.Speed).AddRelativeModifier(new TimeModifier(2f, -0.8f));
            }
        }
    }

    public void Die(GameObject killer) {
        EntityManager.Instance.DestroyEnemy(entity.gameObject);
        var player = PlayerObjectRegistry.GetPlayer(killer.GetComponent<TowerBehaviour>().entity.controller);
        player.behavior.state.Score += _data.score;
        player.behavior.state.Gold += _data.gold;
    }
}
