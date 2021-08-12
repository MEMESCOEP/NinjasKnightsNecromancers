using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public Transform spawnPoint;
    public string Difficulty = "";
    public string classname = "";

    public GameObject Ninja;
    public GameObject Knight;
    public GameObject Necromancer;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Difficulty") == 0)
        {
            Difficulty = "Easy";
        }
        if (PlayerPrefs.GetInt("Difficulty") == 1)
        {
            Difficulty = "Normal";
        }
        if (PlayerPrefs.GetInt("Difficulty") == 2)
        {
            Difficulty = "Hard";
        }


        if (PlayerPrefs.GetInt("SingleplayerClass") == 0)
        {
            classname = "Ninja";
            GameObject gm = (GameObject)Instantiate(Ninja, spawnPoint);
            gm.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            gm.GetComponent<HeroKnight>().enabled = false;
            gm.GetComponent<HeroKnightCampaign>().enabled = true;
        }
        if (PlayerPrefs.GetInt("SingleplayerClass") == 1)
        {
            classname = "Knight";
            GameObject gm = (GameObject)Instantiate(Knight, spawnPoint);
            gm.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            gm.GetComponent<HeroKnight>().enabled = false;
            gm.GetComponent<HeroKnightCampaign>().enabled = true;
        }
        if (PlayerPrefs.GetInt("SingleplayerClass") == 2)
        {
            classname = "Necromancer";
            GameObject gm = (GameObject)Instantiate(Necromancer, spawnPoint);
            gm.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            gm.GetComponent<HeroKnight>().enabled = false;
            gm.GetComponent<HeroKnightCampaign>().enabled = true;
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
