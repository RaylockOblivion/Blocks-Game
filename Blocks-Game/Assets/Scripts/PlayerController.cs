using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rBD;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal")>0)
        {
            rBD.velocity = new Vector2(speed, 0);
        }else if (Input.GetAxis("Horizontal") < 0)
        {
            rBD.velocity = new Vector2(-speed, 0);
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            rBD.velocity = new Vector2(0,speed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            rBD.velocity = new Vector2(0,-speed);
        }
        else
        {
            rBD.velocity = new Vector2(0, 0);
        }
    }
}
