using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Bolt.EntityBehaviour<IEnemyState> {

    EnemyData _data;
    Transform _start;
    Transform _end;
    CharacterController _cc;

    void Start() {
    }

    public override void Attached() {
        // TODO Spawn Manager
        _start = GameObject.FindWithTag("SpawnStart").transform;
        _end = GameObject.FindWithTag("SpawnEnd").transform;
        _cc = GetComponent<CharacterController>();
        transform.position = _start.position;

        state.SetTransforms(state.Transform, transform);
    }

    void Update() {
        if (entity.IsOwner()) {
            var direction = _end.position - _start.position;
            direction.y += -9.81f;
            _cc.Move(direction.normalized * _data.speed * BoltNetwork.frameDeltaTime);
        }
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
