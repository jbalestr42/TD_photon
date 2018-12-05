using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerData : ScriptableObject {

    public GameObject _gameObject;
    public GameObject _bullet;
    public float _damage;
    public string _name;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tower Data")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<TowerData>();
    }
#endif
}
