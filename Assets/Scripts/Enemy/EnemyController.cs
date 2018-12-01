using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    Transform _start;
    Transform _end;
    public float _speed;
    CharacterController _cc;

    void Start() {
        // TODO Spawn Manager
        _start = GameObject.FindWithTag("SpawnStart").transform;
        _end = GameObject.FindWithTag("SpawnEnd").transform;
        _cc = GetComponent<CharacterController>();
        transform.position = _start.position;
    }

	void Update () {
        var direction = _end.position - _start.position;
        direction.y += -9.81f;
        _cc.Move(direction.normalized * _speed * BoltNetwork.frameDeltaTime);
//        transform.Translate(direction.normalized * _speed * BoltNetwork.frameDeltaTime);
	}

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag == "SpawnEnd") {
            BoltEntity.Destroy(gameObject);
        }
    }
}
