using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public enum BulletType {
    Normal = 0,
    Speed,
}

public enum ModifierType {
    Time = 0,
    Regen,
    Flat
}

public class Factory : Singleton<Factory> {

    public List<GameObject> _bullets;

    public GameObject InstantiateBullet(BulletType id) {
        Assert.IsNotNull(_bullets);
        Assert.IsTrue((int)id >= 0 && (int)id < _bullets.Count);

        var bullet = Instantiate(_bullets[(int)id]);
        return bullet;
    }

    public static SKU.IAttributeModifier CreateModifier(ModifierType type, params object[] parameters) {
        SKU.IAttributeModifier modifier = null;
        switch (type) {
            case ModifierType.Flat:
                Assert.IsTrue(parameters.Length == 1);
                modifier = new FlatModifier((float)parameters[0]);
                break;
            case ModifierType.Time:
                Assert.IsTrue(parameters.Length == 2);
                modifier = new TimeModifier((float)parameters[0], (float)parameters[1]);
                break;
            case ModifierType.Regen:
                Assert.IsTrue(parameters.Length == 2);
                modifier = new RegenModifier((float)parameters[0], (float)parameters[1]);
                break;
            default:
                Debug.Log("This ModifierType is no implemented: " + type);
                break;
        }
        Assert.IsNotNull(modifier);
        return modifier;
    }
}
