using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rBD;
    public float speed;
    private float acceleration;
    public float maxSpeed;
    private Vector2 velocityStore;

    public GameObject GameWonPanel,PauseScreen,GameLostPanel;

    private bool isMovingLeft, isMovingRight, isMovingUp, isMovingDown, isBrakeApplied, instaBreak, isGameOver, isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isMovingDown = isMovingLeft = isMovingRight = isMovingUp = false;
        isBrakeApplied = instaBreak = false;
        isGameOver = isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver || isPaused)
            return;
        if (Input.GetAxis("Cancel") > 0)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseScreen.SetActive(true);
                velocityStore = rBD.velocity;
                rBD.velocity = Vector2.zero;
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
        if (isGameOver|| isPaused)
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
            isGameOver = true;
            Debug.Log("Level Complete!!");
        }
        else if (collision.tag == "Enemy")
        {
            GameLostPanel.SetActive(true);
            isGameOver = true;
            Debug.Log("Level Lost!!");
        }
    }

    public void RestartGame()
    {
        isGameOver = false;
        GameWonPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {
        isPaused = false;
        PauseScreen.SetActive(false);
        rBD.velocity = velocityStore;
    }
}
