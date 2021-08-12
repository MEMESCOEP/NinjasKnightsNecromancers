using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RotateGameObject : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        transform.Rotate(0,0, -1200 * Time.deltaTime);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "GroundCollider" || collision.gameObject.name == "props" || collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.name == "GroundCollider" || collision.gameObject.name == "props" || collision.gameObject.tag != "Player")
            {
                Destroy(this.gameObject);
            }
            //identity.AssignClientAuthority(conn);
            print("COLLISION NAME: " + collision.gameObject.name);
            //collision.gameObject.GetComponent<HeroKnight>().ServerTakeDamage(1);
            try
            {
                //collision.gameObject.GetComponent<HeroKnight>().CmdServerTakeDamage(1);
                //collision.gameObject.GetComponent<Necromancer>().TakeDamage(1);
                Destroy(this.gameObject);
            }
            catch
            {

            }
            Destroy(this.gameObject);

        }
        
    }
}
