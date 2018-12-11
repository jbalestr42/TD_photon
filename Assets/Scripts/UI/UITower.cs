using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITower : MonoBehaviour {

    public GameObject _panel;
    public Text _nameText;
    public Text _damageText;
    public Text _rangeText;
    public Text _speedText;
    public Button _upgradeButton;

    public void ShowUI(TowerBehaviour tower) {
        _panel.SetActive(true);
        if (tower.entity.HasControl()) {
            _upgradeButton.gameObject.SetActive(true);
            // add onclick listener

        } else {
            _upgradeButton.gameObject.SetActive(false);
        }
        _nameText.text = tower._data.name;
        _damageText.text = tower._data.damage.ToString();
    }

    public void HideUI() {
        _panel.SetActive(false);
    }
}
