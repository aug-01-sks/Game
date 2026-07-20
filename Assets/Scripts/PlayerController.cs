using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpSpeed = 10f;
    [SerializeField] private float _mouseSensitivity = 5f;
    [SerializeField] private Transform _pivot;
    private Vector3 _movePosition;
    private bool _shouldJump = false;
    private bool _isGrounded = false;
    private Vector3 _mousePosition;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        Move();
        Jump();
        Rotate();
    }
    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveX, 0, moveZ);
        _movePosition = direction * _moveSpeed * Time.deltaTime;
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            _shouldJump = true;
        }
    }
    private void Rotate()
    {
        _mousePosition.x += Input.GetAxis("Mouse X") * _mouseSensitivity;
        _mousePosition.y += Input.GetAxis("Mouse Y") * _mouseSensitivity;
        transform.rotation = Quaternion.Euler(new Vector3(0, _mousePosition.x, 0));
        _pivot.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(_mousePosition.y * -1f, -30, 30), _mousePosition.x, 0f));
    }
    private void FixedUpdate()
    {
        _rigidbody.MovePosition(transform.position + transform.TransformDirection(_movePosition));
        if (_shouldJump)
        {
            _rigidbody.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
            _shouldJump = false;
            _isGrounded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}
