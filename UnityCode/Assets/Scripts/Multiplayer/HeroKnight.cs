using UnityEngine;
using UnityEngine.UI;
using System.Drawing;
using System.Collections;
using Mirror;


public class HeroKnight : NetworkBehaviour {

    public bool isSingleplayer = false;

    [SyncVar]
    public bool HitWithNinjaStar = false;

    public bool IsNecromancer = false;
    
    public GameObject NinjaPrefab;
    public GameObject KnightPrefab;
    public GameObject NecromancerPrefab;

    public GameObject ClassPrefab;

    public GameObject NinjaStar;
    public GameObject PlasmaBall;

    public Transform ObjectSpawnTransform;


    [SerializeField]
    public bool IstheLocalPlayer;
    public bool CanRoll = true;

    public bool StartedFadeIn = false;

    public bool DebugMode = false;

    public bool CanMove = true;

    public bool CanDoubleJump = true;

    public bool CanTakeDamage = true;

    [SerializeField] bool isPlayer1 = false;

    public GameObject PlayerManager;

    //[SyncVar]
    public int PlayerHealth = 0;

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;   
    [SerializeField] GameObject m_slideDust;
    [SyncVar]
    public bool m_isDead = false;

    public Animator             m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private Sensor_HeroKnight   m_wallSensorR1;
    private Sensor_HeroKnight   m_wallSensorR2;
    private Sensor_HeroKnight   m_wallSensorL1;
    private Sensor_HeroKnight   m_wallSensorL2;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    [SerializeField]
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;



    [Command]

    public void AskToSetP2Health(int CurrHealth)
    {
        PlayerManager.GetComponent<PlayerAttackManager>().P2Health = CurrHealth;
        PlayerManager.GetComponent<PlayerAttackManager>().p2healthslider.value = CurrHealth;
    }


    [Command]
    public void SetPlayerAttack(bool AttackState)
    {
        PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking = AttackState;
        PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking = AttackState;
    }


    [Command]
    public void AskToSetP2Attack(bool var, int CurrentHealth)
    {
        if (!isPlayer1)
        {

           
            PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking = var;

        }
        else
        {
            PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking = var;
        }
    }

    [Command]
    public void CmdAssignBaseAuthority(GameObject theGameObject)
    {
        theGameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }
    public override void OnStartLocalPlayer()
    {        
        base.OnStartLocalPlayer();
        localPlayer = this;
        gameObject.name = "Local";
    }
    static public HeroKnight localPlayer;

    // Use this for initialization
    void Start ()
    {
        //isLocalPlayer = true;
        IstheLocalPlayer = isLocalPlayer;
        try
        {
            
            if (PlayerPrefs.GetInt("PlayerClass") == 0)
            {
                ClassPrefab = NinjaPrefab;
                //GameObject.Find("NetworkManager").GetComponent<NetworkManager>().ReplacePlayer(this.connectionToServer, NinjaPrefab);
            }
            if (PlayerPrefs.GetInt("PlayerClass") == 1)
            {
                ClassPrefab = KnightPrefab;
                //GameObject.Find("NetworkManager").GetComponent<NetworkManager>().ReplacePlayer(this.connectionToServer, KnightPrefab);
            }
            if (PlayerPrefs.GetInt("PlayerClass") == 2)
            {
                ClassPrefab = NecromancerPrefab;
                
                //GameObject.Find("NetworkManager").GetComponent<NetworkManager>().ReplacePlayer(this.connectionToServer, NecromancerPrefab);
            }

            /*if (ClassPrefab != GameObject.Find("NetworkManager").GetComponent<NetworkManager>().playerPrefab)
            {
                //Mirror.NetworkClient.Disconnect();
            }*/

            GameObject.Find("NetworkManager").GetComponent<PlayerClassManager>().PlayerClass = PlayerPrefs.GetInt("PlayerClass");
            if(GameObject.Find("NetworkManager").GetComponent<PlayerClassManager>().PlayerClass == PlayerPrefs.GetInt("PlayerClass"))
            {

            }
            else
            {
                Mirror.NetworkClient.Disconnect();
            }
        }
        catch
        {

        }

        
        //GameObject.Find("NetworkManager").GetComponent<NetworkManager>().OnServerAddPlayer(connectionToServer);


        CmdAssignBaseAuthority(gameObject);
        PlayerHealth = 9;
        if (this.GetComponent<NetworkIdentity>().isLocalPlayer)
        {

            PlayerHealth = 9;
            m_animator = GetComponent<Animator>();
            m_body2d = GetComponent<Rigidbody2D>();
            m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
            m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
            m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
            m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
            m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();

        }
        if (!isSingleplayer)
        {


            PlayerManager = GameObject.Find("PlayerManager");
            if (PlayerManager.GetComponent<PlayerAttackManager>().Player1Assigned == false)
            {
                PlayerManager.GetComponent<PlayerAttackManager>().Player1Assigned = true;
                PlayerManager.GetComponent<PlayerAttackManager>().Player1 = this.gameObject;
                isPlayer1 = true;
            }
            else
            {
                PlayerManager.GetComponent<PlayerAttackManager>().Player2 = this.gameObject;
            }

        }
        m_animator = this.GetComponent<Animator>();
        StartCoroutine(Countdown2(1));
        /*if (PlayerManager.GetComponent<PlayerAttackManager>().Player1 == null && PlayerManager.GetComponent<PlayerAttackManager>().Player1Assigned && !isPlayer1)
        {
            print("No Player 1");
            PlayerManager.GetComponent<PlayerAttackManager>().Player1Assigned = false;
            PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking = false;
            PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking = false;
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopClient();
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopHost();
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StartHost();
        }*/
        
    }

    IEnumerator Countdown2(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
       
    }

    IEnumerator FadeIn(float FadeRate)
    {
        float targetAlpha = 1.0f;
        UnityEngine.Color curColor = transform.GetChild(8).GetChild(0).GetChild(0).GetComponent<Image>().color;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
        {
            Debug.Log(transform.GetChild(8).GetChild(0).GetChild(0).GetComponent<Image>().material.color.a);
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, FadeRate * Time.deltaTime);
            transform.GetChild(8).GetChild(0).GetChild(0).GetComponent<Image>().color = curColor;
            yield return null;
        }
    }

    /*public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
    }*/




    // Update is called once per frame


    [ClientRpc]
    void RpcSyncVarWithClients(int varToSync)
    {
        PlayerHealth -= varToSync;
    }


    [Command]
    public void CmdServerTakeDamage(int Value)
    {
        PlayerHealth -= Value;
    }

    void Update ()
    {
        
        if (HitWithNinjaStar && !m_isDead && CanTakeDamage)
        {
            if (PlayerHealth > 0)
            {
                PlayerHealth -= 1;
                HitWithNinjaStar = false;
                CanTakeDamage = false;
                StartCoroutine(Countdown(1));
                //ServerTakeDamage(1);
                //AskToSetP2Health(PlayerHealth);
                m_animator.SetTrigger("Hurt");
            }
            else
            {
                m_isDead = true;
                m_animator.SetTrigger("Death");
            }
            
            /*if (!isPlayer1)
            {

                //RpcSyncVarWithClients(1);
                //PlayerHealth -= 1;
                //ServerTakeDamage(1);
                //AskToSetP2Health(PlayerHealth);
                m_animator.SetTrigger("Hurt");
                //print("Player Attacked!");
                //SetPlayerAttack(false);

                //CanTakeDamage = false;
                //StartCoroutine(Countdown(1));
                HitWithNinjaStar = false;
            }
            else
            {
                RpcSyncVarWithClients(1);
                CmdServerTakeDamage(1);
                PlayerHealth -= 1;
                //AskToSetP2Health(PlayerHealth);
                m_animator.SetTrigger("Hurt");
                //print("Player Attacked!");
                //SetPlayerAttack(false);

                //CanTakeDamage = false;
                //StartCoroutine(Countdown(1));
                
                HitWithNinjaStar = false;
            }*/

        }
        print("Player1 is local player: " + isLocalPlayer);
        if (!StartedFadeIn)
        {
            StartCoroutine(FadeIn(3));
            StartedFadeIn = false;
        }
        if (isSingleplayer)
        {
            if (PlayerHealth > 0)
            {


                this.transform.GetChild(6).GetChild(1).GetComponent<UnityEngine.UI.Slider>().value = PlayerHealth;

                // Increase timer that controls attack combo
                m_timeSinceAttack += Time.deltaTime;

                //Check if character just landed on the ground
                if (!m_grounded && m_groundSensor.State())
                {
                    CanRoll = true;
                    CanDoubleJump = true;
                    m_grounded = true;
                    m_animator.SetBool("Grounded", m_grounded);
                }

                //Check if character just started falling
                if (m_grounded && !m_groundSensor.State())
                {
                    m_grounded = false;
                    m_animator.SetBool("Grounded", m_grounded);
                }

                // -- Handle input and movement --
                float inputX = Input.GetAxis("Horizontal");

                // Swap direction of sprite depending on walk direction
                if (inputX > 0 && m_animator.GetBool("WallSlide") == false && CanMove)
                {
                    Transform playerpos = this.transform;

                    //GetComponent<SpriteRenderer>().flipX = false;
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);
                    this.transform.GetChild(6).rotation = Quaternion.Euler(0, 0, 0);
                    m_facingDirection = 1;
                    /*if (playerpos == this.transform)
                    {
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.005f, this.transform.position.z);
                    }*/
                    if (m_body2d.velocity.x == 0)
                    {
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z);
                    }
                    // Player is moving.

                    var distanceBetween = Vector2.Distance(this.transform.position, GameObject.Find("Tilemap").transform.position);
                    //Debug.Log($"distanceBetween My Player and the stuck block is : {GameObject.Find("Tilemap").name} == {distanceBetween}");
                }

                else if (inputX < 0 && m_animator.GetBool("WallSlide") == false && CanMove)
                {
                    //GetComponent<SpriteRenderer>().flipX = true;
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);
                    this.transform.GetChild(6).rotation = Quaternion.Euler(0, 0, 0);
                    m_facingDirection = -1;
                    if (m_body2d.velocity.x == 0)
                    {
                        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z);
                    }
                }

                if (m_grounded && (m_wallSensorL1.transform.GetComponent<Sensor_HeroKnight>().m_ColCount > 0 || m_wallSensorL2.transform.GetComponent<Sensor_HeroKnight>().m_ColCount > 0 || m_wallSensorR1.transform.GetComponent<Sensor_HeroKnight>().m_ColCount > 0 || m_wallSensorR2.transform.GetComponent<Sensor_HeroKnight>().m_ColCount > 0))
                {
                    m_animator.SetBool("WallSlide", true);
                }
                else
                {
                    m_animator.SetBool("WallSlide", false);
                }

                if (m_facingDirection == 1)
                {
                    m_wallSensorL1.transform.GetComponent<CircleCollider2D>().enabled = false;
                    m_wallSensorL2.transform.GetComponent<CircleCollider2D>().enabled = false;
                    m_wallSensorR1.transform.GetComponent<CircleCollider2D>().enabled = true;
                    m_wallSensorR2.transform.GetComponent<CircleCollider2D>().enabled = true;
                }
                else
                {
                    m_wallSensorL1.transform.GetComponent<CircleCollider2D>().enabled = true;
                    m_wallSensorL2.transform.GetComponent<CircleCollider2D>().enabled = true;
                    m_wallSensorR1.transform.GetComponent<CircleCollider2D>().enabled = false;
                    m_wallSensorR2.transform.GetComponent<CircleCollider2D>().enabled = false;
                }

                // Move
                if (!m_rolling && CanMove)
                    m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

                //Set AirSpeed in animator
                m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

                // -- Handle Animations --
                //Wall Slide
                m_animator.SetBool("WallSlide", (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State()));

                //Death
                if (Input.GetKeyDown("e") && !m_rolling && DebugMode)
                {
                    PlayerHealth = 0;
                    m_animator.SetBool("noBlood", m_noBlood);
                    m_animator.SetTrigger("Death");
                }

                print(m_animator.GetBool("WallSlide"));

                if (m_animator.GetBool("WallSlide") == true)
                {

                    m_animator.SetBool("WallSlide", true);
                    GetComponent<SpriteRenderer>().flipX = false;
                }

                //Hurt
                else if (Input.GetKeyDown("q") && !m_rolling && DebugMode)
                {
                    PlayerHealth -= 1;
                    m_animator.SetTrigger("Hurt");
                }



                //Attack
                else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling && CanMove)
                {
                    m_currentAttack++;

                    // Loop back to one after third attack
                    if (m_currentAttack > 3)
                        m_currentAttack = 1;

                    // Reset Attack combo if time since last attack is too large
                    if (m_timeSinceAttack > 1.0f)
                        m_currentAttack = 1;

                    // Call one of three attack animations "Attack1", "Attack2", "Attack3"
                    m_animator.SetTrigger("Attack" + m_currentAttack);
                    m_animator.SetBool("IsAttacking", true);
                    if (isPlayer1)
                    {

                        PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking = true;
                        print("Player 1 Attacking: " + PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking);
                        //print(PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking);
                    }
                    else
                    {

                        //PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking = true;
                        AskToSetP2Attack(true, PlayerHealth);
                        print("Player 2 Attacking: " + PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking);
                        //print(PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking);
                    }
                    //m_animator.SetBool("IsAttacking", false);
                    // Reset timer
                    m_timeSinceAttack = 0.0f;
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    CmdSpawnProjectile(m_facingDirection);
                }

                if (Input.GetMouseButtonUp(0) && CanMove)
                {
                    if (isPlayer1)
                    {
                        PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking = false;
                    }
                    else
                    {
                        //PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking = false;
                        AskToSetP2Attack(false, PlayerHealth);
                    }
                }

                // Block
                else if (Input.GetMouseButtonDown(1) && !m_rolling)
                {
                    m_animator.SetTrigger("Block");
                    m_animator.SetBool("IdleBlock", true);
                    CanMove = false;
                    CanTakeDamage = false;
                }

                else if (Input.GetMouseButtonUp(1))
                {
                    m_animator.SetBool("IdleBlock", false);
                    CanMove = true;
                    CanTakeDamage = true;
                }


                // Roll
                else if (Input.GetKeyDown("left shift") && !m_rolling && m_animator.GetBool("WallSlide") == false && CanRoll && CanMove)
                {
                    m_rolling = true;
                    m_animator.SetTrigger("Roll");
                    m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
                }


                //Jump
                else if (Input.GetKeyDown("space") && m_grounded && !m_rolling && CanMove)
                {
                    m_animator.SetTrigger("Jump");
                    m_grounded = false;
                    m_animator.SetBool("Grounded", m_grounded);
                    m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                    m_groundSensor.Disable(0.1f);
                }

                else if (Input.GetKeyDown("space") && !m_grounded && CanDoubleJump && CanMove)
                {
                    CanRoll = false;
                    CanDoubleJump = false;
                    m_animator.SetTrigger("Jump");
                    m_grounded = false;
                    m_animator.SetBool("Grounded", m_grounded);
                    m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                    m_groundSensor.Disable(0.1f);
                }
                else if (Input.GetKeyDown("space") && !m_grounded && CanDoubleJump && CanMove)
                {

                    m_animator.SetTrigger("Jump");
                    m_grounded = false;
                    m_animator.SetBool("Grounded", m_grounded);
                    m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                    m_groundSensor.Disable(0.1f);
                }

                //Run
                else if (Mathf.Abs(inputX) > Mathf.Epsilon)
                {
                    // Reset timer
                    m_delayToIdle = 0.05f;
                    m_animator.SetInteger("AnimState", 1);
                }

                //Idle
                else
                {
                    // Prevents flickering transitions to idle
                    m_delayToIdle -= Time.deltaTime;
                    /*if (isPlayer1)
                    {
                        PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking = false;
                    }
                    else
                    {
                        PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking = false;
                    }*/
                    if (m_delayToIdle < 0)
                    {
                        m_animator.SetInteger("AnimState", 0);
                        /*if (isPlayer1)
                        {
                            PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking = false;
                        }
                        else
                        {
                            PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking = false;
                        }*/
                    }


                }

            }
            else
            {
                m_animator.SetBool("noBlood", m_noBlood);
                m_animator.SetTrigger("Death");
                this.transform.GetChild(6).GetChild(1).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(0, 0, 0, 0);
                m_isDead = true;

            }
        }
        
        else if (GameObject.Find("MainCamera").GetComponent<CameraController>().DisablePlayerMovement == false)
        {



            if (isPlayer1)
            {
                /*if(PlayerManager.GetComponent<PlayerAttackManager>().Player1 == null && PlayerManager.GetComponent<PlayerAttackManager>().Player1Assigned)
                {

                    print("No Player 1");
                    PlayerManager.GetComponent<PlayerAttackManager>().Player1Assigned = false;
                    GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopClient();
                    GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopHost();
                }*/
                this.transform.GetChild(6).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "Player 1";
                PlayerManager.GetComponent<PlayerAttackManager>().P1Health = PlayerHealth;
            }
            else
            {
                this.transform.GetChild(6).GetChild(2).GetComponent<UnityEngine.UI.Text>().text = "Player 2";
                //PlayerManager.GetComponent<PlayerAttackManager>().P2Health = PlayerHealth;
                AskToSetP2Health(PlayerHealth);
            }
            if (this.GetComponent<NetworkIdentity>().isLocalPlayer && !m_isDead)
            {
                
                if (PlayerHealth > 0)
                {


                    this.transform.GetChild(6).GetChild(1).GetComponent<UnityEngine.UI.Slider>().value = PlayerHealth;

                    // Increase timer that controls attack combo
                    m_timeSinceAttack += Time.deltaTime;

                    //Check if character just landed on the ground
                    if (!m_grounded && m_groundSensor.State())
                    {
                        CanRoll = true;
                        CanDoubleJump = true;
                        m_grounded = true;
                        m_animator.SetBool("Grounded", m_grounded);
                    }

                    //Check if character just started falling
                    if (m_grounded && !m_groundSensor.State())
                    {
                        m_grounded = false;
                        m_animator.SetBool("Grounded", m_grounded);
                    }

                    // -- Handle input and movement --
                    float inputX = Input.GetAxis("Horizontal");

                    // Swap direction of sprite depending on walk direction
                    if (inputX > 0 && m_animator.GetBool("WallSlide") == false && CanMove)
                    {
                        Transform playerpos = this.transform;
                        
                        //GetComponent<SpriteRenderer>().flipX = false;
                        this.transform.rotation = Quaternion.Euler(0, 0, 0);
                        this.transform.GetChild(6).rotation = Quaternion.Euler(0, 0, 0);
                        m_facingDirection = 1;
                        /*if (playerpos == this.transform)
                        {
                            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.005f, this.transform.position.z);
                        }*/
                        if (m_body2d.velocity.x == 0)
                        {
                            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z);
                        }
// Player is moving.

                    var distanceBetween = Vector2.Distance(this.transform.position, GameObject.Find("Tilemap").transform.position);
                        //Debug.Log($"distanceBetween My Player and the stuck block is : {GameObject.Find("Tilemap").name} == {distanceBetween}");
                    }

                    else if (inputX < 0 && m_animator.GetBool("WallSlide") == false && CanMove)
                    {
                        //GetComponent<SpriteRenderer>().flipX = true;
                        this.transform.rotation = Quaternion.Euler(0, 180, 0);
                        this.transform.GetChild(6).rotation = Quaternion.Euler(0, 0, 0);
                        m_facingDirection = -1;
                        if (m_body2d.velocity.x == 0)
                        {
                            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.05f, this.transform.position.z);
                        }
                    }

                    if (m_grounded && (m_wallSensorL1.transform.GetComponent<Sensor_HeroKnight>().m_ColCount > 0 || m_wallSensorL2.transform.GetComponent<Sensor_HeroKnight>().m_ColCount > 0 || m_wallSensorR1.transform.GetComponent<Sensor_HeroKnight>().m_ColCount > 0 || m_wallSensorR2.transform.GetComponent<Sensor_HeroKnight>().m_ColCount > 0))
                    {
                        m_animator.SetBool("WallSlide", true);
                    }
                    else
                    {
                        m_animator.SetBool("WallSlide", false);
                    }

                    if (m_facingDirection == 1)
                    {
                        m_wallSensorL1.transform.GetComponent<CircleCollider2D>().enabled = false;
                        m_wallSensorL2.transform.GetComponent<CircleCollider2D>().enabled = false;
                        m_wallSensorR1.transform.GetComponent<CircleCollider2D>().enabled = true;
                        m_wallSensorR2.transform.GetComponent<CircleCollider2D>().enabled = true;
                    }
                    else
                    {
                        m_wallSensorL1.transform.GetComponent<CircleCollider2D>().enabled = true;
                        m_wallSensorL2.transform.GetComponent<CircleCollider2D>().enabled = true;
                        m_wallSensorR1.transform.GetComponent<CircleCollider2D>().enabled = false;
                        m_wallSensorR2.transform.GetComponent<CircleCollider2D>().enabled = false;
                    }

                    // Move
                    if (!m_rolling && CanMove)
                        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

                    //Set AirSpeed in animator
                    m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

                    // -- Handle Animations --
                    //Wall Slide
                    m_animator.SetBool("WallSlide", (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State()));

                    //Death
                    if (Input.GetKeyDown("e") && !m_rolling && DebugMode)
                    {
                        PlayerHealth = 0;
                        m_animator.SetBool("noBlood", m_noBlood);
                        m_animator.SetTrigger("Death");
                    }

                    print(m_animator.GetBool("WallSlide"));

                    if (m_animator.GetBool("WallSlide") == true)
                    {

                        m_animator.SetBool("WallSlide", true);
                        GetComponent<SpriteRenderer>().flipX = false;
                    }

                    //Hurt
                    else if (Input.GetKeyDown("q") && !m_rolling && DebugMode)
                    {
                        PlayerHealth -= 1;
                        m_animator.SetTrigger("Hurt");
                    }



                    //Attack
                    else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling && CanMove)
                    {
                        m_currentAttack++;

                        // Loop back to one after third attack
                        if (m_currentAttack > 3)
                            m_currentAttack = 1;

                        // Reset Attack combo if time since last attack is too large
                        if (m_timeSinceAttack > 1.0f)
                            m_currentAttack = 1;

                        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
                        m_animator.SetTrigger("Attack" + m_currentAttack);
                        m_animator.SetBool("IsAttacking", true);
                        if (isPlayer1)
                        {

                            PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking = true;
                            print("Player 1 Attacking: " + PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking);
                            //print(PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking);
                        }
                        else
                        {

                            //PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking = true;
                            AskToSetP2Attack(true, PlayerHealth);
                            print("Player 2 Attacking: " + PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking);
                            //print(PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking);
                        }
                        //m_animator.SetBool("IsAttacking", false);
                        // Reset timer
                        m_timeSinceAttack = 0.0f;
                    }

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        CmdSpawnProjectile(m_facingDirection);
                    }

                    if (Input.GetMouseButtonUp(0) && CanMove)
                    {
                        if (isPlayer1)
                        {
                            PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking = false;
                        }
                        else
                        {
                            //PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking = false;
                            AskToSetP2Attack(false, PlayerHealth);
                        }
                    }

                    // Block
                    else if (Input.GetMouseButtonDown(1) && !m_rolling)
                    {
                        m_animator.SetTrigger("Block");
                        m_animator.SetBool("IdleBlock", true);
                        CanMove = false;
                        CanTakeDamage = false;
                    }

                    else if (Input.GetMouseButtonUp(1))
                    {
                        m_animator.SetBool("IdleBlock", false);
                        CanMove = true;
                        CanTakeDamage = true;
                    }


                    // Roll
                    else if (Input.GetKeyDown("left shift") && !m_rolling && m_animator.GetBool("WallSlide") == false && CanRoll && CanMove)
                    {
                        m_rolling = true;
                        m_animator.SetTrigger("Roll");
                        m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
                    }


                    //Jump
                    else if (Input.GetKeyDown("space") && m_grounded && !m_rolling && CanMove)
                    {
                        m_animator.SetTrigger("Jump");
                        m_grounded = false;
                        m_animator.SetBool("Grounded", m_grounded);
                        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                        m_groundSensor.Disable(0.1f);
                    }

                    else if (Input.GetKeyDown("space") && !m_grounded && CanDoubleJump && CanMove)
                    {
                        CanRoll = false;
                        CanDoubleJump = false;
                        m_animator.SetTrigger("Jump");
                        m_grounded = false;
                        m_animator.SetBool("Grounded", m_grounded);
                        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                        m_groundSensor.Disable(0.1f);
                    }
                    else if(Input.GetKeyDown("space") && !m_grounded && CanDoubleJump && CanMove)
                    {
                        
                        m_animator.SetTrigger("Jump");
                        m_grounded = false;
                        m_animator.SetBool("Grounded", m_grounded);
                        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                        m_groundSensor.Disable(0.1f);
                    }

                    //Run
                    else if (Mathf.Abs(inputX) > Mathf.Epsilon)
                    {
                        // Reset timer
                        m_delayToIdle = 0.05f;
                        m_animator.SetInteger("AnimState", 1);
                    }

                    //Idle
                    else
                    {
                        // Prevents flickering transitions to idle
                        m_delayToIdle -= Time.deltaTime;
                        /*if (isPlayer1)
                        {
                            PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking = false;
                        }
                        else
                        {
                            PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking = false;
                        }*/
                        if (m_delayToIdle < 0)
                        {
                            m_animator.SetInteger("AnimState", 0);
                            /*if (isPlayer1)
                            {
                                PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking = false;
                            }
                            else
                            {
                                PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking = false;
                            }*/
                        }


                    }

                }
                else
                {
                    m_animator.SetBool("noBlood", m_noBlood);
                    m_animator.SetTrigger("Death");
                    this.transform.GetChild(6).GetChild(1).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(0, 0, 0, 0);
                    m_isDead = true;

                }
            }
            else
            {

            }
        }
    }



    [ClientRpc]
    public void RpcAddForce(GameObject gm, Vector2 forcevector, float multiplier)
    {
        Debug.Log("command add force");
        gm.GetComponent<Rigidbody2D>().AddForce(forcevector * multiplier);
    }


    [Command]

    public void CmdSpawnProjectile(int Direction)
    {
        if (true)
        {
            if (IsNecromancer)
            {
                GameObject projectile = (GameObject)Instantiate(PlasmaBall, new Vector3(ObjectSpawnTransform.position.x, ObjectSpawnTransform.position.y, ObjectSpawnTransform.position.z), Quaternion.identity);
                NetworkServer.Spawn(projectile, connectionToClient);
                if (IsNecromancer)
                {

                    projectile.transform.localScale = new Vector3(1.5f, 1.5f, 1);

                }
                if (Direction == 1)
                {
                    RpcAddForce(projectile, new Vector2(600, 0), 1f);
                    //ninjastar.GetComponent<Rigidbody2D>().AddForce(new Vector2(600, 0));
                }
                else
                {
                    RpcAddForce(projectile, new Vector2(-600, 0), 1f);
                    //ninjastar.GetComponent<Rigidbody2D>().AddForce(new Vector2(-600, 0));
                }

                projectile.transform.parent = null;
            }
            else
            {
                GameObject projectile = (GameObject)Instantiate(NinjaStar, new Vector3(ObjectSpawnTransform.position.x, ObjectSpawnTransform.position.y, ObjectSpawnTransform.position.z), Quaternion.identity);
                NetworkServer.Spawn(projectile, connectionToClient);
                
                if (Direction == 1)
                {
                    RpcAddForce(projectile, new Vector2(600, 0), 1f);
                    //ninjastar.GetComponent<Rigidbody2D>().AddForce(new Vector2(600, 0));
                }
                else
                {
                    RpcAddForce(projectile, new Vector2(-600, 0), 1f);
                    //ninjastar.GetComponent<Rigidbody2D>().AddForce(new Vector2(-600, 0));
                }

                projectile.transform.parent = null;

            }
            //GameObject ninjastar = (GameObject)Instantiate(NinjaStar, new Vector3(ObjectSpawnTransform.position.x, ObjectSpawnTransform.position.y, ObjectSpawnTransform.position.z), Quaternion.identity);
            
        }
            
        
        
        
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
            if (other.gameObject.tag == "NinjaStar")
            {

            HitWithNinjaStar = true;
            /*m_animator.SetTrigger("Hurt");
            print("Player Attacked!");
            SetPlayerAttack(false);
            PlayerHealth -= 1;
            CanTakeDamage = false;
            StartCoroutine(Countdown(1));
            */


            }
        
            
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if(other.tag == "Player")
        if (isPlayer1 && other.tag == "Player")
        {
            PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking = false;
        }
        else
        {
            PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking = false;
        }
    }

    IEnumerator Countdown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }
        CanTakeDamage = true;
        //DoStuff();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!m_isDead)
        {

            
            if (other.transform.tag == "Player")
            {
                if (other.name != "Local")
                {
                    //PlayerHealth -= 1;
                    if (PlayerManager.GetComponent<PlayerAttackManager>().Player1Attacking && !isPlayer1 && CanTakeDamage)
                    {
                        m_animator.SetTrigger("Hurt");
                        print("Player Attacked!");
                        SetPlayerAttack(false);
                        PlayerHealth -= 1;
                        CanTakeDamage = false;
                        StartCoroutine(Countdown(1));
                        //AskToSetP2Attack(false, 0);
                    }
                    else if (PlayerManager.GetComponent<PlayerAttackManager>().Player2Attacking && isPlayer1 && CanTakeDamage)
                    {
                        m_animator.SetTrigger("Hurt");
                        print("Player Attacked!");
                        SetPlayerAttack(false);
                        PlayerHealth -= 1;
                        CanTakeDamage = false;
                        StartCoroutine(Countdown(1));
                        //AskToSetP2Attack(false, 0);
                    }
                }
            }
        }
        else
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
            this.transform.GetChild(6).GetChild(1).GetChild(1).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new UnityEngine.Color(0, 0, 0, 0);
            m_isDead = true;
        }
        
        
    }

    [Command]
    public void TakeDamage(int amount)
    {
        PlayerHealth -= amount;
    }



    // Animation Events
    // Called in end of roll animation.
    void AE_ResetRoll()
    {
        m_rolling = false;
    }

    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }

}
