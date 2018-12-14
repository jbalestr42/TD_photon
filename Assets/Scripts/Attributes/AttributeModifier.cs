using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SKU {

    public interface IAttributeModifier {

        float ApplyModifier();
        bool IsOver();
    }

    public class TimeModifier : IAttributeModifier {
        float _duration = 0f;
        float _value = 0f;
        float _start = 0f;

        public TimeModifier(float duration, float value) {
            _duration = duration;
            _value = value;
            _start = Time.realtimeSinceStartup;
        }

        public float ApplyModifier() {
            return _value * (1f - GetRatio());
        }

        public bool IsOver() {
            return (Time.realtimeSinceStartup - _start) >= _duration;
        }

        float GetRatio() {
            float ratio = (Time.realtimeSinceStartup - _start) / _duration;
            ratio = Mathf.Clamp(ratio, 0f, 1f);
            return ratio;
        }
    }

    public class RegenModifier : IAttributeModifier {

        float _regenRate = 0f;
        float _value = 0f;
        float _start = 0f;

        public RegenModifier(float value, float regenRate) {
            _value = value;
            _regenRate = regenRate;
            _start = Time.realtimeSinceStartup;
        }

        public float ApplyModifier() {
            float diff = Time.realtimeSinceStartup - _start;
            if (diff >= _regenRate) {
                _start = Time.realtimeSinceStartup - (diff - _regenRate);
                return _value;
            }
            return 0f;
        }

        public bool IsOver() {
            return false;
        }
    }

    public class FlatModifier : IAttributeModifier {

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
}