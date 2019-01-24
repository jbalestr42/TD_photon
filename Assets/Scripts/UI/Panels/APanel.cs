using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APanel : MonoBehaviour {

    [SerializeField]
    private GameObject _panel;

    public void ShowUI(MonoBehaviour mono) {
        if (!IsActive()) {
            _panel.SetActive(true);
        }
        UpdateUI(mono);
    }

    public abstract void UpdateUI(MonoBehaviour mono);

    public void HideUI() {
        _panel.SetActive(false);
    }

    public bool IsActive() {
        return _panel.activeInHierarchy;
    }
}