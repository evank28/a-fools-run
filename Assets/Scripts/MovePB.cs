using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePB : MonoBehaviour
{
    private float _playerInput;
    private float _rotationInput;
    private Vector3 _userRot;
    private bool _userJumped;

    private const float _inputScale = 0.5f;
    private const float _groundThreshold = 0.1f;
    private const float _jumpMultiplier = 1.6f;
    private const float _maxSpeed = 5.0f;

    private Rigidbody _rigidbody;
    private Transform _transform;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        _playerInput = Input.GetAxis("Vertical");
        _rotationInput = Input.GetAxis("Horizontal");
        _userJumped = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        _userRot = _transform.rotation.eulerAngles;
        _userRot += new Vector3(0, _rotationInput, 0);

        _transform.rotation = Quaternion.Euler(_userRot);

        // Up is always z so velocity of x and z is clamped down
        if (euclideanNorm(_rigidbody.velocity.x,
                          _rigidbody.velocity.z) < _maxSpeed)
          _rigidbody.velocity += transform.forward * _playerInput * _inputScale;

        // If the player is *close* to the ground, the jump will be triggered.
        // This allows for a "harder"/"longer" keypress to enable a slightly larger jump.
        if(_userJumped && _transform.position[1] <= _groundThreshold)
        {
            _rigidbody.AddForce(Vector3.up * _jumpMultiplier, ForceMode.Impulse);
            _userJumped = false;
        }
    }

    /** Return the euclidean norm of x and y */
    private float euclideanNorm (float x, float y) {
      return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
    }
}
