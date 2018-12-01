using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : Bolt.EntityBehaviour<IPlayerState> {

    PlayerUI _playerUI;
    Renderer _renderer;

    public override void Attached() {
        _renderer = GetComponentInChildren<Renderer>();
        _playerUI = GetComponentInChildren<PlayerUI>();

        state.AddCallback("Color", () => { _renderer.material.color = state.Color; });
        state.AddCallback("Name", () => { _playerUI.SetPlayerName(state.Name); });
        state.AddCallback("IsReady", () => { UIManager.Instance.SetReadyButton(state.IsReady); });
    }
}
