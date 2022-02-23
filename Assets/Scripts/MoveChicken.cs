using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChicken : MonoBehaviour
{
  private float _playerInput;
  private float _rotationInput;
  private Vector3 _userRot;
  private bool _userJumped;
  private bool _jumpInProgress = false;

  // original mass, drag, angularDrag: 1, 2, 0.05;
  private float MoveScale = 0.5f; // original 0.5
  private const float RotateScale = 3.0f; // original 1.0
  private float distanceToGround;
  private const float JumpMultiplier = 2.0f; // original 1.6
  private const float MaxSpeed = 5.0f;

  private Rigidbody _rigidbody;
  private Transform _transform;

  // Animation related
  Animator animator;
  bool moving_forward;
  bool is_grounded;
  bool jumping;

  void Start()
  {
      _rigidbody = GetComponent<Rigidbody>();
      _transform = GetComponent<Transform>();
      animator = GetComponent<Animator>();
      distanceToGround = GetComponent<Collider>().bounds.extents.y;
      //StartCoroutine(printStates());
  }

  void Update()
  {
      _playerInput = Input.GetAxis("Vertical");
      _rotationInput = Input.GetAxis("Horizontal");
      _userJumped = Input.GetButton("Jump");

      moving_forward = Input.GetKey("w") || Input.GetKey("s") ||
                        Input.GetKey("up") || Input.GetKey("down");
      is_grounded = IsGrounded();
      jumping  = Input.GetKey("space");
  }

  private void FixedUpdate()
  {
      _userRot = _transform.rotation.eulerAngles;
      _userRot += new Vector3(0, _rotationInput * RotateScale, 0);

      _transform.rotation = Quaternion.Euler(_userRot);

      // Up is always y so velocity of x and z is clamped down
      var norm = euclideanNorm(_rigidbody.velocity.x, _rigidbody.velocity.z);
      _rigidbody.velocity += transform.forward * _playerInput * MoveScale;
      animator.SetFloat("velocity", norm);
      if (moving_forward)
      {
          animator.SetBool("isIdle", false);
          animator.SetBool("isJumping", false);
          animator.SetBool("isWalking", true);
      }
      else if (!moving_forward && !jumping)
      {
          animator.SetBool("isWalking", false);
          animator.SetBool("isJumping", false);
          animator.SetBool("isIdle", true);
      }
      else if (jumping && !moving_forward)
      {
          animator.SetBool("isWalking", false);
          animator.SetBool("isIdle", false);
          animator.SetBool("isJumping", true);
      }


      // Only able to jump if you are on the ground
      if (is_grounded && _userJumped)
      {
        _rigidbody.AddForce(Vector3.up * JumpMultiplier, ForceMode.Impulse);
      }
  }

  IEnumerator printStates() {
      var norm = euclideanNorm(_rigidbody.velocity.x, _rigidbody.velocity.z);
      if (norm != 0)
      {
          print("Running");
          print($"velocity: {norm}");
      }
      else
      {
          print("Flying");
      }
      yield return new WaitForSeconds(5);
  }

  /** Return the euclidean norm of x and y */
  private float euclideanNorm (float x, float y) {
    return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
  }

  /** Send a raycast to check if player is grounded and returns true if
   the player is on some sort of ground */
  private bool IsGrounded()
  {
    return Physics.Raycast(transform.position, Vector3.down, distanceToGround - 0.3f);
  }
}
