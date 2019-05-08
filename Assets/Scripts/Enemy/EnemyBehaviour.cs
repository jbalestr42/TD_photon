using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Init order:
 * Init_Server
 * Start
 * */
public class EnemyBehaviour : Bolt.EntityBehaviour<IEnemyState>, ISelectable, ITargetable {

    UIEnemyHUD _enemyUI;
    EnemyData _data;
    SKU.ResourceAttribute _health = null;

    void Start() {
        _enemyUI = GetComponentInChildren<UIEnemyHUD>();
        state.AddCallback("Health", UpdateHealth);
    }

    public override void Attached() {
        state.SetTransforms(state.Transform, transform);
    }

    void UpdateHealth() {
        _enemyUI.SetHealthBar(state.Health / state.HealthMax);
        UIManager.Instance.UpdatePanel(PanelType.Enemy, this);
    }

    void OnDestroy() {
        UIManager.Instance.HidePanel(PanelType.Enemy);
    }

    #region Server Methods

    public void Init_Server(EnemyData data) {
        if (entity.IsOwner()) {
            _data = data;
            state.Health = data.health;
            _health = new SKU.ResourceAttribute(_data.health, _data.health, 1, 0.5f);
            _health.AddOnValueChangedListener(UpdateHealth_Server);

            AttributeManager attributeManager = gameObject.AddComponent<AttributeManager>();
            attributeManager.Add(AttributeType.Health, _health);

            var movement = gameObject.AddComponent<EnemyMovement>();
            movement.Init_Server(_data.speed);
        }
    }

    void UpdateHealth_Server(SKU.ResourceAttribute attribute) {
        state.Health = attribute.Value;
        state.HealthMax = attribute.Max.Value;
    }

    #endregion

    #region ISelectable

    public void Select() {
        UIManager.Instance.ShowPanel(PanelType.Enemy, this);
    }

    public void UnSelect() {
        UIManager.Instance.HidePanel(PanelType.Enemy);
    }

    #endregion

    #region ITargetable

    public void OnHit(GameObject emitter) {
        if (entity.IsOwner())
        {
            var attacker = emitter.GetComponent<IAttacker>();
            if (attacker != null) {
                attacker.ApplyOnHitEffect(gameObject);
            }

            if (_health.Value <= 0) {
                Die(emitter);
            }
        }
    }

    public void Die(GameObject killer) {
        EntityManager.Instance.DestroyEnemy(entity.gameObject);
        var player = PlayerObjectRegistry.GetPlayer(killer.GetComponent<TowerBehaviour>().entity.controller);
        player.behavior.state.Score += _data.score;
        player.behavior.state.Gold += _data.gold;
    }

    #endregion

    void OnCollisionEnter(Collision collision)
    {
        if (entity.IsOwner()) {
            if (collision.gameObject.tag == "SpawnEnd") {
                EntityManager.Instance.DestroyEnemy(entity.gameObject);
                GameManager.Instance.LooseLife(_data.lifeCost);
            }
        }
    }
}
