using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraController : MonoBehaviour
{
    private CinemachineVirtualCamera cvcam;
    public float cameraVelocity = 5f;
    // Start is called before the first frame update
    void Start()
    {
        cvcam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //  var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

        transform.Rotate(mouseMovement.y * 0, mouseMovement.x * cameraVelocity, 0f);
        cvcam.transform.Rotate(mouseMovement.y *0, mouseMovement.x * cameraVelocity, 0f);
    }
}
