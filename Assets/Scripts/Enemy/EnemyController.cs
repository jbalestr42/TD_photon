using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Bolt.EntityBehaviour<IEnemyState> {

    Transform _start;
    Transform _end;
    public float _speed;
    CharacterController _cc;

    public override void Attached() {
        // TODO Spawn Manager
        _start = GameObject.FindWithTag("SpawnStart").transform;
        _end = GameObject.FindWithTag("SpawnEnd").transform;
        _cc = GetComponent<CharacterController>();
        transform.position = _start.position;

        state.SetTransforms(state.Transform, transform);
        if (entity.IsOwner()) {
            state.Health = 15;
        }
    }

    void Update() {
        var direction = _end.position - _start.position;
        direction.y += -9.81f;
        _cc.Move(direction.normalized * _speed * BoltNetwork.frameDeltaTime);
	}

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag == "SpawnEnd") {
            if (entity != null) {
                WaveManager.Instance.KillEnemy(entity.gameObject);
            }
        }
    }

    public void TakeDamage(GameObject owner) {
        if (entity.IsOwner()) {
            state.Health -= owner.GetComponent<TowerBehaviour>()._data._damage;
            if (state.Health <= 0) {
                WaveManager.Instance.KillEnemy(entity.gameObject);
                var player = PlayerObjectRegistry.GetPlayer(owner.GetComponent<TowerBehaviour>().entity.controller);
                player.behavior.state.Score += 10;
                player.behavior.state.Gold += 1;
            }
        }
    }
}
