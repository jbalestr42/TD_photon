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
}