using System;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    private GameObject _pickedUpObject;

    public Camera mainCamera;
    public LayerMask groundLayerMask;

    private void Update()
    {
        if (_pickedUpObject != null)
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButton(0) && Physics.Raycast(ray.origin, ray.direction, out var hit, 50, groundLayerMask))
            {
                _pickedUpObject.transform.position = hit.point + Vector3.up;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _pickedUpObject.transform.position = new Vector3(_pickedUpObject.transform.position.x, 0, _pickedUpObject.transform.position.z);
            _pickedUpObject = null;
        }
    }

    public void SetPickedUpObject(Token token)
    {
        _pickedUpObject = token.gameObject;
    }
}
