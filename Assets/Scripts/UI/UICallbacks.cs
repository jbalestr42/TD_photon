using UnityEngine;

// Contains the event when interacting with the UI
public class UICallbacks : MonoBehaviour {

    public void IsReady_OnClick(UnityEngine.UI.Button button) {
        var evnt = IsReadyEvent.Create();
        evnt.IsReady = true;
        evnt.Send();
    }

    public void SetName(UnityEngine.UI.Text text) {
        var evnt = SetNameEvent.Create();
        evnt.Name = text.text;
        evnt.Send();
    }

    public void SetColor(UnityEngine.UI.Image image) {
        var evnt = SetColorEvent.Create();
        evnt.Color = image.color;
        evnt.Send();
    }
}
