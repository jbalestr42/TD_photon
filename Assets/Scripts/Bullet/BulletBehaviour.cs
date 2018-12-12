using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    public float _speed;

    GameObject _owner;
    GameObject _target;

	void Update () {
		if (_target) {
            var direction = _target.transform.position - transform.position;
            transform.Translate(direction.normalized * _speed * BoltNetwork.frameDeltaTime);
        } else {
            Destroy(gameObject);
        }
	}

    public GameObject Target {
        get { return _target; }
        set { _target = value; }
    }

    public GameObject Owner {
        get { return _owner; }
        set { _owner = value; }
    }

    void OnTriggerEnter(Collider other) {
        var target = other.gameObject.GetComponent<ITargetable>();
        if (target != null) {
            target.ApplyEffect(_owner);
            EntityManager.Instance.DestroyBullet(gameObject);
        }
    }
}
