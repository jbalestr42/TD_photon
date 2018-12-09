using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyData : ScriptableObject {

    public Bolt.PrefabId enemyId;
    public float health;
    public float speed;
    public int score;
    public int gold;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Enemy Data")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<EnemyData>();
    }
#endif
}
