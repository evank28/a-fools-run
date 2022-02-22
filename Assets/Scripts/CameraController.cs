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
    public float rotateSpeedH = 3.0f;
    public float rotateSpeedV = 3.0f;

    // initial camera rotation angle
    Quaternion center;

    // camera rotaion angle limit
    private float minAngle = -45f;
    private float maxAngle = 45f;

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
        Vector3 newPosition = PlayerTransform.position + this._cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

        // get mouse input can calculate rotation angles
        float rotateH = Input.GetAxis("Mouse X") * rotateSpeedH;
        float rotateV = Input.GetAxis("Mouse Y") * rotateSpeedV * -1;
        
        // perform horizontal rotation
        Quaternion cameraTurnAngleY = Quaternion.AngleAxis(rotateH, Vector3.up);
        Quaternion cameraTurnAngleZ, cameraTurnAngleX;
        
        // calculate vertical rotation on Z axis within limit
        if (_cameraOffset[2] >= 0) {
            cameraTurnAngleZ = Quaternion.AngleAxis(rotateV, Vector3.left);
        } else {
            cameraTurnAngleZ = Quaternion.AngleAxis(rotateV, Vector3.right);
        }
        
        // calculate vertical rotation on X axis within limit
        if (_cameraOffset[0] >= 0) {
            cameraTurnAngleX = Quaternion.AngleAxis(rotateV, Vector3.forward);
        } else {
            cameraTurnAngleX = Quaternion.AngleAxis(rotateV, Vector3.back);
        }

        // perform vertical rotation on Z axis within limit (bugs in limit need to be fixed)
        Quaternion tempZ = transform.rotation * cameraTurnAngleZ;
        //if (Quaternion.Angle(this.center, tempZ) < this.maxAngle) {
            this._cameraOffset = cameraTurnAngleZ * this._cameraOffset;
        //}
        
        // perform vertical rotation on X axis within limit (bugs in limit need to be fixed)
        Quaternion tempX = transform.rotation * cameraTurnAngleX;
        //if (Quaternion.Angle(this.center, tempX) < this.maxAngle) {
            this._cameraOffset = cameraTurnAngleX * this._cameraOffset;
        //}

        // perform horizontal rotation
        this._cameraOffset = cameraTurnAngleY * this._cameraOffset;

        // update center angle
        this.center = cameraTurnAngleY * this.center;

        // let camera focus on character
        this.transform.LookAt(PlayerTransform.position);
    }
}
