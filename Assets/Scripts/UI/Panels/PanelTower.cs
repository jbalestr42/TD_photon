using UnityEngine;
using UnityEngine.UI;

public class PanelTower : APanel {

    public Text _nameText;
    public Text _damageText;
    public Text _rangeText;
    public Text _speedText;
    public Button _upgradeButton;

    public override void UpdateUI(MonoBehaviour mono) {
        var tower = (TowerBehaviour)mono;
        if (tower.entity.HasControl()) {
            _upgradeButton.gameObject.SetActive(true);
            // add onclick listener

        } else {
            _upgradeButton.gameObject.SetActive(false);
        }
        //_nameText.text = tower._data.name;
        _damageText.text = "Damage: " + tower.state.Damage.ToString();
        _rangeText.text = "Range: " + tower.state.Range.ToString();
        _speedText.text = "Attack Rate: " +tower.state.AttackRate.ToString();
    }
}
