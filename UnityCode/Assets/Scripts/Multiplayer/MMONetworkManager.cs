using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public struct CreateMMOCharacterMessage : NetworkMessage
{
    public plclass PlayerClass;
    
}

public enum plclass
{
    ninja,
    knight,
    necromancer
}

public class MMONetworkManager : NetworkManager
{

    public bool SetPlayer2Class = false;
    public bool SpawnPlayer1 = true;
    public bool SpawnPlayer2 = false;
    public bool ClassSelected = false;
    public UnityEngine.UI.Dropdown classselect;
    public int Player1Class = 0;
    public int Player2Class = 0;
    public GameObject NinjaPrefab;
    public GameObject KnightPrefab;
    public GameObject NecromancerPrefab;
    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<CreateMMOCharacterMessage>(OnCreateCharacter);
    }

    public void Start()
    {
        this.GetComponent<NetworkManagerHUD>().showGUI = false;
    }
    

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        //int PlClass = 0;

        

        // you can send the message here, or wherever else you want
        CreateMMOCharacterMessage characterMessage = new CreateMMOCharacterMessage
        {
            PlayerClass = plclass.knight,
            //name = "Joe Gaba Gaba",
            //hairColor = Color.red,
            //eyeColor = Color.green
        };

        if (PlayerPrefs.GetInt("PlayerClass") == 0)
        {
            characterMessage.PlayerClass = plclass.ninja;
        }
        if (PlayerPrefs.GetInt("PlayerClass") == 1)
        {
            characterMessage.PlayerClass = plclass.knight;
        }
        if (PlayerPrefs.GetInt("PlayerClass") == 2)
        {
            characterMessage.PlayerClass = plclass.necromancer;
        }


        conn.Send(characterMessage);
    }

    public void ChangePlayerClass()
    {
        
        Player1Class = classselect.value;
    }
    
    public void CreatePlayer()
    {
        ClassSelected = true;

        SetPlayer2Class = true;
        classselect.gameObject.SetActive(false);
        this.GetComponent<NetworkManagerHUD>().showGUI = true;
    }
    public void SpawnPlayer(NetworkConnection conn)
    {
        if (Player1Class == 0)
        {
            playerPrefab = NinjaPrefab;
        }
        if (Player1Class == 1)
        {
            playerPrefab = KnightPrefab;
        }
        if (Player1Class == 2)
        {
            playerPrefab = NecromancerPrefab;
        }

        GameObject gameobject = Instantiate(playerPrefab);

        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        //Player player = gameobject.GetComponent();

        //player.name = message.name;
        //player.PlayerClass = message.PlayerClass;

        // call this to use this gameobject as the primary controller
        NetworkServer.AddPlayerForConnection(conn, gameobject);
    }

    IEnumerator WaitForDuration(int duration, Action onExecute)
    {
        yield return new WaitForSeconds(duration);
        onExecute?.Invoke();
    }
    private void Update()
    {
        if (SpawnPlayer1 && !SpawnPlayer2)
        {
            //SpawnPlayer1 = false;
            if (Player1Class == 0)
            {
                playerPrefab = NinjaPrefab;
            }
            if (Player1Class == 1)
            {
                playerPrefab = KnightPrefab;
            }
            if (Player1Class == 2)
            {
                playerPrefab = NecromancerPrefab;
            }
        }else if(!SpawnPlayer1 && SpawnPlayer2)
        {
            if (Player2Class == 0)
            {
                playerPrefab = NinjaPrefab;
            }
            if (Player2Class == 1)
            {
                playerPrefab = KnightPrefab;
            }
            if (Player2Class == 2)
            {
                playerPrefab = NecromancerPrefab;
            }
        }
    }

    void OnCreateCharacter(NetworkConnection conn, CreateMMOCharacterMessage message)
    {

        if (message.PlayerClass == plclass.ninja)
        {
            GameObject gameobject = Instantiate(NinjaPrefab);
            //Player2 player = gameobject.GetComponent<Player2>();
            NetworkServer.AddPlayerForConnection(conn, gameobject);
        }
        else if (message.PlayerClass == plclass.knight)
        {
            GameObject gameobject = Instantiate(KnightPrefab);
            //Player2 player = gameobject.GetComponent<Player2>();
            NetworkServer.AddPlayerForConnection(conn, gameobject);
        }
        else if (message.PlayerClass == plclass.necromancer)
        {
            GameObject gameobject = Instantiate(NecromancerPrefab);
            //Player2 player = gameobject.GetComponent<Player2>();
            NetworkServer.AddPlayerForConnection(conn, gameobject);
        }
        /*float time = 10f;

        while (time >= 0f)
        {
            print("TIME: " + time);
            time -= Time.deltaTime;
        }
        Debug.LogError("I was printed 2 seconds later...");
        if (SpawnPlayer2 && !SpawnPlayer1)
        {
            SpawnPlayer2 = false;
            if (Player2Class == 0)
            {
                playerPrefab = NinjaPrefab;
            }
            if (Player2Class == 1)
            {
                playerPrefab = KnightPrefab;
            }
            if (Player2Class == 2)
            {
                playerPrefab = NecromancerPrefab;
            }

            GameObject gameobject = Instantiate(playerPrefab);

            // Apply data from the message however appropriate for your game
            // Typically Player would be a component you write with syncvars or properties
            //Player player = gameobject.GetComponent();

            //player.name = message.name;
            //player.PlayerClass = message.PlayerClass;

            // call this to use this gameobject as the primary controller
            NetworkServer.AddPlayerForConnection(conn, gameobject);
            //classselect.enabled = false;
            ClassSelected = false;
        }
        if (SpawnPlayer1)
        {
            SpawnPlayer1 = false;
            if (Player1Class == 0)
            {
                playerPrefab = NinjaPrefab;
            }
            if (Player1Class == 1)
            {
                playerPrefab = KnightPrefab;
            }
            if (Player1Class == 2)
            {
                playerPrefab = NecromancerPrefab;
            }

            GameObject gameobject = Instantiate(playerPrefab);

            // Apply data from the message however appropriate for your game
            // Typically Player would be a component you write with syncvars or properties
            //Player player = gameobject.GetComponent();

            //player.name = message.name;
            //player.PlayerClass = message.PlayerClass;

            // call this to use this gameobject as the primary controller
            NetworkServer.AddPlayerForConnection(conn, gameobject);
            //classselect.enabled = false;
            ClassSelected = false;
        }


        WaitForDuration(2, () =>
        {

            

        });
        */
        /*while(ClassSelected == false)
        {
            System.Threading.Thread.Sleep(10);
        }*/
        // playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example

        
        
    }
}
