using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ChangeNetManClass : NetworkBehaviour
{
    int setvalue = 500;
    public UnityEngine.UI.Dropdown classSelector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    //[Client]
    void Update()
    {
        PlayerPrefs.SetInt("PlayerClass", classSelector.value);
        SetClassValue(0, classSelector.value);
        /*if (GameObject.Find("NetworkManager").GetComponent<MMONetworkManager>().SetPlayer2Class == false)
        {
            //GameObject.Find("NetworkManager").GetComponent<MMONetworkManager>().Player1Class = classSelector.value;
        }
        else
        {

            
            while(setvalue >= 1)
            {
                //GameObject.Find("NetworkManager").GetComponent<MMONetworkManager>().Player2Class = classSelector.value;
                print("SETVALUE: " + setvalue);
                setvalue--;
            }
            
        }*/
        
    }

    [Command]
    public void SetClassValue(int Player1, int classvalue)
    {
        if(Player1 == 0)
        {
            GameObject.Find("NetworkManager").GetComponent<MMONetworkManager>().Player1Class = classvalue;
        }
        else
        {
            GameObject.Find("NetworkManager").GetComponent<MMONetworkManager>().Player2Class = classvalue;
        }
    }
}
