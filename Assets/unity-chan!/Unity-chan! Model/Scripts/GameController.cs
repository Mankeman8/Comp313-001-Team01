using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour

{
    [Header("UI Objects")]
    public TextMeshProUGUI scoreLable;
    public TextMeshProUGUI livesLabel;

    [Header("Audio Manager")]
    //public SoundClip soundClip;
    public AudioSource[] audioSources;

    private int _score;
    private int _highScore;
    private int _lives;

    public int Score{ get { return _score; } set { _score = value; } }
    public int HighScore { get { return _highScore; } set { _highScore = value; } }
    public int Lives { get { return _lives; } set { _lives = value; } }


    public void AddScore()
    {
        Score += 50;
        if (Score>=HighScore)
        {
            HighScore = Score;
        }
        scoreLable.text = $"Score: {Score}";
    }

    public void AddLives(int amount)
    {
        Lives +=amount;
        livesLabel.text = $"Lives: {Lives}";

    }
    public void ResetScore()
    {
        Score = 0;
        Lives = 3;
    }

    public void ResetAll()
    {
        Score = 0;
        HighScore = 0;
        Lives = 3;
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetAll();
        scoreLable.text = $"Score: {Score}";
        livesLabel.text = $"Lives: {Lives}";
    }

    // Update is called once per frame
    void Update()
    {
        scoreLable.text = $"Score: {Score}";
        livesLabel.text = $"Lives: {Lives}";
    }
}
