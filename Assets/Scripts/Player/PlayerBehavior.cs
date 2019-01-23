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

    UIPlayerHUD _playerUI;
    Renderer _renderer;

    void Start() {
        if (entity.IsOwner()) {
            state.Gold = 100;
            state.Score = 0;
            state.Color = Random.ColorHSV();
            state.IsReady = false;
        }
    }

    public override void Attached() {
        _renderer = GetComponentInChildren<Renderer>();
        _playerUI = GetComponentInChildren<UIPlayerHUD>();

        state.AddCallback("Color", () => { _renderer.material.color = state.Color; });
        state.AddCallback("Name", () => { _playerUI.SetPlayerName(state.Name); });
    }

    public override void ControlGained() {
        state.AddCallback("Gold", () => { UpdateGold(state.Gold); });
        state.AddCallback("Score", () => { UIManager.Instance.SetScore(state.Score); });
        state.AddCallback("IsReady", () => { UIManager.Instance.OnReadyStateChanged(state.IsReady); });
    }

    void UpdateGold(int gold) {
        UIManager.Instance.SetGold(state.Gold);
        UIManager.Instance.GetUIInventory.UpdateAffordableTowers(state.Gold);
    }
}
