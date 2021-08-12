using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTextPosition : MonoBehaviour
{

    public Transform TP1;
    public Transform TP2;
    public GameObject MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MainCamera.GetComponent<CameraController>().Players.Length >= 2)
        {
            TP1.position = new Vector3(MainCamera.GetComponent<CameraController>().Players[0].transform.position.x, MainCamera.GetComponent<CameraController>().Players[0].transform.position.y + 0.5f, 0);
            TP2.position = new Vector3(MainCamera.GetComponent<CameraController>().Players[1].transform.position.x, MainCamera.GetComponent<CameraController>().Players[1].transform.position.y + 0.5f, 0);
        }
        
    }
}
