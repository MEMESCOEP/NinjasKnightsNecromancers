using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerClass : NetworkBehaviour
{

    public int Class = 0;

    public string ClassName = "";
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        //gameObject.name = "Local";
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            Class = PlayerPrefs.GetInt("PlayerClass");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Class == 0)
        {
            ClassName = "Ninja";
        }else if(Class == 1)
        {
            ClassName = "Knight";
        }
        else
        {
            ClassName = "Necromancer";
        }
    }
}
