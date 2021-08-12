using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetValueToPlPrefs : MonoBehaviour
{

    public GameObject ClassUI;

    // Start is called before the first frame update
    void Start()
    {
        ClassUI.GetComponent<UnityEngine.UI.Dropdown>().value = PlayerPrefs.GetInt("PlayerClass");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
