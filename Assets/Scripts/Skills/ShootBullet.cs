using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : ASkill {

    void Start() {
        var requirement = new List<IRequirement>();
        requirement.Add(new ValidTargetReq(gameObject));
        SKU.Attribute rate = GetComponent<AttributeManager>().Get<SKU.Attribute>(AttributeType.AttackRate);
        base.Init(rate, requirement);
    }

    public override void Cast(GameObject p_owner) {
        var tower = p_owner.GetComponent<TowerBehaviour>();
        var target = tower.GetTarget();
        EntityManager.Instance.SpawnBullet(tower.GetBulletType(), tower.entity.networkId, target.GetComponent<EnemyBehaviour>().entity.networkId, true);
    }
}