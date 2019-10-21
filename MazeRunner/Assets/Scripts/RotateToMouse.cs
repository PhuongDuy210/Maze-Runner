using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RotateToMouse : MonoBehaviour
{
    public float rotateSpeed;

    private Quaternion prefFrameRotation;
    private Quaternion deltaRotation;
    public Text debug;
    private Camera mainCam;
    private Vector3 firstFingerPos = Vector3.zero;
    private Vector3 secondFingerPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.isEditor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Application.platform == RuntimePlatform.Android)
        {
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.isEditor)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            float rotX = Input.GetAxis("Mouse X");
            float rotY = Input.GetAxis("Mouse Y");
            RotateTo(rotX, rotY, rotateSpeed);
            if (Input.GetKey(KeyCode.Escape))
            {
                if (Cursor.visible == false)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            /*Quaternion gyroQua = Input.gyro.attitude;
            debug.text = "x: " + gyroQua.x + " y: " + gyroQua.y + " z: " + gyroQua.z + " w: " + gyroQua.w;
            deltaRotation = prefFrameRotation * gyroQua;
            transform.Rotate(deltaRotation.x, 0, deltaRotation.z);
            prefFrameRotation = gyroQua;*/
            if ((Input.touchCount > 0) && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector3 vector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.nearClipPlane);
                firstFingerPos = mainCam.ScreenToWorldPoint(vector);
            }
            if ((Input.touchCount > 0) && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector3 vector = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.nearClipPlane);
                secondFingerPos = mainCam.ScreenToWorldPoint(vector);
                Vector3 distance;
                distance = (secondFingerPos - firstFingerPos).normalized;
                RotateTo(distance.x, distance.z, 1f);
            }
        } 
    }


    private void RotateTo(float rotX, float rotY, float speed)
    {
        transform.Rotate(rotY * speed, 0f, -rotX * speed);
        if (transform.rotation.y != 0)
            transform.rotation = new Quaternion(transform.rotation.x, 0f, transform.rotation.z, transform.rotation.w);
    }
}
