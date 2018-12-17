using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType {
    Normal = 0,
    Speed,
}

public class DataManager : Singleton<DataManager> {

    public List<WaveData> _waves = null;
    public List<TowerData> _towers = null;


    [SerializeField]
    private BulletTypeGameObjectDictionary _bullets = BulletTypeGameObjectDictionary.New<BulletTypeGameObjectDictionary>();
    public Dictionary<BulletType, GameObject> Bullets {
        get { return _bullets.dictionary; }
    }

    public int GetTowerCost(Bolt.PrefabId towerId) {
        for (int i = 0; i < _towers.Count; i++) {
            if (_towers[i].towerId == towerId) {
                return _towers[i].cost;
            }
        }
        Debug.Log("This tower is not registered in the list: " + towerId);
        return 0;
    }


}
