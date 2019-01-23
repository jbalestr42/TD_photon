using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttributeType {
    Health = 0,
    Speed,
    AttackRate,
    Damage,
    Range
}

public class AttributeManager : MonoBehaviour {

    Dictionary<AttributeType, SKU.IAttribute> _attributes;

	void Awake () {
        _attributes = new Dictionary<AttributeType, SKU.IAttribute>();
    }
	
	void Update () {
        foreach (var attribute in _attributes) {
            attribute.Value.Update();
        }
	}

    public SKU.IAttribute Add(AttributeType type, SKU.IAttribute attribute) {
        if (_attributes.ContainsKey(type)) {
            Debug.LogError("This AttributeType already exists in the AttributeManager: " + type);
        }
        _attributes.Add(type, attribute);
        return attribute;
    }

    public SKU.IAttribute Get(AttributeType type) {
        if (!_attributes.ContainsKey(type)) {
            Debug.LogError("This AttributeType doesn't exists in the AttributeManager: " + type);
        }
        return _attributes[type];
    }

    public T Get<T>(AttributeType type) where T : SKU.IAttribute {
        if (!_attributes.ContainsKey(type)) {
            Debug.LogError("This AttributeType doesn't exists in the AttributeManager: " + type);
        }
        return (T)_attributes[type];
    }

    public float GetValue(AttributeType type) {
        if (!_attributes.ContainsKey(type)) {
            Debug.LogError("This AttributeType doesn't exists in the AttributeManager: " + type);
        }
        return _attributes[type].Value;
    }
}
