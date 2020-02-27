using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public PlayerControl player1;
    private Rigidbody2D player1Rigid;

    public PlayerControl player2;
    private Rigidbody2D player2Rigid;

    public BallControl ball;
    private Rigidbody2D ballRigid;
    private CircleCollider2D ballCollider;

    public Fireball fireBall;

    public int maxScore;

    private bool isDebugWindowShown = false;

    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text winMessageText;

    public Trajectory trajectory;
    // Start is called before the first frame update
    void Start()
    {
        player1Rigid = player1.GetComponent<Rigidbody2D>();
        player2Rigid = player2.GetComponent<Rigidbody2D>();
        ballRigid = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartGame()
    {
        player1.ResetScore();
        player2.ResetScore();

        ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
    }
   

    void OnGUI()
    {
        
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100, 100), "" + player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100, 100), "" + player2.Score);


        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
           
            player1.ResetScore();
            player2.ResetScore();

            Destroy(GameObject.FindGameObjectWithTag("PowerUp"));
            

            Time.timeScale = 1;
            ball.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
            fireBall.SendMessage("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);

        }

        
        if (player1.Score == maxScore)
        {
            Time.timeScale = 0;
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");

            fireBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            Destroy(GameObject.FindGameObjectWithTag("PowerUp"));
            
        }
      
        else if (player2.Score == maxScore)
        {
            Time.timeScale = 0;
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");

            fireBall.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
            Destroy(GameObject.FindGameObjectWithTag("PowerUp"));

        }
        
        if (isDebugWindowShown)
        {
            Color oldColor = GUI.backgroundColor;

            GUI.backgroundColor = Color.red;

            float ballMass = ballRigid.mass;
            Vector2 ballVelocity = ballRigid.velocity;
            float ballSpeed = ballRigid.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = ballCollider.friction;

            float impulsePlayer1X = player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.LastContactPoint.tangentImpulse;

            string debugText =
                    "Ball mass = " + ballMass + "\n" +
                    "Ball velocity = " + ballVelocity + "\n" +
                    "Ball speed = " + ballSpeed + "\n" +
                    "Ball momentum = " + ballMomentum + "\n" +
                    "Ball friction = " + ballFriction + "\n" +
                    "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                    "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            GUI.backgroundColor = oldColor;
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 60, Screen.height - 73, 120, 53), "Toggle\nDebug Info"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }
    }
}
