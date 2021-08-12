using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject[] Players;
    public int[] PlayerClasses;
    public Vector2 Player1Pos;
    public Vector2 Player2Pos;
    public Vector3 CamPos;
    public bool DisablePlayerMovement;
    public bool GameStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Players.Length >= 2)
        {
            GameStarted = true;
        }
        try
        {
            CamPos = new Vector3(Player1Pos.x + Player2Pos.x, Player1Pos.y + Player2Pos.y, -48) / 2;
            Players = GameObject.FindGameObjectsWithTag("Player");
            PlayerClasses[0] = Players[0].GetComponent<PlayerClass>().Class;
            PlayerClasses[1] = Players[1].GetComponent<PlayerClass>().Class;

            Player1Pos = Players[0].transform.position;
            Player2Pos = Players[1].transform.position;

            foreach (GameObject player in Players)
            {

                //Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
            }
            this.transform.position = CamPos;
        }
        catch
        {

        }
        
    }
}
