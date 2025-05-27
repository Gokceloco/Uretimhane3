using DG.Tweening;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform followObject;
    public float offsetByLookDirection;

    private Vector3 _vel;
    public float smoothTime;

    public Camera mainCamera;
    private Vector3 _cameraStartPos;

    private void Start()
    {
        _cameraStartPos = mainCamera.transform.localPosition;
    }

    private void FixedUpdate()
    {
        var targetPos = followObject.position + followObject.forward * offsetByLookDirection;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _vel, smoothTime);
    }

    public void ShakeCamera(float magnitude, float duration)
    {
        mainCamera.transform.DOKill();
        mainCamera.transform.localPosition = _cameraStartPos;
        mainCamera.transform.DOShakePosition(duration, magnitude);
    }

}
