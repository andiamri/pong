using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRandomColor : MonoBehaviour
{
    private Color oldColor;
    // Start is called before the first frame update
    void Start()
    {
        oldColor = Color.white;
        GetComponent<Renderer>().material.color = oldColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player1Ball")
        {
            GetComponent<Renderer>().material.color = Color.blue ;
        }
        if(collision.gameObject.name == "Player2Ball")
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
