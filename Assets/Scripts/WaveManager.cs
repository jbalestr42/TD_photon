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

    void Awake() {
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
            var entity = BoltNetwork.Instantiate(BoltPrefabs.Enemy);
            count--;
            yield return wait;
        }
    }
}
