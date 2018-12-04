using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerData : ScriptableObject {

    public GameObject _gameObject;

    [MenuItem("Assets/Create/Tower Data")]
    public static void CreateAsset() {
        ScriptableObjectUtility.CreateAsset<TowerData>();
    }
}
