using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDoorTrigger : MonoBehaviour
{
    public bool isActivated = false;
    public bool isSingleUse = true;
    public GameObject[] SpikeDoors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isSingleUse)
        {
            if (other.transform.tag == "Player" && !isActivated)
            {
                foreach(GameObject spiketrap in SpikeDoors)
                {
                    spiketrap.GetComponent<SpikesController>().IsActivated = true;
                }
                isActivated = true;
            }
        }
        else
        {
            if (other.transform.tag == "Player")
            {

            }
        }
        
    }
}
