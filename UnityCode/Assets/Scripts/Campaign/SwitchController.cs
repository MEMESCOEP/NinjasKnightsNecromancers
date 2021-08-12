using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ActionObject;

    public Sprite switch_OFF;
    public Sprite switch_ON;

    public bool switchON = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (switchON)
        {
            //this.GetComponent<SpriteRenderer>().FlipX = true;
            this.GetComponent<SpriteRenderer>().sprite = switch_ON;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = switch_OFF;
        }
    }




    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                this.GetComponent<AudioSource>().Play();
                switchON = !switchON;
                if(ActionObject.transform.tag == "door")
                {

                    ActionObject.GetComponent<DoorController>().SwitchDoorState(!switchON);
                }
                if (ActionObject.transform.tag == "spiketrap")
                {

                    ActionObject.GetComponent<SpikesController>().IsActivated = !switchON;
                }
                //print("lol");
            }
        }
    }
}
