using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager> {

    Camera _camera;
    ICameraMovement _cameraMovement;

	void Awake() {
        _camera = Camera.main;
        _cameraMovement = null;

    }
	
	void Update() {
		if (_cameraMovement != null) {
            _cameraMovement.Update(_camera);
        }
	}

    public void SetCameraMovement(ICameraMovement cameraMovement) {
        _cameraMovement = cameraMovement;
        // Faire une transition quand on change de camera ? ICameraTransition, lerp, instant, ...
    }
}
