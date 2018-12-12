using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    Transform _start;
    Transform _end;
    CharacterController _cc;

    SKU.Attribute _speed = null;

    void Awake() {
        // TODO Spawn Manager
        _start = GameObject.FindWithTag("SpawnStart").transform;
        _end = GameObject.FindWithTag("SpawnEnd").transform;
        _cc = GetComponent<CharacterController>();
        transform.position = _start.position;
    }

    void Update() {
        _speed.Update();
        var direction = _end.position - _start.position;
        direction.y += -9.81f;
        _cc.Move(direction.normalized * _speed.Value * BoltNetwork.frameDeltaTime);
	}

    public void SetSpeed(float speed) {
        _speed = new SKU.Attribute(speed);
    }

    public SKU.Attribute Speed {
        get { return _speed; }
    }
}
