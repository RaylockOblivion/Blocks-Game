using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rBD;
    public float speed;
    private float acceleration;
    public float maxSpeed;
    public GameObject startPos;

    public GameObject GameWonPanel,PauseScreen;

    private bool isMovingLeft, isMovingRight, isMovingUp, isMovingDown, isBrakeApplied, instaBreak, isGameWon, isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isMovingDown = isMovingLeft = isMovingRight = isMovingUp = false;
        isBrakeApplied = instaBreak = false;
        isGameWon = isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameWon||isPaused)
            return;
        if (Input.GetAxis("Cancel") > 0)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseScreen.SetActive(true);
                return;
            }
            else
            {
                ResumeGame();
            }
        }
        if (acceleration == 0)
        {
            isMovingRight = false;
            isMovingLeft = false;
            isMovingUp = false;
            isMovingDown = false;
            isBrakeApplied = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            acceleration = 0;
            isMovingRight = false;
            isMovingLeft = false;
            isMovingUp = false;
            isMovingDown = false;
            isBrakeApplied = false;
            instaBreak = true;
            return;
        }
        if (instaBreak)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                instaBreak = false;
            }
            else
            {
                return;
            }
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
        if (isGameWon|| isPaused)
            return;
        if (instaBreak)
        {
            rBD.velocity = new Vector2(0, 0);
            return;
        }
        if (isBrakeApplied)
        {
            if (acceleration > 0)
            {
                acceleration = acceleration - speed;
            }
            else
            {
                Debug.Log("entered Break clean");
                acceleration = 0;
                isMovingRight = false;
                isMovingLeft = false;
                isMovingUp = false;
                isMovingDown = false;
                isBrakeApplied = false;
                rBD.velocity = new Vector2(0, 0);
            }
        }
        if (isMovingLeft)
        {
            rBD.velocity = new Vector2(-acceleration*Time.deltaTime, 0);
        }
        else if (isMovingRight)
        {
            rBD.velocity = new Vector2(acceleration * Time.deltaTime, 0);
        }
        else if (isMovingUp)
        {
            rBD.velocity = new Vector2(0, -acceleration * Time.deltaTime);
        }
        else if (isMovingDown)
        {
            rBD.velocity = new Vector2(0, acceleration * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WinBox")
        {
            GameWonPanel.SetActive(true);
            isGameWon = true;
            Debug.Log("Level Complete!!");
        }
    }

    public void RestartGame()
    {
        isGameWon = false;
        GameWonPanel.SetActive(false);
        gameObject.transform.position = startPos.transform.position;
    }

    public void ResumeGame()
    {
        isPaused = false;
        PauseScreen.SetActive(false);
    }
}
