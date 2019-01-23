using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerData : ScriptableObject {

    public GameObject model;
    public TowerType towerType;
    public BulletType bulletId;
    public float damage;
    public float attackRate;
    public float range;
    public string name;
    public int cost;
    public List<ModifierType> modifiers = null;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tower Data")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<TowerData>();
    }
#endif
}
