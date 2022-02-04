using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle: MonoBehaviour
{

    // Configuration paramaters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    private GameSession gameSession;
    private Ball ball;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;

        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        paddlePosition.x = Mathf.Clamp(GetXPos(), minX, maxX);

        transform.position = paddlePosition;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }

}
