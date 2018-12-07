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

    void Start() {
        if (entity.IsOwner()) {
            state.Gold = 100;
            state.Score = 0;
            state.Color = Random.ColorHSV();
        }
    }

    public override void Attached() {
        _renderer = GetComponentInChildren<Renderer>();
        _playerUI = GetComponentInChildren<PlayerUI>();

        state.AddCallback("Color", () => { _renderer.material.color = state.Color; });
        state.AddCallback("Name", () => { _playerUI.SetPlayerName(state.Name); });
        state.AddCallback("IsReady", () => { UIManager.Instance.SetReadyButton(state.IsReady); });
    }

    public override void ControlGained() {
        state.AddCallback("Gold", () => { UIManager.Instance.SetGold(state.Gold); });
        state.AddCallback("Score", () => { UIManager.Instance.SetScore(state.Score); });
    }
}
