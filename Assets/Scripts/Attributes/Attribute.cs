using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SKU {

    public class Attribute {

        float _baseValue;
        float _value;
        float _min = 0.05f;
        float _max = 10000000000;
        List<IAttributeModifier> _relativeModifiers;
        List<IAttributeModifier> _absoluteModifiers;

        public Attribute(float value) {
            _baseValue = value;
            _relativeModifiers = new List<SKU.IAttributeModifier>();
            _absoluteModifiers = new List<SKU.IAttributeModifier>();
        }

        public void Update() {
            float relativeBonus = 1f;
            for (int i = _relativeModifiers.Count - 1; i >= 0; i--) {
                relativeBonus *= 1f + _relativeModifiers[i].ApplyModifier();
                if (_relativeModifiers[i].IsOver()) {
                    _relativeModifiers.RemoveAt(i);
                    Debug.Log("remove");
                }
            }

            float absoluteBonus = 0f;
            for (int i = _absoluteModifiers.Count - 1; i >= 0; i--) {
                absoluteBonus += _absoluteModifiers[i].ApplyModifier();
                if (_absoluteModifiers[i].IsOver()) {
                    _absoluteModifiers.RemoveAt(i);
                    Debug.Log("remove");
                }
            }

            _value = _baseValue * relativeBonus + absoluteBonus;
            _value = Mathf.Clamp(_value, _min, _max);
        }

        public void AddRelativeModifier(IAttributeModifier modifier) {
            _relativeModifiers.Add(modifier);
        }

        public float Value {
            // TODO compute here et set dirty when needed ?
            get { return _value; }
        }
    }
}