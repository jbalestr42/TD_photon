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
        UpdateUI(tower);
    }

    public void UpdateUI(TowerBehaviour tower) {
        if (_panel.activeInHierarchy) {
            if (tower.entity.HasControl()) {
                _upgradeButton.gameObject.SetActive(true);
                // add onclick listener

            } else {
                _upgradeButton.gameObject.SetActive(false);
            }
            //_nameText.text = tower._data.name;
            _damageText.text = tower.state.Damage.ToString();
            _rangeText.text = tower.state.Range.ToString();
            _speedText.text = tower.state.AttackRate.ToString();
        }
    }

    public void HideUI() {
        _panel.SetActive(false);
    }
}
