using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SKU {

    public class ResourceAttribute : IAttribute {

        public delegate void OnValueChanged(ResourceAttribute attribute);
        event OnValueChanged _onValueChanged;

        float _value;
        float _prevValue;
        SKU.Attribute _max;

        List<IAttributeModifier> _modifiers;

        public ResourceAttribute(float value, float max) {
            _value = value;
            _max = new SKU.Attribute(max);

            _modifiers = new List<SKU.IAttributeModifier>();
            _max.AddOnValueChangedListener(OnCurrentOrMaxValueChanged);
        }

        public ResourceAttribute(float value, float max, float regen, float regenRate) 
            :this (value, max)
        {
            AddModifier(new RegenModifier(regen, regenRate));
        }

        public void Update() {
            for (int i = _modifiers.Count - 1; i >= 0; i--) {
                _value += _modifiers[i].ApplyModifier();
                if (_modifiers[i].IsOver()) {
                    _modifiers.RemoveAt(i);
                }
            }
            _max.Update();
            _value = Mathf.Clamp(_value, 0f, _max.Value);

            if (_onValueChanged != null && _prevValue != _value) {
                OnCurrentOrMaxValueChanged(null);
            }
            _prevValue = _value;
        }

        public float Value {
            get { return _value; }
        }

        public Attribute Max {
            get { return _max; }
        }

        public float Ratio() {
            return _value / _max.Value;
        }

        public void Add(float value) {
            _value += value;
        }

        public void Remove(float value) {
            _value -= value;
        }

        public void AddModifier(IAttributeModifier modifier) {
            _modifiers.Add(modifier);
        }

        public void AddOnValueChangedListener(OnValueChanged onValueChanged) {
            _onValueChanged += onValueChanged;
        }

        public void RemoveOnValueChangedListener(OnValueChanged onValueChanged) {
            _onValueChanged -= onValueChanged;
        }

        void OnCurrentOrMaxValueChanged(Attribute attribute) {
            if (_onValueChanged != null) {
                _onValueChanged(this);
            }
        }
    }
}