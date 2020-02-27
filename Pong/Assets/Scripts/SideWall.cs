using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    public PlayerControl player;

    private GameObject centerWall;
   
    
    // Start is called before the first frame update
    void Start()
    {
        centerWall = GameObject.Find("CenterWall");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
           
            collision.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            centerWall.gameObject.GetComponent<Renderer>().material.color = Color.white;
            player.IncrementScore();
            if (player.Score < gameManager.maxScore)
            {
                
                if(collision.gameObject.name == "Player1Ball")
                {
                    //player.IncrementScore();
                    collision.gameObject.name = "Ball";
                    collision.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
                    centerWall.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                    

                }
                if (collision.gameObject.name == "Player2Ball")
                {
                    //player.IncrementScore();
                    collision.gameObject.name = "Ball";
                    collision.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
                    centerWall.gameObject.GetComponent<Renderer>().material.color = Color.green;
                    

                }
            }
        }
    }
}
