using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SKU {

    public class Attribute : IAttribute {

        public delegate void OnValueChanged(Attribute attribute);
        event OnValueChanged _onValueChanged;

        float _baseValue;
        float _value;
        float _prevValue;
        List<IAttributeModifier> _relativeModifiers;
        List<IAttributeModifier> _absoluteModifiers;

        public Attribute(float value) {
            _prevValue = _value;
            _baseValue = value;
            _relativeModifiers = new List<SKU.IAttributeModifier>();
            _absoluteModifiers = new List<SKU.IAttributeModifier>();
            Update();
        }

        public void Update() {
            float relativeBonus = 1f;
            for (int i = _relativeModifiers.Count - 1; i >= 0; i--) {
                relativeBonus *= 1f + _relativeModifiers[i].ApplyModifier();
                if (_relativeModifiers[i].IsOver()) {
                    _relativeModifiers.RemoveAt(i);
                }
            }

            float absoluteBonus = 0f;
            for (int i = _absoluteModifiers.Count - 1; i >= 0; i--) {
                absoluteBonus += _absoluteModifiers[i].ApplyModifier();
                if (_absoluteModifiers[i].IsOver()) {
                    _absoluteModifiers.RemoveAt(i);
                }
            }

            _prevValue = _value;
            _value = _baseValue * relativeBonus + absoluteBonus;
            _value = Mathf.Max(_value, 0f);

            if (_onValueChanged != null && _prevValue != _value) {
                _onValueChanged(this);
            }
        }

        public void AddRelativeModifier(IAttributeModifier modifier) {
            _relativeModifiers.Add(modifier);
        }

        public void AddAbsoluteModifier(IAttributeModifier modifier) {
            _absoluteModifiers.Add(modifier);
        }

        public float Value {
            get { return _value; }
        }

        public void AddOnValueChangedListener(OnValueChanged onValueChanged) {
            _onValueChanged += onValueChanged;
        }

        public void RemoveOnValueChangedListener(OnValueChanged onValueChanged) {
            _onValueChanged -= onValueChanged;
        }
    }
}