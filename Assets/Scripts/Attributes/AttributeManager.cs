using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType {
    Health = 0,
    Speed,
    AttackRate,
    Damage,
    Range
}

public class AttributeManager : MonoBehaviour {

    Dictionary<StatType, SKU.IAttribute> _attributes;

	void Awake () {
        _attributes = new Dictionary<StatType, SKU.IAttribute>();
    }
	
	void Update () {
        foreach (var attribute in _attributes) {
            attribute.Value.Update();
        }
	}

    public SKU.IAttribute Add(StatType type, SKU.IAttribute attribute) {
        if (_attributes.ContainsKey(type)) {
            Debug.LogError("This StatType already exists in the AttributeManager: " + type);
        }
        _attributes.Add(type, attribute);
        return attribute;
    }

    public SKU.IAttribute Get(StatType type) {
        if (!_attributes.ContainsKey(type)) {
            Debug.LogError("This StatType doesn't exists in the AttributeManager: " + type);
        }
        return _attributes[type];
    }

    public T Get<T>(StatType type) where T : SKU.IAttribute {
        if (!_attributes.ContainsKey(type)) {
            Debug.LogError("This StatType doesn't exists in the AttributeManager: " + type);
        }
        return (T)_attributes[type];
    }

    public float GetValue(StatType type) {
        if (!_attributes.ContainsKey(type)) {
            Debug.LogError("This StatType doesn't exists in the AttributeManager: " + type);
        }
        return _attributes[type].Value;
    }
}
