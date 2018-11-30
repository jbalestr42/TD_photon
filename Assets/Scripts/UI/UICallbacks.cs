using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICallbacks : MonoBehaviour {

    bool _isReady = false;

    public void IsReady_OnClick(UnityEngine.UI.Button button) 
    {
        _isReady = !_isReady;
        var evnt = IsReadyEvent.Create();
        evnt.IsReady = _isReady;
        evnt.Send();

        if (_isReady) {
            button.GetComponentInChildren<UnityEngine.UI.Text>().text = "Ready!";
        } else {
            button.GetComponentInChildren<UnityEngine.UI.Text>().text = "Not Ready!";
        }
    }

    public void SetName(UnityEngine.UI.Text text) {
        var evnt = SetNameEvent.Create();
        evnt.Name = text.text;
        evnt.Send();
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void SetColor(UnityEngine.UI.Image image) {
        var evnt = SetColorEvent.Create();
        evnt.Color = image.color;
        evnt.Send();
    }
}
