using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Camera mainCamera;

    //camera sensitivity
    public float zoomSensitivity;
    public float cameraSensitivity;

    //vector from origin to the center of grid
    public Vector3 orbitOrigin;


    //pitch and orbit angle of the camera from the orbit point
    public float startOrbitAngle;
    public float startPitchAngle;

    float orbitAngle;
    float pitchAngle;

    //a vector to store the normalised vector pointing from the center of grid to the camera
    Vector3 cameraDirectionVector = Vector3.zero;
    // the magnitude of the direction vector and how close it is to the center grid
    float cameraDistanceFromOrigin = 6.0f;

    //vector for storing delta mouse position
    Vector3 deltaMousePos = Vector3.zero;
    Vector3 lastMousePos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //set the initial orbit angle and pitch angle
        orbitAngle = startOrbitAngle;
        pitchAngle = startPitchAngle;
    }

    // Update is called once per frame
    void Update()
    {
        //calculate delta mouse position logic
        deltaMousePos = Input.mousePosition - lastMousePos;
        lastMousePos = Input.mousePosition;


        // if the left button is held down then change the pitch angle and orbit angle depending on the change in mouse position
        if (Input.GetMouseButton(0))
        {
            orbitAngle -= deltaMousePos.x * cameraSensitivity;
            pitchAngle -= deltaMousePos.y * cameraSensitivity;
            pitchAngle = Mathf.Clamp(pitchAngle, 0, Mathf.PI / 2.01f); // clamp the pitch angle to 0 degrees to roughly 90 degrees
        }


        //set the camera direction vectors components with trig values with given angles
        cameraDirectionVector.x = Mathf.Cos(pitchAngle) * Mathf.Cos(orbitAngle);
        cameraDirectionVector.z = Mathf.Cos(pitchAngle) * Mathf.Sin(orbitAngle);

        cameraDirectionVector.y = Mathf.Sin(pitchAngle);

        

        // if the mouse is scrolled forward then zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        { //scroll forward

            cameraDistanceFromOrigin -= zoomSensitivity;

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) //scroll backwards then zoom out
        {

            cameraDistanceFromOrigin += zoomSensitivity;

        }
        //set the position of the camera
        mainCamera.transform.position = (cameraDirectionVector * cameraDistanceFromOrigin) + orbitOrigin;
        //set the rotation of the camera to look at the grid center
        mainCamera.transform.LookAt(orbitOrigin);

    }
}
