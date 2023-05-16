using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Camera mainCamera;

    public string cameraMode;
    // Start is called before the first frame update
    void Start()
    {
        cameraMode = "Iso45Ortho";
        transform.position = playerTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        switch (cameraMode) {
            case "Iso45Ortho":
                transform.rotation = Quaternion.Euler(30, 45, 0);
                mainCamera.orthographic = true;
                break;
            case "Iso70Per":
                transform.rotation = Quaternion.Euler(70, 45, 0);
                mainCamera.orthographic = false;
                break;
            case "FreeCam":
                mainCamera.orthographic = false;
                break;
            default:
                break;
        }
        transform.position = playerTransform.position;
    }

    void MyInput() {
        //Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Keypad1)
        
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Keypad1)) {
            cameraMode = "Iso45Ortho";
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Keypad2)) {
            cameraMode = "Iso70Per";
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Keypad3)) {
            cameraMode = "FreeCam";
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Keypad4)) {
            cameraMode = "Iso45";
        }
    }
}
