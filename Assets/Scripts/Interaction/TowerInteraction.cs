﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInteraction : AInteraction {

    TowerData _data = null;
    GameObject _tower = null;

    public TowerInteraction(TowerData data) {
        Debug.Log("Spawn");
        _data = data;
        _tower = GameObject.Instantiate(_data._gameObject);
    }

    public override int GetLayer() {
        return Layers.Terrain;
    }

    public override void OnMouseClick(Vector3 position) {
        var evnt = SpawnEvent.Create();
        evnt.PrefabId = _tower.GetComponentInChildren<BoltEntity>().prefabId;
        evnt.Position = position;
        evnt.Send();
        InteractionManager.Instance.EndInteraction();
    }

    public override void OnMouseOver(Vector3 position) {
        if (_tower) {
            _tower.transform.position = position;
        }
    }

    public override void Cancel() {
        if (_tower) {
            GameObject.Destroy(_tower);
            _tower = null;
        }
    }

    public override void End() {
        if (_tower) {
            GameObject.Destroy(_tower);
            _tower = null;
        }
    }
}