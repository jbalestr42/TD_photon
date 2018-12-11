using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerData : ScriptableObject {

    public GameObject localVisualisation;
    public Bolt.PrefabId towerId;
    public BulletType bulletId;
    public float damage;
    public string name;
    public int cost;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tower Data")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<TowerData>();
    }
#endif
}
