using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    public Sprite Activated;
    public Sprite Deactivated;
    public GameObject ObjectTrigger;
    public GameObject DoorCloseTrigger;

    public bool IsActivated = true;
    public bool IsADoor = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(IsActivated && IsADoor)
        {
            if(ObjectTrigger != null)
            {
                this.GetComponent<BoxCollider2D>().enabled = true;
                //this.GetComponent<SpriteRenderer>().FlipX = true;
                this.GetComponent<SpriteRenderer>().sprite = Activated;
            }
            else
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
                this.GetComponent<SpriteRenderer>().sprite = Deactivated;
            }
            
        }
        else
        {
            if (IsActivated && !IsADoor)
            {
                this.GetComponent<BoxCollider2D>().enabled = true;
                //this.GetComponent<SpriteRenderer>().FlipX = true;
                this.GetComponent<SpriteRenderer>().sprite = Activated;
            }
            else
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
                this.GetComponent<SpriteRenderer>().sprite = Deactivated;
            }
        }
        
    }



    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player" && IsActivated)
        {
            other.gameObject.GetComponent<HeroKnightCampaign>().PlayerHealth -= 1;
        }
        else
        {
            if (IsADoor)
            {
                //IsActivated = true;
            }
            
        }


    }


}
