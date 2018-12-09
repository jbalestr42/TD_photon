using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public enum BulletType {
    Normal,
    Speed,
}

public class Factory : Singleton<Factory> {

    public List<GameObject> _bullets;

    void Start() {

    }

    public GameObject InstantiateBullet(BulletType id) {
        Assert.IsNotNull(_bullets);
        Assert.IsTrue((int)id >= 0 && (int)id < _bullets.Count);

        var bullet = Instantiate(_bullets[(int)id]);
        return bullet;
    }
}
