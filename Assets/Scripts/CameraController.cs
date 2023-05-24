using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Camera mainCamera;

    private Vector3 camOrigin = new Vector3(0, 0, -10);

    public float sensX;
    public float sensY;
    public float camSpeed = 0.025f;

    public string cameraMode;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
                transform.position = new Vector3(0, 3, -2);
                break;
            case "Iso70Per":
                transform.rotation = Quaternion.Euler(70, 45, 0);
                transform.position = new Vector3(0, 3, -2);
                break;
            case "FreeCam":    
                FreeCam();
                break;
            default:
                break;
        }
        transform.position = playerTransform.position;

    }

    void MyInput() {

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Keypad1)) {
            cameraMode = "Iso45Ortho";
            mainCamera.orthographic = true;
            mainCamera.transform.localPosition = new Vector3(0, 0, -10);
            mainCamera.transform.localRotation = Quaternion.identity;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Keypad2)) {
            cameraMode = "Iso70Per"; 
            mainCamera.orthographic = false;
            mainCamera.transform.localPosition = new Vector3(0, 0, -10);
            mainCamera.transform.localRotation = Quaternion.identity;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Keypad3)) {
            cameraMode = "FreeCam";
            mainCamera.transform.localPosition = camOrigin;
            mainCamera.orthographic = false;
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Keypad4)) {
            cameraMode = "Iso45";
        }
    }

    private void FreeCam() {       
        float xInput = 0f;
        float yInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))        xInput = -1f; //Left
        else if (Input.GetKey(KeyCode.RightArrow))  xInput = 1f;  //Right
        if (Input.GetKey(KeyCode.UpArrow))          yInput = 1f;  //Up      
        else if (Input.GetKey(KeyCode.DownArrow))   yInput = -1f; //Down
        
        mainCamera.transform.LookAt(playerTransform);
        mainCamera.transform.Translate(new Vector3(xInput, yInput, 0) * camSpeed);
    }

}
