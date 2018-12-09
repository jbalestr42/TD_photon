using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : Bolt.EntityBehaviour<IGameState> {

    public override void Attached() {
        state.AddCallback("Life", () => { UIManager.Instance.SetLife(state.Life); });
    }

    public int Life {
        get { return state.Life; }
        set { state.Life = value; }
    }

    public bool IsOwner() {
        return entity.IsOwner();
    }
}
