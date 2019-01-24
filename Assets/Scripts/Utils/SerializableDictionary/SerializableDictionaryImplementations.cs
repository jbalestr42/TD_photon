using System;

using UnityEngine;

// ---------------
//  String => Int
// ---------------
[Serializable]
public class StringIntDictionary : SerializableDictionary<string, int> { }

// ---------------
//  GameObject => Float
// ---------------
[Serializable]
public class GameObjectFloatDictionary : SerializableDictionary<GameObject, float> { }

// ---------------
//  BulletType => GameObject
// ---------------
[Serializable]
public class BulletTypeGameObjectDictionary : SerializableDictionary<BulletType, GameObject> { }

// ---------------
//  TowerType => TowerData
// ---------------
[Serializable]
public class TowerTypeTowerDataDictionary : SerializableDictionary<TowerType, TowerData> { }

// ---------------
//  UIType => APanel
// ---------------
[Serializable]
public class PanelTypeAPanelDictionary : SerializableDictionary<PanelType, APanel> { }