using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {

    public GameObject _enemy;
    public int _count;
    public float _spawnRate;
}

public class WaveManager : Singleton<WaveManager> {

    public Wave _wave;
    List<GameObject> _enemies;

    void Awake() {
        _enemies = new List<GameObject>();
        _wave = new Wave();
        //TODO _wave._enemy = BoltPrefabs.Enemy;
        _wave._count = 5;
        _wave._spawnRate = 1f;
    }

    public void StartWave() {
        StartCoroutine(SpawnEnemy(_wave));
    }

    IEnumerator SpawnEnemy(Wave wave) {
        int count = wave._count;

        var wait = new WaitForSeconds(wave._spawnRate);
        while (count != 0) {
            var enemy = BoltNetwork.Instantiate(BoltPrefabs.Enemy);
            _enemies.Add(enemy);
            count--;
            yield return wait;
        }
    }

    public void KillEnemy(GameObject enemy) {
        _enemies.Remove(enemy);
        BoltNetwork.Destroy(enemy);
    }

    public List<GameObject> GetEnemies() {
        return _enemies;
    }
}
