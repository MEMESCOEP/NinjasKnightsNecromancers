using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public bool PlayingSound = false;
    public Sprite Door_Locked;
    public Sprite Door_Open;
    public bool IsLocked = false;
    public Transform TPDestination;
    public Image img;
    public UnityEngine.UI.Text HintText;
    public AudioClip Door_Open_Sound;
    public AudioClip Door_Close_Sound;
    //public GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        //Canvas.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocked)
        {
            
            
            this.GetComponent<SpriteRenderer>().sprite = Door_Locked;
        }
        else
        {
            
            this.GetComponent<SpriteRenderer>().sprite = Door_Open;
        }
    }

    public void SwitchDoorState(bool islocked)
    {
        IsLocked = islocked;
        if (islocked)
        {
            
            this.GetComponent<AudioSource>().clip = Door_Close_Sound;
            this.GetComponent<AudioSource>().Play();


        }
        else
        {
            this.GetComponent<AudioSource>().clip = Door_Open_Sound;
            this.GetComponent<AudioSource>().Play();
        }
    }


    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 0.5f; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 0.5f; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i + 0.5f);
                yield return null;
            }
        }

    }

    IEnumerator FadeIn(int seconds, GameObject gobject)
    {
        gobject.GetComponent<HeroKnightCampaign>().CanMove = false;
        yield return new WaitForSeconds(seconds);
        gobject.transform.position = TPDestination.position;
        for (float i = 0; i <= 1f; i += Time.deltaTime)
        {
            
            
            yield return null;
        }
        gobject.GetComponent<HeroKnightCampaign>().CanMove = true;
        StartCoroutine(FadeImage(true));
        

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!IsLocked)
        {
            if(other.transform.tag == "Player" && Input.GetKeyDown(KeyCode.R))
            {

                StartCoroutine(FadeImage(false));
                StartCoroutine(FadeIn(1, other.gameObject));
                
            }



        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            HintText.text = "The door is locked. It seems to be linked to some kind of switch...";
            HintText.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        HintText.enabled = false;
    }
}
