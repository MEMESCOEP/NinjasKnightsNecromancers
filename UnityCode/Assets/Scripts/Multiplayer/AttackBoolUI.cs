using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoolUI : MonoBehaviour
{
    public GameObject PlayerManager;
    public GameObject AT1;
    public GameObject AT2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AT1.GetComponent<UnityEngine.UI.Text>().text = "Player 1 Attacking: " + PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking;
        AT2.GetComponent<UnityEngine.UI.Text>().text = "Player 2 Attacking: " + PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking;
    }
}
