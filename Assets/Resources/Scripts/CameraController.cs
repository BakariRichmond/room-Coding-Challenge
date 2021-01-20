using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = 10f;
    public float yMaxLimit = 80f;
    public float distanceMin = 2f;
    public float distanceMax = 15f;
    Vector3 PlayerOffset;
    public bool init;
    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start () {

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

    }

    void LateUpdate () {
        if (target != null) {
            //sets xy coordinates  based on mouse input values
            x += Input.GetAxis ("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle (y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler (y, x, 0);

            distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast (target.position, transform.position, out hit)) {
                distance -= hit.distance;

            }
            Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            if (!init) {
                init = true;
                rotation = transform.rotation;
                position = transform.position;

                x = transform.eulerAngles.y;
                y = transform.eulerAngles.x;

            }
            transform.rotation = rotation;
            transform.position = position;

        }
        else{
            Debug.Log("target is null");
        }
    }

    public static float ClampAngle (float angle, float min, float max) {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp (angle, min, max);
    }
}