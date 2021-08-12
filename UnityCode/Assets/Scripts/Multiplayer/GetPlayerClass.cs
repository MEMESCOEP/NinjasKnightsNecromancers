using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GetPlayerClass : NetworkBehaviour
{

    public GameObject NinjaPrefab;
    public GameObject KnightPrefab;
    public GameObject NecromancerPrefab;
    

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        gameObject.name = "Local";
    }
    public string PlayerClassName = "";
    public int PlayerClass = 0;

    // Start is called before the first frame update


    public GameObject SpawnKnight(SpawnMessage msg)
    {
        return Instantiate(KnightPrefab, msg.position, msg.rotation);
    }

    public void DespawnKnight(GameObject spawned)
    {
        Destroy(spawned);
    }
    void Start()
    {
        if (isLocalPlayer)
        {
            PlayerClass = PlayerPrefs.GetInt("PlayerClass");
            if (PlayerClass == 0)
            {

                //Ninja
            }
            if (PlayerClass == 1)
            {
                //Knight
                GameObject netman = GameObject.Find("NetworkManager");
                GameObject prefab = (GameObject)NetworkManager.Instantiate(KnightPrefab, this.transform);
                //ClientScene.RegisterPrefab(KnightPrefab, SpawnKnight, DespawnKnight);
                NetworkServer.Spawn(prefab);
                prefab.transform.parent = null;
            }
            if (PlayerClass == 2)
            {
                //Necromancer
                GameObject prefab = (GameObject)NetworkManager.Instantiate(NecromancerPrefab, this.transform);
                prefab.transform.parent = null;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
