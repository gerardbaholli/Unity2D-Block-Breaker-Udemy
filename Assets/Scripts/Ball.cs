using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] Paddle paddle;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;

    // cache
    AudioSource audioSource;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    private void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
        
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);

        transform.position = paddlePos + paddleToBallVector;
        //transform.position = (Vector2) paddle.transform.position + paddleToBallVector;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
        audioSource.PlayOneShot(clip);
    }


}
