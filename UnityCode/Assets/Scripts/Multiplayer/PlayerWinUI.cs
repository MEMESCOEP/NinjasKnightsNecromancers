using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWinUI : MonoBehaviour
{

    public GameObject PlayerWinUIgm;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("MainCamera").GetComponent<CameraController>().GameStarted == true)
        {
           
                if (GameObject.Find("MainCamera").GetComponent<CameraController>().Players[0].GetComponent<HeroKnight>().m_isDead && !GameObject.Find("MainCamera").GetComponent<CameraController>().Players[1].GetComponent<HeroKnight>().m_isDead)
                {
                    PlayerWinUIgm.SetActive(true);
                    PlayerWinUIgm.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Player 2 Wins!";
                    GameObject.Find("MainCamera").GetComponent<CameraController>().DisablePlayerMovement = true;
                }
                if (GameObject.Find("MainCamera").GetComponent<CameraController>().Players[1].GetComponent<HeroKnight>().m_isDead && !GameObject.Find("MainCamera").GetComponent<CameraController>().Players[0].GetComponent<HeroKnight>().m_isDead)
                {
                    PlayerWinUIgm.SetActive(true);
                    PlayerWinUIgm.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Player 1 Wins!";
                    GameObject.Find("MainCamera").GetComponent<CameraController>().DisablePlayerMovement = true;
                }
            
            
        }

            
    }
}
