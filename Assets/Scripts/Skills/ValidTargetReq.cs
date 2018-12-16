using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidTargetReq : IRequirement {

    IAttacker _attacker;

    public ValidTargetReq(GameObject p_owner) {
        _attacker = p_owner.GetComponent<IAttacker>();
    }

    public bool IsValid(GameObject p_owner) {
        return (_attacker.GetTarget());
    }
}
