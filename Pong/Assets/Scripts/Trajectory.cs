using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public BallControl ball;
    CircleCollider2D ballCollider;
    Rigidbody2D rigid;

    public GameObject ballAtCollision;
    // Start is called before the first frame update
    void Start()
    {
        ballCollider = ball.GetComponent<CircleCollider2D>();
        rigid = ball.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool drawBallAtCollision = false;

        Vector2 offsetHitPoint = new Vector2();

        RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(rigid.position, ballCollider.radius, rigid.velocity.normalized);

        foreach(RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            if(circleCastHit2D.collider != null && circleCastHit2D.collider.GetComponent<BallControl>() == null)
            {
                Vector2 hitPoint = circleCastHit2D.point;
                Vector2 hitNormal = circleCastHit2D.normal;

                offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;

                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);


                if(circleCastHit2D.collider.GetComponent<SideWall>() == null)
                {
                    Vector2 invector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;

                    Vector2 outVector = Vector2.Reflect(invector, hitNormal);

                    float outdot = Vector2.Dot(outVector, hitNormal);

                    if(outdot>-1.0f && outdot < 1.0)
                    {
                        DottedLine.DottedLine.Instance.DrawDottedLine(offsetHitPoint, offsetHitPoint + outVector * 10f);

                        drawBallAtCollision = true;

                        
                    }
                }
            }
        }
        if (drawBallAtCollision)
        {
            ballAtCollision.transform.position = offsetHitPoint;
            ballAtCollision.SetActive(true);
        }
        else
        {
            ballAtCollision.SetActive(false);
        }
    }
}
