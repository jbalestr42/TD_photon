using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayer : MonoBehaviour {

    public UnityEngine.UI.Text _name;

    void Update() {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    public void SetPlayerName(string name) {
        _name.text = name;
    }
}
