using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    public UnityEngine.UI.Button _isReadyButton;
    public UnityEngine.UI.InputField _inputFieldName;
    public List<UnityEngine.UI.Button> _chooseColor;
    public UnityEngine.UI.Text _goldText;
    public UnityEngine.UI.Text _scoreText;
    public UnityEngine.UI.Text _lifeText;

    public UITower _uiTower;
    public UIInventory _uiInventory;

    void Awake() {
        _isReadyButton.onClick.AddListener(IsReady_OnClick);
        _inputFieldName.onEndEdit.AddListener(SetName_OnEndEdit);
        for (int i = 0; i < _chooseColor.Count; i++) {
            int index = i;
            _chooseColor[i].onClick.AddListener(() => SetColor_OnClick(_chooseColor[index].GetComponent<UnityEngine.UI.Image>().color));
        }
    }

    #region Accessor to other UI systems

    public UITower GetUITower {
        get { return _uiTower; }
    }

    public UIInventory GetUIInventory {
        get { return _uiInventory; }
    }

    #endregion

    #region Public methods exposed to update the UI

    public void SetGold(int gold) {
        _goldText.text = "Gold: " + gold.ToString();
    }

    public void SetScore(int score) {
        _scoreText.text = "Score: " + score.ToString();
    }

    public void SetLife(int life) {
        _lifeText.text = "Life: " + life.ToString();
    }

    public void OnReadyStateChanged(bool ready) {
        _isReadyButton.interactable = !ready;
    }

    #endregion

    #region Private UI Listeners

    void IsReady_OnClick() {
        var evnt = IsReadyEvent.Create();
        evnt.Send();
    }

    void SetName_OnEndEdit(string text) {
        var evnt = SetNameEvent.Create();
        evnt.Name = text;
        evnt.Send();
    }

    void SetColor_OnClick(Color color) {
        var evnt = SetColorEvent.Create();
        evnt.Color = color;
        evnt.Send();
    }

    #endregion
}
