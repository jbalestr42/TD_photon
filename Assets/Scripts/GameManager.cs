using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    GameBehaviour _game;

    void Start() {
    }

    public GameBehaviour Game {
        get { return _game; }
        set { _game = value; }
    }

    public void LooseLife(int amount) {
        _game.state.Life -= amount;
    }
}
