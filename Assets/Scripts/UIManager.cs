using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    public UnityEngine.UI.Button _isReadyButton;
    public UnityEngine.UI.InputField _inputFieldName;
    public List<UnityEngine.UI.Button> _chooseColor;

    void Awake() {
        _isReadyButton.onClick.AddListener(IsReady_OnClick);
        _inputFieldName.onEndEdit.AddListener(SetName_OnEndEdit);
        for (int i = 0; i < _chooseColor.Count; i++) {
            int index = i;
            _chooseColor[i].onClick.AddListener(() => SetColor_OnClick(_chooseColor[index].GetComponent<UnityEngine.UI.Image>().color));
        }
    }

    #region Public methods exposed to update the UI

    public void SetReadyButton(bool isReady) {
        _isReadyButton.GetComponentInChildren<UnityEngine.UI.Text>().text = isReady ? "Ready!" : "Not Ready!";
    }

    #endregion

    #region Private UI Listeners

    void IsReady_OnClick() {
        var evnt = IsReadyEvent.Create();
        evnt.IsReady = true;
        evnt.Send();
    }

    void SetName_OnEndEdit(string text) {
        var evnt = SetNameEvent.Create();
        evnt.Name = text;
        evnt.Send();
    }

    void okok(int i) {
        Debug.Log(i);
    }

    void SetColor_OnClick(Color color) {
        var evnt = SetColorEvent.Create();
        evnt.Color = color;
        evnt.Send();
    }

    #endregion
}
