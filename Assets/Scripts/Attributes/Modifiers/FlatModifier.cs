using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatModifier : SKU.IAttributeModifier {

    float _value = 0f;

    public FlatModifier(float value) {
        _value = value;
    }

    public float ApplyModifier() {
        return _value;
    }

    public bool IsOver() {
        return false;
    }
}