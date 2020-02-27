using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode upButton, downButton;

    public float speed = 10f;
    public float yBoundary = 8.6f;

    private Rigidbody2D rigid;
    private BoxCollider2D boxCollider2D;

    public GameObject upBorder;
    public GameObject downBorder;

    private int score;

    private bool isPowerActivated = false;

    private ContactPoint2D lastContactPoint;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        upBorder = GameObject.Find("UpBorder");
        downBorder = GameObject.Find("DownBorder");
       
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //get player velocity
        Vector2 playerVelocity = rigid.velocity;

        //check input button
        //if up button then do the following on curly braces
        if (Input.GetKey(upButton))
        {
            playerVelocity.y = speed;
        }
        //if down button do the following on curly braces
        else if (Input.GetKey(downButton))
        {
            playerVelocity.y = -speed;
        }
        //if not above the set playerVelocity to zero
        else
        {
            playerVelocity.y = 0;
        }

        //set rigidbody velocity to playerVelocity
        rigid.velocity = playerVelocity;

        //get the player position
        Vector3 position = transform.position;

        //check if position.y(transform.position) greater than yBoundary then set position.y to yBoundary variable
        //if(position.y > yBoundary)
        //{ 
        //  position.y = yBoundary;
        // }
        //if the position(transform.position) less than -yBoundary the set position.y to -yBoundary
        //else if(position.y < -yBoundary)
        //{
        //   position.y = -yBoundary;
        // Debug.Log("Player2"+downBorder.transform.position.y);
        //}
        if (isPowerActivated)
        {
            float newYboundary = 7.6f;
           

            if(position.y > newYboundary)
            {
                position.y = newYboundary;
            }
            else if(position.y < -newYboundary)
            {
                position.y = -newYboundary;
            }  
        }
        else
        {
            if(position.y > yBoundary)
            {
                position.y = yBoundary;
            }else if(position.y < -yBoundary)
            {
                position.y = -yBoundary;
            }
        }
        

        //set transform.position to position
        transform.position = position;
    }

    public void PowerUpActivated()
    {
        isPowerActivated = true;
        transform.localScale = new Vector3(1, 2, 1);
        Debug.Log("PowerUp");
    }
    public void PowerUpDeactivated()
    {
        isPowerActivated = false;
        transform.localScale = new Vector3(1, 1, 1);
        Debug.Log("PowerUpDeactivated");
    }
    

    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    public void IncrementScore()
    {
        score++;
    }
    public void ResetScore()
    {
        score = 0;
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
        
    }
}
