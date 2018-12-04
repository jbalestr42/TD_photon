using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInteraction : AInteraction {

    TowerData _data = null;
    GameObject _tower = null;

    public GridInteraction(TowerData data) {
        Debug.Log("Spawn");
        _data = data;
        _tower = GameObject.Instantiate(_data._gameObject);
    }

    public override int GetLayer() {
        return 8; // TODO Layer class static
    }

    public override void OnMouseClick(Vector3 position) {
        Debug.Log("Click");
        InteractionManager.Instance.EndInteraction();
    }

    public override void OnMouseEnter(Vector3 position) {
        Debug.Log("enter");
    }

    public override void OnMouseExit() {
        Debug.Log("exit");
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
        _tower = null;
    }
}