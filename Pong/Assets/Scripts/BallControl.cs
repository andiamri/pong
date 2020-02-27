using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigid;

    public float xInitialForce;
    public float yInitialForce;
    public float force;

    private Vector2 trajectoryOrigin;

    private AudioSource audioSource;
    public AudioClip audioClip;

    public GameObject player1;
    public GameObject player2;
    private GameObject wallsUp, wallsDown,center;

    public GameObject powerUp;

    public float nextSpawn;
    public float countDown;

    [Header("X Spawn Range")]
    public float xMin;
    public float xMax;

    [Header("Y Spawn Range")]
    public float yMin;
    public float yMax;

    private float powerUpCount = 0;
    private float maxPowerUpCount = 5;
    

    private Color ballColor;
    private bool isPlayer1PowerUpActivated = false;
    private bool isPlayer2PowerUpActivated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        wallsUp = GameObject.Find("UpWall");
        wallsDown = GameObject.Find("DownWall");
        center = GameObject.Find("CenterWall");
        ballColor = Color.red;
        audioSource = GetComponent<AudioSource>();
        trajectoryOrigin = transform.position;
        rigid = GetComponent<Rigidbody2D>();
        RestartGame();
        
        GetComponent<Renderer>().material.color = ballColor;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;

        if(countDown <= 0)
        {
            SpawnPowerUp();
            countDown = nextSpawn;
        }
    }
    public void SpawnPowerUp()
    {
        Vector2 pos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

        Instantiate(powerUp,pos,Quaternion.identity);

        powerUpCount++;
    }
    public void ResetBall()
    {
        transform.position = Vector2.zero;

        rigid.velocity = Vector2.zero;
        
    }
    public void PushBall()
    {
        
        float randomDirection = Random.Range(0, 2);
        float randomYInitialForce = Random.Range(-yInitialForce, yInitialForce);
       
        
        
        if(randomDirection < 1)
        {
            rigid.velocity = new Vector2(-2, 1);
            rigid.AddForce(new Vector2(-xInitialForce , randomYInitialForce));
           
            Debug.Log("Left");
            
        }
        else
        {
            rigid.velocity = new Vector2(2, 1);
            rigid.AddForce(new Vector2(xInitialForce, randomYInitialForce));
            Debug.Log("Right");
            
        }
        gameObject.name = "Ball";
        Debug.Log(randomDirection);
    }
    public void RestartGame()
    {
        isPlayer1PowerUpActivated = false;
        isPlayer2PowerUpActivated = false;
        ResetBall();
        player1.GetComponent<PlayerControl>().PowerUpDeactivated();
        player2.GetComponent<PlayerControl>().PowerUpDeactivated();
        gameObject.GetComponent<Renderer>().material.color = ballColor;
        center.GetComponent<Renderer>().material.color = Color.white;
        Invoke("PushBall", 2);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
        if(collision.gameObject.tag == "Player1")
        {
            
            gameObject.name = "Player1Ball";
            GetComponent<Renderer>().material.color = collision.gameObject.GetComponent<Renderer>().material.color;
            
           
        }else if(collision.gameObject.tag == "Player2")
        {
            
            gameObject.name = "Player2Ball";
            GetComponent<Renderer>().material.color = collision.gameObject.GetComponent<Renderer>().material.color;
        }
    }
    

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            audioSource.PlayOneShot(audioClip);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.name == "Player1Ball")
        {
            if(collision.gameObject.tag == "PowerUp" && isPlayer1PowerUpActivated == false)
            {
                
                player1.GetComponent<PlayerControl>().PowerUpActivated();
                Debug.Log("Player1GainPowerUp");
                Destroy(collision.gameObject);
                isPlayer1PowerUpActivated = true;
                
            }
        }
        if(gameObject.name == "Player2Ball")
        {
            if(collision.gameObject.tag == "PowerUp" && isPlayer2PowerUpActivated == false)
            {
               
                player2.GetComponent<PlayerControl>().PowerUpActivated();
                Debug.Log("Player2GainpowerUp");
                Destroy(collision.gameObject);
                isPlayer2PowerUpActivated = true;
                
               
            }
        }
    }
}
