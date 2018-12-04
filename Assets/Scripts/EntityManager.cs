using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : Singleton<EntityManager> {
    /*
    TowerData _data;
    GameObject _tower;

    void Start() {
        _data = null;
        _tower = null;
    }

    public GameObject SpawnTower() {
        Debug.Log("Spawn" + _tower);
        DestroyTower();
        _tower = Instantiate(_data._gameObject);
        return _tower;
    }

    public void DestroyTower() {
        Debug.Log("Destroy " + _tower);
        if (_tower != null) {
            GameObject.Destroy(_tower);
            _tower = null;
        }
    }

    public TowerData SelectedTower {
        get { return _data; }
        set {
            _data = value;
        }
    }*/
}
