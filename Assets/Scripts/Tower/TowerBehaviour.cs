using UnityEngine;

public interface IAttacker {
    void ApplyOnHitEffect(GameObject target);
    GameObject GetTarget();
}

public class TowerBehaviour : Bolt.EntityBehaviour<ITowerState>, ISelectable, IAttacker {

    [SerializeField]
    TowerData _data;

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

            // TODO init from data
           gameObject.AddComponent<ShootBullet>();
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

    #endregion

    #region IAttacker

    public void ApplyOnHitEffect(GameObject target) {
        var attributes = target.GetComponent<AttributeManager>();
        attributes.Get<SKU.ResourceAttribute>(StatType.Health).Remove(_damage.Value);

        // TODO: clean data for modifiers
        if (_data.modifiers != null) {
            for (int i = 0; i < _data.modifiers.Count; i++) {
                if (_data.modifiers[i] == ModifierType.Time) {
                    attributes.Get<SKU.Attribute>(StatType.Speed).AddRelativeModifier(Factory.CreateModifier(ModifierType.Time, 2f, -0.8f));
                }
            }
        }
    }

    public GameObject GetTarget() {
        return GetNearestEnemy();
    }

    #endregion

    #region ISelectable

    public void Select() {
        UIManager.Instance.GetUITower.ShowUI(this);
    }

    public void UnSelect() {
        UIManager.Instance.GetUITower.HideUI();
    }

    #endregion

    // TODO abstract strategy pour choisir le bon enemy (plus pret, plus de vie, plus proche de la fin, boss, etc...)
    // store current target to avoid useless computation
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

    public BulletType GetBulletType() {
        return _data.bulletId;
    }
}
