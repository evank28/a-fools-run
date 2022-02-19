using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // player object's transform'
    public Transform PlayerTransform;

    // relative position between camera and player
    public Vector3 _cameraOffset;

    // smooth factor of moving camera and following player
    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.5f;

    // multiplier for camera rotation
    public float rotateSpeedX = 2.0f;
    public float rotateSpeedY = 2.0f;

    // switches related to camera roration
    public bool lookAtPlayer = true;
    public bool rotateAroundPlayer = true;

    // initial camera rotation angle
    Quaternion center;

    // camera rotaion angle limit
    public float maxAngle = 45.0f;

    // Start is called before the first frame update
    void Start() {
        // record relative position between camera and player
        this._cameraOffset = transform.position - PlayerTransform.position;
        // record initial camera rotation angle
        this.center = transform.rotation;
    }

    // LateUpdate is called after Update methods
    void LateUpdate() {
        // let camera follow player
        Vector3 newPosition = PlayerTransform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

        if (rotateAroundPlayer) {
            // get mouse input can calculate rotation angles
            Quaternion cameraTurnAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotateSpeedX, Vector3.up);
            Quaternion cameraTurnAngleY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * rotateSpeedY, Vector3.right);

            // perform vertical rotation within limit
            Quaternion temp = transform.rotation * cameraTurnAngleY;
            if (Quaternion.Angle(center, temp) < this.maxAngle) {
                _cameraOffset = cameraTurnAngleY * _cameraOffset;
            }

            // perform horizontal rotation
            _cameraOffset = cameraTurnAngleX * _cameraOffset;
        }

        // let the camera focus on player
        if (lookAtPlayer || rotateAroundPlayer) {
            transform.LookAt(PlayerTransform.position);
        }
    }
}
