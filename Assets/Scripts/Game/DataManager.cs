using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum BulletType {
    Normal = 0,
    Speed,
}

public enum TowerType {
    None = 0,
    Normal,
    Slow,
    Wall
}

public class DataManager : Singleton<DataManager> {

    public List<WaveData> _waves = null;

    [SerializeField]
    private BulletTypeGameObjectDictionary _bullets = BulletTypeGameObjectDictionary.New<BulletTypeGameObjectDictionary>();
    public Dictionary<BulletType, GameObject> Bullets {
        get { return _bullets.dictionary; }
    }

    [SerializeField]
    private TowerTypeTowerDataDictionary _towers = TowerTypeTowerDataDictionary.New<TowerTypeTowerDataDictionary>();
    public Dictionary<TowerType, TowerData> Towers {
        get { return _towers.dictionary; }
    }

    public TowerData GetTowerData(TowerType type) {
        Assert.IsTrue(Towers.ContainsKey(type), "This tower is not registered in the list: " + type);
        return Towers[type];
    }
}
