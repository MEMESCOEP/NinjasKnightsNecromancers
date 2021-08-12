using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{

    public UnityEngine.UI.Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        if (dropdown.value == 0)
        {
            SceneManager.LoadScene("Level1");
        }
        else
        {
            SceneManager.LoadScene("Level3");
        }
    }
}
