using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASkill : MonoBehaviour {

    // TODO Can be a list in case of multiple item want the cooldown ?
    IProgressTracker _progressTracker = null;

    List<IRequirement> _requirements;

    float _castTimer = 0.0f;
    SKU.Attribute _castDuration = null;

    float _cooldown = 0.0f;
    SKU.Attribute _cooldownDuration = null;

    protected void Init(SKU.Attribute p_castDuration, SKU.Attribute p_cooldownDuration, List<IRequirement> p_requirements, IProgressTracker p_progressTracker = null) {
        _castDuration = p_castDuration;
        _cooldownDuration = p_cooldownDuration;
        _requirements = p_requirements;
        _progressTracker = p_progressTracker;
    }

    protected void Init(SKU.Attribute p_cooldownDuration, List<IRequirement> p_requirements, IProgressTracker p_progressTracker = null) 
    {
        Init(new SKU.Attribute(0f), p_cooldownDuration, p_requirements, p_progressTracker);
    }

    void Update() {
        if (_cooldown <= 0.0f) {
            if (IsRequirementValidated()) {
                _castTimer -= Time.deltaTime;
                if (_castTimer <= 0.0f) {
                    Cast(gameObject);
                    _castTimer = _castDuration.Value;
                    _cooldown = _cooldownDuration.Value;
                }
            } else {
                _castTimer = _castDuration.Value;
            }
        } else {
            _cooldown = Mathf.Clamp(_cooldown - Time.deltaTime, 0.0f, _cooldownDuration.Value);
            if (_progressTracker != null) {
                _progressTracker.UpdateProgress(this);
            }
        }
    }

    bool IsRequirementValidated() {
        if (_requirements != null) {
            foreach (var requirement in _requirements) {
                if (!requirement.IsValid(gameObject)) {
                    return false;
                }
            }
        }
        return true;
    }

    float GetCooldownRatio() {
        return _cooldown / _cooldownDuration.Value;
    }

    public abstract void Cast(GameObject p_owner);
}