using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rBD;
    public float speed;
    private float acceleration;
    public float maxSpeed;

    private bool isMovingLeft, isMovingRight, isMovingUp, isMovingDown, isBrakeApplied;

    // Start is called before the first frame update
    void Start()
    {
        isMovingDown = isMovingLeft = isMovingRight = isMovingUp = false;
        isBrakeApplied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Brake Applied");
            acceleration = 0;
            rBD.velocity = new Vector2(0, 0);
            isMovingRight = false;
            isMovingLeft = false;
            isMovingUp = false;
            isMovingDown = false;
            isBrakeApplied = false;
            return;
        }
        if (Input.GetAxis("Horizontal")>0)
        {
            if (acceleration < maxSpeed)
                acceleration = acceleration + speed;
            else
                acceleration = maxSpeed;
            isMovingRight = true;
            isMovingLeft = false;
            isMovingUp = false;
            isMovingDown = false;
            isBrakeApplied = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            if (acceleration < maxSpeed)
                acceleration = acceleration + speed;
            else
                acceleration = maxSpeed;
            isMovingRight = false;
            isMovingLeft = true;
            isMovingUp = false;
            isMovingDown = false;
            isBrakeApplied = false;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            if (acceleration < maxSpeed)
                acceleration = acceleration + speed;
            else
                acceleration = maxSpeed;
            isMovingRight = false;
            isMovingLeft = false;
            isMovingUp = false;
            isMovingDown = true;
            isBrakeApplied = false;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            if (acceleration < maxSpeed)
                acceleration = acceleration + speed;
            else
                acceleration = maxSpeed;
            isMovingRight = false;
            isMovingLeft = false;
            isMovingUp = true;
            isMovingDown = false;
            isBrakeApplied = false;
        }
        else
        {
            isBrakeApplied = true;
        }
    }

    private void FixedUpdate()
    {
        if (isBrakeApplied)
        {
            if (acceleration > 0)
                acceleration -= speed;
            else
                acceleration = 0;
        }
        if (isMovingLeft)
        {
            rBD.velocity = new Vector2(-acceleration, 0);
        }
        else if (isMovingRight)
        {
            rBD.velocity = new Vector2(acceleration, 0);
        }
        else if (isMovingUp)
        {
            rBD.velocity = new Vector2(0, -acceleration);
        }
        else if (isMovingDown)
        {
            rBD.velocity = new Vector2(0, acceleration);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="WinBox")
            Debug.Log("Level Complete!!");
    }
}
