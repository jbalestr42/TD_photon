using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum GameState {
    None,
    WaitingForPlayers,
    StartWave, // Start the coroutine to spawn enemies
    Spawning, // Spawn enemies
    WaitingForEndOfWave, // All enemies are spawned, waiting for them to be killed or reach the end
    NextWave, // Prepare the next wave
    GameOver, // Game lost
    GameEnd, // Game win
}

/*
 * Only instantiated by the server
 * */
public class GameManager : Singleton<GameManager> {

    List<WaveData> _waves = null;

    GameState _state;
    int _currentWave = 0;
    GameBehaviour _game = null; // The game behaviour is set when the server player is instantiated
    EntityManager _entities = null; // The game behaviour is set when the server player is instantiated

    void Start() {
        _state = GameState.WaitingForPlayers;
        _entities = EntityManager.Instance;
        _waves = DataManager.Instance._waves;
    }

    void Update() {
        if (!IsOwner()) {
            return;
        }

        switch (_state) {
            case GameState.WaitingForPlayers:
                if (CanStartGame()) {
                    _game.Life = 10; // TODO: From game mode
                    StartGame();
                    _state = GameState.Spawning;
                }
                break;
            case GameState.Spawning:
                break;
            case GameState.WaitingForEndOfWave:
                if (_entities.AreAllEnemyDead()) {
                     _state = GameState.NextWave;
                }
                break;
            case GameState.NextWave:
                SetAllPlayerReadyState(false);
                _currentWave++;
                if (_currentWave >= _waves.Count) {
                    _state = GameState.GameEnd;
                } else {
                    _state = GameState.WaitingForPlayers;
                }
                break;
            case GameState.GameEnd:
                _currentWave = 0; //TODO: proper end, for now it's a restart
                _state = GameState.WaitingForPlayers;
                break;
            case GameState.GameOver:
                // Restart all, kill enemies, bullets, towers, reset score and gold
                _state = GameState.GameEnd;
                break;
            default:
                Debug.Log("State not implemented: " + _state);
                break;
        }
    }

    bool IsOwner() {
        return _game && _game.IsOwner();
    }

    public GameBehaviour Game {
        get { return _game; }
        set { _game = value; }
    }

    public void LooseLife(int amount) {
        _game.Life -= amount;
        if (_game.Life <= 0) {
            _state = GameState.GameOver;
        }
    }

    public bool CanStartGame() {
        return AreAllPlayerReady();
    }

    public void StartGame() {
        Assert.IsNotNull(_waves);
        StartCoroutine(StartWave(_waves[_currentWave]));
    }

    public void SetAllPlayerReadyState(bool ready) {
        var players = PlayerObjectRegistry.GetPlayers;
        foreach (var player in players) {
            if (player.behavior != null && player.behavior.state != null) {
                player.behavior.state.IsReady = ready;
            }
        }
    }

    public bool AreAllPlayerReady() {
        var players = PlayerObjectRegistry.GetPlayers;
        bool ready = true;
        foreach (var player in players) {
            ready = ready && player.behavior.state.IsReady;
        }
        return ready;
    }

    IEnumerator StartWave(WaveData wave) {
        int count = wave.count;

        var wait = new WaitForSeconds(wave.spawnRate);
        while (count != 0) {
            EntityManager.Instance.SpawnEnemy(wave.enemyData);
            count--;
            yield return wait;
        }
        _state = GameState.WaitingForEndOfWave;
        yield return null;
    }
}
