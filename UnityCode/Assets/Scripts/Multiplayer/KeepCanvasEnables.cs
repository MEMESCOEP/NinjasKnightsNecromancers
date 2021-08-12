using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepCanvasEnables : MonoBehaviour
{

    public GameObject MainCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MainCanvas.SetActive(true);
    }
}
