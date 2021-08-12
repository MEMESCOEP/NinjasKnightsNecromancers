using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGamePlayerSettings : MonoBehaviour
{

    public UnityEngine.UI.Dropdown classselect;
    public UnityEngine.UI.Dropdown difficulty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("SingleplayerClass", classselect.value);
        PlayerPrefs.SetInt("Difficulty", difficulty.value);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("clvl0");
    }
}
