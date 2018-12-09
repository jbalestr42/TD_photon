using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerData : ScriptableObject {

    public GameObject _gameObject;
    public BulletType _bulletId;
    public float _damage;
    public string _name;
    public int _cost;

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tower Data")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<TowerData>();
    }
#endif
}
