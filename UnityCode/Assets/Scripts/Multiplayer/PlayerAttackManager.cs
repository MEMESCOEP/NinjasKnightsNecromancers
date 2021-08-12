using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerAttackManager : NetworkBehaviour
{
    [SyncVar]
    public bool Player1Attacking = false;
    [SyncVar]
    public bool Player2Attacking = false;
    [SyncVar]
    public bool Player1Assigned = false;
    [SyncVar]
    public int P1Health;
    [SyncVar]
    public int P2Health;

    public GameObject Player1;
    public GameObject Player2;

    public UnityEngine.UI.Slider p1healthslider;
    public UnityEngine.UI.Slider p2healthslider;



    [Command]
    public void CmdAssignBaseAuthority(GameObject theGameObject)
    {
        theGameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }
    [Command]
    public void CmdChangePlayerAttackVariable(bool IsPlayer1, bool mode)
    {
        //Player1Attacking = true;

    }

    // Start is called before the first frame update
    void Start()
    {
        CmdAssignBaseAuthority(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
        p1healthslider.value = P1Health;
        p2healthslider.value = P2Health;
    }
}
