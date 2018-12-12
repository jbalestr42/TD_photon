using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Bolt.EntityBehaviour<IEnemyState>, ITargetable {

    EnemyData _data;

    void Start () {
		if (entity.IsOwner()) {
            var movement = gameObject.AddComponent<EnemyMovement>();
            movement.SetSpeed(_data.speed);
        }
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
            state.Health -= emitter.GetComponent<TowerBehaviour>()._data.damage;
            if (state.Health <= 0) {
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

    public void SetData(EnemyData data) {
        _data = data;
        if (entity.IsOwner()) {
            state.Health = data.health;
        }
    }
}
