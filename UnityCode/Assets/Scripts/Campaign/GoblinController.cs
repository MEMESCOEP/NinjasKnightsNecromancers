using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    public float speed = 10f;
    public bool SetTrigger = false;
    public bool DoAction = true;
    public bool PlayerInRange = false;
    public bool CanAttack = false;
    public Animator anim;
    public Transform player;
    public Vector2 playerXAxis;
    public Vector2 relativePoint;

    // Start is called before the first frame update
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerXAxis = new Vector2(player.position.x, transform.position.y);
        float dist = Vector3.Distance(this.transform.position, player.position);
        if(dist <= 8.5f && CanAttack)
        {
            relativePoint = transform.InverseTransformPoint(player.position);
            if (relativePoint.x < 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
                Debug.Log("Right");
            }
            if (relativePoint.x > 0f && Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.y))
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
                Debug.Log("Left");
            }
            if (relativePoint.y > 0 && Mathf.Abs(relativePoint.x) < Mathf.Abs(relativePoint.y))
            {
                Debug.Log("Under");
            }
            if (relativePoint.y < 0 && Mathf.Abs(relativePoint.x) < Mathf.Abs(relativePoint.y))
            {
                Debug.Log("Above");
            }
            //transform.LookAt(player);
            PlayerInRange = true;
        }
        else
        {
            PlayerInRange = false;
        }
        if (PlayerInRange)
        {
            anim.SetBool("IsIdle", false);
            anim.SetBool("IsRunning", true);
            chase();
        }
        else
        {
            anim.SetBool("IsIdle", true);
            anim.SetBool("IsRunning", false);
        }
        
        if (SetTrigger && DoAction)
        {
            anim.SetBool("IsIdle", false);
            anim.SetTrigger("Attack");
            DoAction = false;
            //SetTrigger = false;
        }
        else if(!SetTrigger && DoAction)
        {
            //anim.SetBool("IsIdle", true);
            anim.ResetTrigger("Attack");
        }
    }

    

    void chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerXAxis, speed * Time.deltaTime);
    }

    public void AnimationDone()
    {
        SetTrigger = false;
        DoAction = true;
    }
}
