using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelType {
    None,
    Tower,
    Enemy,
}

public class UIManager : Singleton<UIManager> {

    public UnityEngine.UI.Button _isReadyButton;
    public UnityEngine.UI.InputField _inputFieldName;
    public List<UnityEngine.UI.Button> _chooseColor;
    public UnityEngine.UI.Text _goldText;
    public UnityEngine.UI.Text _scoreText;
    public UnityEngine.UI.Text _lifeText;

    public PanelTower _uiTower;
    public UIInventory _uiInventory;

    PanelType _selectedPanel = PanelType.None;
    MonoBehaviour _selectedMono = null;

    [SerializeField]
    private PanelTypeAPanelDictionary _panels = PanelTypeAPanelDictionary.New<PanelTypeAPanelDictionary>();
    private Dictionary<PanelType, APanel> Panels {
        get { return _panels.dictionary; }
    }

    void Awake() {
        _isReadyButton.onClick.AddListener(IsReady_OnClick);
        _inputFieldName.onEndEdit.AddListener(SetName_OnEndEdit);
        for (int i = 0; i < _chooseColor.Count; i++) {
            int index = i;
            _chooseColor[i].onClick.AddListener(() => SetColor_OnClick(_chooseColor[index].GetComponent<UnityEngine.UI.Image>().color));
        }
    }

    #region Accessor to other UI systems

    public UIInventory GetUIInventory {
        get { return _uiInventory; }
    }

    public void ShowPanel(PanelType type, MonoBehaviour target) {
        if (_selectedPanel != type && type != PanelType.None) {
            if (_selectedPanel != PanelType.None) {
                Panels[_selectedPanel].HideUI();
            }
            _selectedPanel = type;
            _selectedMono = target;
            Panels[type].ShowUI(target);
        }
    }

    public void UpdatePanel(PanelType type, MonoBehaviour target) {
        if (type != PanelType.None && Panels[type].IsActive() && _selectedMono == target) {
            Panels[type].UpdateUI(target);
        }
    }

    public void HidePanel(PanelType type) {
        if (type != PanelType.None) {
            Panels[type].HideUI();
            _selectedPanel = PanelType.None;
        }
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
