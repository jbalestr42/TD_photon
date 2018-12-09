using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : Bolt.EntityBehaviour<IGameState> {

    public override void Attached() {
        state.AddCallback("Life", () => { UIManager.Instance.SetLife(state.Life); });
    }

    public int GetLife() {
        return state.Life;
    }

    public bool IsOwner() {
        return entity.IsOwner();
    }
}
