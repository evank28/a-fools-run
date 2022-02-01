using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePB : MonoBehaviour
{
    private float _playerVerticalInput;
    private float _playerHorizontalInput;
    private Vector3 _userRot;
    private bool _userJumped;
    private bool _touchingGround;

    private const float _inputScale = 0.5f;

    private Rigidbody _rigidbody;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerVerticalInput = Input.GetAxis("Vertical");
        _playerHorizontalInput = Input.GetAxis("Horizontal");
        _userJumped = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        _userRot = _transform.rotation.eulerAngles;
        _userRot += new Vector3(0, _playerHorizontalInput, 0);

        _rigidbody.velocity += transform.forward
                               * _playerVerticalInput
                               * _inputScale;
        _rigidbody.velocity += transform.right
                               * _playerHorizontalInput
                               * _inputScale;

        if(_userJumped)
        {
            _rigidbody.AddForce(Vector3.up, ForceMode.VelocityChange);
            _userJumped = false;
        }
    }
}
