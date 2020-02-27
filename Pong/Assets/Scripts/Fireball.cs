using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    private Rigidbody2D rigid;

    public float xInitialForce;
    public float yInitialForce;

    public GameManager gameManager;

    public PlayerControl player1;
    public PlayerControl player2;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ResetBall()
    {
        transform.position = Vector2.zero;
        rigid.velocity = Vector2.zero;
    }
    void PushBall()
    {
        float randomYInitialForce = Random.Range(-yInitialForce, yInitialForce);

        float randomDirection = Random.Range(0, 2);

        if(randomDirection < 1)
        {
            rigid.AddForce(new Vector2(-xInitialForce, randomYInitialForce));
        }
        else
        {
            rigid.AddForce(new Vector2(xInitialForce, randomYInitialForce));
        }
    }
    void RestartGame()
    {
        ResetBall();

        Invoke("PushBall", 2);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player1")
        {
            player2.Score = gameManager.maxScore;
            
        }
        if(collision.gameObject.tag == "Player2")
        {
            player1.Score = gameManager.maxScore;
            
        }
    }
}
