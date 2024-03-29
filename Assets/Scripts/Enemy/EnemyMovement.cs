﻿using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour {

    Transform _start;
    Transform _end;

    SKU.Attribute _speed = null;
    AIPath _aiPath = null;

    void Awake() {
        // TODO Spawn Manager
        _start = GameObject.FindWithTag("SpawnStart").transform;
        _end = GameObject.FindWithTag("SpawnEnd").transform;
        transform.position = _start.position;
    }

    void Update() {
        if (_aiPath != null) {
            _aiPath.maxSpeed = _speed.Value;
        }
    }

    public void Init_Server(float speed) {
        _speed = new SKU.Attribute(speed);
        GetComponent<AttributeManager>().Add(AttributeType.Speed, _speed);

        _aiPath = gameObject.AddComponent<AIPath>();
        _aiPath.pickNextWaypointDist = 0.5f;
        AIDestinationSetter destination = gameObject.AddComponent<AIDestinationSetter>();
        destination.target = _end;

        RaycastModifier modifier = gameObject.AddComponent<RaycastModifier>();
        modifier.thickRaycast = true;
        modifier.thickRaycastRadius = 0.5f;
    }
}
