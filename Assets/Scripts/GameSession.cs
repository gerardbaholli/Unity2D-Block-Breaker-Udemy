using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{

    [Range(.1f,10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] int pointsPerBlockDestroyed = 83;

    [SerializeField] int currentScore;

    // singleton pattern
    private void Awake()
    {
        int numberOfInstance = FindObjectsOfType<GameSession>().Length;
        if (numberOfInstance > 1)
        {
            gameObject.SetActive(false); // prevent a bug with unity life cicle
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        currentScore = 0;
        score.text = currentScore.ToString();
    }

    private void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        score.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }


}
