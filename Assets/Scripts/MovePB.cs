using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePB : MonoBehaviour
{
    private float _playerInput;
    private float _rotationInput;
    private Vector3 _userRot;
    private bool _userJumped;
    private bool _jumpInProgress = false;
    
    // original mass, drag, angularDrag: 1, 2, 0.05;
    private float MoveScale = 0.5f; // original 0.5
    private const float RotateScale = 3.0f; // original 1.0
    private const float GroundThreshold = 0;
    private const float CloseToGroundThreshold = 0.1f;
    private const float JumpMultiplier = 1.6f; // original 1.6
    private const float MaxSpeed = 5.0f;

    private Rigidbody _rigidbody;
    private Transform _transform;

    Animator animator;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        // StartCoroutine(printStates());
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
        _userRot += new Vector3(0, _rotationInput * RotateScale, 0);

        _transform.rotation = Quaternion.Euler(_userRot);

        // Up is always y so velocity of x and z is clamped down
        var norm = euclideanNorm(_rigidbody.velocity.x, _rigidbody.velocity.z);
        if (norm < MaxSpeed) {
          _rigidbody.velocity += transform.forward * _playerInput * MoveScale;
          animator.SetFloat("velocity", norm);
          if (norm != 0)
          {
              animator.SetTrigger("triggerWalking");
          }
          else
          {
              animator.SetTrigger("triggerIdle");
          }
        }

        // If the player is *close* to the ground, the jump will be triggered.
        // This allows for a "harder"/"longer" keypress to enable a slightly larger jump.
        if(_userJumped &&
           ((_jumpInProgress && _transform.position[1] <= CloseToGroundThreshold)
           ||  _transform.position[1] <= GroundThreshold))
        {
            _rigidbody.AddForce(Vector3.up * JumpMultiplier, ForceMode.Impulse);
            _userJumped = false;
            _jumpInProgress = true;
        }

        // Once the user is far from the ground, indicate a jump is no longer in progress
        // This will require the user to hit the ground again before jumping again.
        if (_jumpInProgress && _transform.position[1] > CloseToGroundThreshold)
        {
            _jumpInProgress = false;
        }

        // Seems to be a duplicate code block. Please verify. ///////////////////////////

        // Once the user is far from the ground, indicate a jump is no longer in progress
        // This will require the user to hit the ground again before jumping again.
        // if (_jumpInProgress && _transform.position[1] > CloseToGroundThreshold)
        // {
        //     _jumpInProgress = false;
        // }

        // //////////////////////////////////////////////////////////////////////////////
    }
    
    IEnumerator printStates() {
        var norm = euclideanNorm(_rigidbody.velocity.x, _rigidbody.velocity.z);
        if (norm != 0)
        {
            print("walking...");
            print($"velocity: {norm}");
        }
        else
        {
            print("idle...");
        }
        yield return new WaitForSeconds(5);
    }

    /** Return the euclidean norm of x and y */
    private float euclideanNorm (float x, float y) {
      return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
    }
}
