using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : Bolt.EntityBehaviour<IPlayerState> {

    public GameObject _nameGo;
    Renderer _renderer;

    public override void Attached() {
        _renderer = GetComponentInChildren<Renderer>();

        state.AddCallback("Color", ColorChanged);
        state.AddCallback("Name", NameChanged);
    }

    void ColorChanged() {
        _renderer.material.color = state.Color;
    }

    void NameChanged() {
        _nameGo.GetComponent<UnityEngine.UI.Text>().text = state.Name;
    }
}
