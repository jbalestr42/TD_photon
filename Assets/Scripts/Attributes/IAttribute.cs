using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SKU {

    public interface IAttribute {
        void Update();
        float Value { get; }
    }
}