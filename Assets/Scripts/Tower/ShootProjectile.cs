using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotProjectile : ASkill {

    public BulletType BulletId { get; set; }

    void Start() {
        var requirement = new List<IRequirement>();
        requirement.Add(new ValidTargetReq(gameObject));
        Attribute<float> rate = GetComponent<AttributeManager>().GetAttribute<float>(AttributeType.AttackRate);
        base.Init(0.1f, rate.Value, requirement, null); 
        // TODO the firerate value will not be updated if it's changing in the attribute manager
        // We must give the AttributeType or a reference to the Attribute<float>
    }

    public override void Cast(GameObject p_owner) {
        var ownerId = p_owner.GetComponent<TowerBehaviour>().entity.networkId;
        var target = p_owner.GetComponent<TowerBehaviour>().GetNearestEnemy();
        var targetId = target.GetComponent<EnemyController>().entity.networkId;
        EntityManager.Instance.SpawnBullet(BulletId, ownerId, targetId, true);
    }
}