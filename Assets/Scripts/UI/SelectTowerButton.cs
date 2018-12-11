using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTowerButton : MonoBehaviour {

    public TowerData Data { get; set; }

    public void SelectTower() {
        if (PlayerObjectRegistry.GetPlayer().state.Gold >= Data.cost) {
            InteractionManager.Instance.SetInteraction(new GridInteraction(Data));
        }
    }
}
