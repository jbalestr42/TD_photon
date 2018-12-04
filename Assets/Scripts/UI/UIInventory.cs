using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour {

    public List<TowerData> _towerData;
    public GameObject _inventoryConainer;
    public GameObject _inventoryTower;

	void Start () {
		foreach (var data in _towerData) {
            var tower = Instantiate(_inventoryTower);
            tower.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { SelectTower(data); });
            tower.transform.SetParent(_inventoryConainer.transform);
        }
	}

    public void SelectTower(TowerData data) {
        InteractionManager.Instance.SetInteraction(new GridInteraction(data));
    }
}
