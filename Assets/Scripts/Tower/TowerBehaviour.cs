using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : Bolt.EntityBehaviour<ITowerState>, ISelectable {

    public TowerData _data;

	void Start () {
		
	}

    public override void Attached() {
        state.SetTransforms(state.Transform, transform);
    }

    public void Select() {
        UIManager.Instance.GetUITower.ShowUI(this);
    }

    public void UnSelect() {
        UIManager.Instance.GetUITower.HideUI();
    }
}
