using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemy : MonoBehaviour {

    public UnityEngine.UI.Text _name;
    public UnityEngine.UI.Image _health;
    public float amount = 0.5f;

    void Update() {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    public void SetPlayerName(string name) {
        _name.text = name;
    }

    public void SetHealthBar(float ratio) {
        _health.fillAmount = ratio;
    }
}
