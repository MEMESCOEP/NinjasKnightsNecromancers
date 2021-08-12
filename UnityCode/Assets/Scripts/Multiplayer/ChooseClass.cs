using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseClass : MonoBehaviour
{
    public Dropdown ClassChooserDropdown;

    // Start is called before the first frame update
    void Start()
    {
        ClassChooserDropdown = this.GetComponent<Dropdown>();
        try
        {
            ClassChooserDropdown.value = PlayerPrefs.GetInt("PlayerClass");
        }
        catch
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("PlayerClass", ClassChooserDropdown.value);
    }
}
