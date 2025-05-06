using System;
using UnityEngine;

public class PlayerNavigator : MonoBehaviour
{
    public float speed;
    public float jumpPower;

    public bool playerLooksAtMouse;
    public LayerMask lookAtLayerMask;

    private Rigidbody _rb;
    private Transform _transform;
    private bool _isGrounded;

    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MovePlayerWithKeys();

        if (playerLooksAtMouse)
        {
            LookAtMouse();
        }
    }

    private void LookAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 50, lookAtLayerMask))
        {
            var lookPos = hit.point;
            lookPos.y = _transform.position.y;
            _transform.LookAt(lookPos);
        }
    }

    void MovePlayerWithKeys()
    {
        var direction = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction -= Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction -= Vector3.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        var yVelocity = _rb.linearVelocity;

        yVelocity.x = 0;
        yVelocity.z = 0;

        _rb.linearVelocity = direction.normalized * speed + yVelocity;

        _isGrounded = Physics.Raycast(_transform.position + Vector3.up * .1f, Vector3.down, 1, lookAtLayerMask);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpPower, _rb.linearVelocity.z);
        }
    }

    public void ResetPosition()
    {
        _rb.position = Vector3.zero;
    }
}
