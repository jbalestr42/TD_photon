using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Bolt.EntityBehaviour<IEnemyState> {

    EnemyData _data;

    void Start () {
		if (entity.IsOwner()) {
            var movement = gameObject.AddComponent<EnemyMovement>();
            movement._speed = _data.speed;
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

    public void TakeDamage(GameObject owner) {
        if (entity.IsOwner()) {
            state.Health -= owner.GetComponent<TowerBehaviour>()._data.damage;
            if (state.Health <= 0) {
                EntityManager.Instance.DestroyEnemy(entity.gameObject);
                var player = PlayerObjectRegistry.GetPlayer(owner.GetComponent<TowerBehaviour>().entity.controller);
                player.behavior.state.Score += _data.score;
                player.behavior.state.Gold += _data.gold;
            }
        }
    }

    public void SetData(EnemyData data) {
        _data = data;
        if (entity.IsOwner()) {
            state.Health = data.health;
        }
    }
}
