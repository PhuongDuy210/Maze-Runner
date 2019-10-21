using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] camPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCameraPosition(int input)
    {
        transform.position = camPos[input].transform.position;
        transform.rotation = camPos[input].transform.rotation;
    }
}
