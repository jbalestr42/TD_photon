using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaveData : ScriptableObject {

    public EnemyData enemyData;
    public float spawnRate;
    public int count;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Wave Data")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<WaveData>();
    }
#endif
}
