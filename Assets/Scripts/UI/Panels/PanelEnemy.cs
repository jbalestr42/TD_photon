using UnityEngine;
using UnityEngine.UI;

public class PanelEnemy : APanel {

    public Text _healthText;

    public override void UpdateUI(MonoBehaviour mono) {
        var enemy = (EnemyBehaviour)mono;
        _healthText.text = "Health: " + enemy.state.Health.ToString() + " / " + enemy.state.HealthMax.ToString();
    }
}
