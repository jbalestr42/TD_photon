using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SKU {

    public class Attribute {

        public delegate void OnValueChanged(Attribute attribute);
        event OnValueChanged _onValueChanged;

        float _baseValue;
        float _value;
        float _prevValue;
        float _relativeValue;
        float _absoluteValue;
        List<IAttributeModifier> _relativeModifiers;
        List<IAttributeModifier> _absoluteModifiers;

        bool _isDirty = true;

        public Attribute(float value) {
            _baseValue = value;
            _relativeValue = 1f;
            _absoluteValue = 0f;
            _relativeModifiers = new List<SKU.IAttributeModifier>();
            _absoluteModifiers = new List<SKU.IAttributeModifier>();
        }

        public void Update() {
            if (_isDirty) {
                float relativeBonus = _relativeValue;
                for (int i = _relativeModifiers.Count - 1; i >= 0; i--) {
                    relativeBonus *= 1f + _relativeModifiers[i].ApplyModifier();
                    if (_relativeModifiers[i].IsOver()) {
                        _relativeModifiers.RemoveAt(i);
                    }
                }

                float absoluteBonus = _absoluteValue;
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
                _isDirty = _relativeModifiers.Count != 0 || _absoluteModifiers.Count != 0;
            }
        }

        public void AddRelativeModifier(IAttributeModifier modifier) {
            _isDirty = true;
            _relativeModifiers.Add(modifier);
        }

        public void AddAbsoluteModifier(IAttributeModifier modifier) {
            _isDirty = true;
            _absoluteModifiers.Add(modifier);
        }

        public void AddRelativeValue(float value) {
            _isDirty = true;
            _relativeValue += value;
        }

        public void AddAbsoluteValue(float value) {
            _isDirty = true;
            _absoluteValue += value;
        }

        public float Value {
            get {
                Update();
                return _value;
            }
            set { _value = value; }
        }

        public void AddOnValueChangedListener(OnValueChanged onValueChanged) {
            _isDirty = true;
            _onValueChanged += onValueChanged;
        }

        public void RemoveOnValueChangedListener(OnValueChanged onValueChanged) {
            _isDirty = true;
            _onValueChanged -= onValueChanged;
        }
    }
}