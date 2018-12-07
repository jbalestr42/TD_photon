using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : Bolt.EntityBehaviour<IGameState> {

    void Start() {
        if (entity.IsOwner()) {
            state.Life = 10;
        }
    }

    void Update() {
        if (entity.IsOwner()) {
            // TODO si la vague est terminer il faut changer le ready state
        }
    }

    public override void Attached() {
        state.AddCallback("Life", () => { UIManager.Instance.SetLife(state.Life); });
    }
}
