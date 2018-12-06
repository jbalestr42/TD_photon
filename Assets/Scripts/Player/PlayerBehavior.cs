using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Call order
 * Attached
 * ControlGained
 * Start
 * */
public class PlayerBehavior : Bolt.EntityBehaviour<IPlayerState> {

    PlayerUI _playerUI;
    Renderer _renderer;

    private void Start() {
        if (entity.IsOwner()) {
            state.Gold = 100;
            state.Score = 0;
        }
    }

    public override void Attached() {
        _renderer = GetComponentInChildren<Renderer>();
        _playerUI = GetComponentInChildren<PlayerUI>();
    }

    public override void ControlGained() {
        state.AddCallback("Gold", () => { UIManager.Instance.SetGold(state.Gold); });
        state.AddCallback("Score", () => { UIManager.Instance.SetScore(state.Score); });
        state.AddCallback("Color", () => { _renderer.material.color = state.Color; });
        state.AddCallback("Name", () => { _playerUI.SetPlayerName(state.Name); });
        state.AddCallback("IsReady", () => { UIManager.Instance.SetReadyButton(state.IsReady); });
    }
}
