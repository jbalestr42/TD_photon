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

    void Start() {
        _enemyUI = GetComponentInChildren<UIEnemy>();
        state.AddCallback("Health", UpdateHealth);
    }

    public void Init_Server(EnemyData data) {
        if (entity.IsOwner()) {
            _data = data;
            state.Health = data.health;
            _health = new SKU.ResourceAttribute(_data.health, _data.health, 5, 0.5f);
            _health.AddOnValueChangedListener(UpdateHealth_Server);

            var movement = gameObject.AddComponent<EnemyMovement>();
            movement.Init(_data.speed);
        }
    }

    void Update() {
        if (entity.IsOwner() && _health != null)
            _health.Update();
    }

    void UpdateHealth_Server(SKU.ResourceAttribute attribute) {
        state.Health = attribute.Current;
        state.HealthMax = attribute.Max.Value;
    }

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
            if (_health.Current <= 0) {
                Die(emitter);
            }

            // TODO Get from emitter
            var movement = GetComponent<EnemyMovement>();
            if (movement != null) {
                movement.Speed.AddRelativeModifier(new SKU.TimeModifier(2f, -0.8f));
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
