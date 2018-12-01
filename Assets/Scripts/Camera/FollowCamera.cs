using UnityEngine;

public class FollowCamera : ICameraMovement {

    public Transform _target;
    public float _smoothTime = 0.3f;
    public Vector3 _offset = new Vector3(0, 5, -11);

    private Vector3 _velocity = Vector3.zero;

    public FollowCamera(Transform target, Vector3 offset) {
        _target = target;
        _offset = offset;
    }

    public void Update(Camera camera) {
        Vector3 targetPosition = _target.transform.position + _offset;
        //Vector3 targetPosition = _target.TransformPoint(_offset); to look toward the target

        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, targetPosition, ref _velocity, _smoothTime);
        camera.transform.LookAt(_target);
        // TODO: mouse wheel to zoom in out
    }
}