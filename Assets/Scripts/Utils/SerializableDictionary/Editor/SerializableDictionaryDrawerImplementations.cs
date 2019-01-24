using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

// ---------------
//  String => Int
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(StringIntDictionary))]
public class StringIntDictionaryDrawer : SerializableDictionaryDrawer<string, int> {
    protected override SerializableKeyValueTemplate<string, int> GetTemplate() {
        return GetGenericTemplate<SerializableStringIntTemplate>();
    }
}
internal class SerializableStringIntTemplate : SerializableKeyValueTemplate<string, int> { }

// ---------------
//  GameObject => Float
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(GameObjectFloatDictionary))]
public class GameObjectFloatDictionaryDrawer : SerializableDictionaryDrawer<GameObject, float> {
    protected override SerializableKeyValueTemplate<GameObject, float> GetTemplate() {
        return GetGenericTemplate<SerializableGameObjectFloatTemplate>();
    }
}
internal class SerializableGameObjectFloatTemplate : SerializableKeyValueTemplate<GameObject, float> { }

// ---------------
//  BulletType => GameObject
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(BulletTypeGameObjectDictionary))]
public class BulletTypeGameObjectDictionaryDrawer : SerializableDictionaryDrawer<BulletType, GameObject> {
    protected override SerializableKeyValueTemplate<BulletType, GameObject> GetTemplate() {
        return GetGenericTemplate<SerializableBulletTypeGameObjectTemplate>();
    }
}
internal class SerializableBulletTypeGameObjectTemplate : SerializableKeyValueTemplate<BulletType, GameObject> { }

// ---------------
//  TowerType => TowerData
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(TowerTypeTowerDataDictionary))]
public class TowerTypeTowerDataDictionaryDrawer : SerializableDictionaryDrawer<TowerType, TowerData> {
    protected override SerializableKeyValueTemplate<TowerType, TowerData> GetTemplate() {
        return GetGenericTemplate<SerializableTowerTypeTowerDataTemplate>();
    }
}
internal class SerializableTowerTypeTowerDataTemplate : SerializableKeyValueTemplate<TowerType, TowerData> { }

// ---------------
//  PanelType => APanel
// ---------------
[UnityEditor.CustomPropertyDrawer(typeof(PanelTypeAPanelDictionary))]
public class PanelTypeAPanelDictionaryDrawer : SerializableDictionaryDrawer<PanelType, APanel> {
    protected override SerializableKeyValueTemplate<PanelType, APanel> GetTemplate() {
        return GetGenericTemplate<SerializablePanelTypeAPanelTemplate>();
    }
}
internal class SerializablePanelTypeAPanelTemplate : SerializableKeyValueTemplate<PanelType, APanel> { }
