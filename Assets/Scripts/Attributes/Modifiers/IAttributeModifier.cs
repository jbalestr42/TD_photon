using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SKU {

    public interface IAttributeModifier {

        float ApplyModifier();
        bool IsOver();
    }
}