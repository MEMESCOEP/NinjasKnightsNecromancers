using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SetPlayerPrefabToSpawn : MonoBehaviour
{

    public GameObject NinjaPrefab;
    public GameObject KnightPrefab;
    public GameObject NecromancerPrefab;
    public bool isPlayer1Set = false;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("PlayerClass") == 0)
        {
            //Ninja
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().playerPrefab = NinjaPrefab;
            isPlayer1Set = true;
        }
        else if(PlayerPrefs.GetInt("PlayerClass") == 1)
        {
            //Knight
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().playerPrefab = KnightPrefab;
            isPlayer1Set = true;
        }
        else
        {
            //Necromancer
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().playerPrefab = NecromancerPrefab;
            isPlayer1Set = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayer1Set == true)
        {
            
            if (GameObject.Find("Local").GetComponent<GetPlayerClass>().PlayerClass == 0)
            {
                //Ninja
                GameObject.Find("NetworkManager").GetComponent<NetworkManager>().playerPrefab = NinjaPrefab;
            }
            else if (GameObject.Find("Local").GetComponent<GetPlayerClass>().PlayerClass == 1)
            {
                //Knight
                GameObject.Find("NetworkManager").GetComponent<NetworkManager>().playerPrefab = KnightPrefab;
            }
            else
            {
                //Necromancer
                GameObject.Find("NetworkManager").GetComponent<NetworkManager>().playerPrefab = NecromancerPrefab;
            }
        }
        
    }
}
