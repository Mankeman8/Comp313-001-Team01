using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    //Variables for navigating the menu
    public string mainMenu = "MainMenu";
    public string tutorial = "Tutorial";
    public string levelOne = "Level01";
    //Bools for inside the game
    public bool levelDone = false;
    public bool startTeleporter = false;
    //Keeping track of timer and stuff
    public float timeAlive; // Time player has stayed alive
    public int enemiesKilled; // Number of enemies killed by the player
    public int levelsBeaten; // Number of levels beaten by the player
    public float damageDealt; // Damage dealt by the player
    public float damageTaken; // Damage taken by the player
    private float difficultyModifier = 1f; // Initial difficulty modifier
    private string difficulty = "Easy";
    //UI Stuff
    public TextMeshProUGUI timerInGame;
    public TextMeshProUGUI difficultyInGame;
    public TextMeshProUGUI enemyDifficultyInGame;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        timerInGame = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        difficultyInGame = GameObject.Find("Difficulty").GetComponent<TextMeshProUGUI>();
        enemyDifficultyInGame = GameObject.Find("EnemyDifficulty").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {

        levelDone = false;
        startTeleporter = false;
        difficultyInGame.text = difficulty;
    }

    public void QuitGame()
    {
        //Simplest way to quit the game
        Application.Quit();
    }

    public void NextLevel()
    {
        //Basic way of loading the next level without any strings or hard coded variables
        //Just make sure that the project build is in the right order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TutorialLevel()
    {
        //Used in main menu, to load the tutorial level
        SceneManager.LoadScene(tutorial);
    }

    public void LevelOne()
    {
        //Used in main menu, to load the first level
        SceneManager.LoadScene(levelOne);
    }

    public void Restart()
    {
        //Restart the current scene without any hardcoded variables
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        //This one is fine because the main menu is universal and there should
        //only be one of them per game.
        SceneManager.LoadScene(mainMenu);
    }

    public void StartTeleporter()
    {
        //Increase spawn rate of enemies, along with summoning a boss
        //make it so that if level's done, we can't start it again
        if (!levelDone)
        {
            startTeleporter = true;
        }
    }

    public void LevelDone()
    {
        //The teleporter script will call this function
        //It's used to display to the player that the level is complete and they can proceed to the next level.
        levelDone = true;
    }
    void FixedUpdate()
    {
        UpdateTimeAlive();
        UpdateDifficultyModifier();
        EnemyDifficulty();
    }

    void UpdateTimeAlive()
    {
        //Increase time by a normal rate
        //Show it on screen
        timeAlive += Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeAlive);
        string timer = string.Format("{0:D}:{1:D2}", timeSpan.Minutes + (timeSpan.Hours * 60), timeSpan.Seconds);
        timerInGame.text = timer;
    }

    void UpdateDifficultyModifier()
    {
        // Update the difficulty modifier based on timeAlive or other factors
        // We'll modify based on what we wanna do with it, and use the variable
        // to increase health + damage (as a multiplicator of some sort. e.g, health x difficultyModifier)
        if(difficulty=="Easy")
        {
            difficultyModifier = 1f + (timeAlive / difficultyModifier);
        }
        if(difficulty=="Normal")
        {
            difficultyModifier = 1.5f + (timeAlive / difficultyModifier);
        }
        if(difficulty=="Hard")
        {
            difficultyModifier = 2f + (timeAlive / difficultyModifier);
        }
    }
    //Not sure what to do with this yet
    //but, we could use it as a multiplier for health/damage for enemies
    public float GetDifficultyModifier()
    {
        return difficultyModifier;
    }

    public string GetDifficulty()
    {
        return difficulty;
    }

    private void EnemyDifficulty()
    {
        switch (difficulty)
        {
            case "Easy":
                switch (timeAlive)
                {
                    case < 300:
                        enemyDifficultyInGame.text = "Sunshine and Rainbows";
                        break;
                    case < 600:
                        enemyDifficultyInGame.text = "Slowly becoming hard";
                        break;
                    case < 900:
                        enemyDifficultyInGame.text = "You should hurry up";
                        break;
                    case < 1200:
                        enemyDifficultyInGame.text = "I'm coming for you";
                        break;
                    case < 1500:
                        enemyDifficultyInGame.text = "Why aren't you running?";
                        break;
                    case < 1800:
                        enemyDifficultyInGame.text = "You should run";
                        break;
                    case < 2100:
                        enemyDifficultyInGame.text = "RUN";
                        break;
                    case < 2400:
                        enemyDifficultyInGame.text = "I'M RIGHT BEHIND YOU";
                        break;
                    case < 2700:
                        enemyDifficultyInGame.text = "YOU'RE TOO SLOW";
                        break;
                    case >= 2700:
                        enemyDifficultyInGame.text = "HAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAH";
                        break;
                    default:
                        break;
                }
                break;
            case "Normal":
                switch (timeAlive)
                {
                    case < 300:
                        enemyDifficultyInGame.text = "You should hurry up";
                        break;
                    case < 600:
                        enemyDifficultyInGame.text = "I'm coming for you";
                        break;
                    case < 900:
                        enemyDifficultyInGame.text = "Why aren't you running?";
                        break;
                    case < 1200:
                        enemyDifficultyInGame.text = "You should run";
                        break;
                    case < 1500:
                        enemyDifficultyInGame.text = "RUN";
                        break;
                    case < 1800:
                        enemyDifficultyInGame.text = "I'M RIGHT BEHIND YOU";
                        break;
                    case < 2100:
                        enemyDifficultyInGame.text = "YOU'RE TOO SLOW";
                        break;
                    case >= 2100:
                        enemyDifficultyInGame.text = "HAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAH";
                        break;
                    default:
                        enemyDifficultyInGame.text = "HAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAH";
                        break;
                }
                break;
            case "Hard":
                switch (timeAlive)
                {
                    case < 300:
                        enemyDifficultyInGame.text = "Why aren't you running?";
                        break;
                    case < 600:
                        enemyDifficultyInGame.text = "You should run";
                        break;
                    case < 900:
                        enemyDifficultyInGame.text = "RUN";
                        break;
                    case < 1200:
                        enemyDifficultyInGame.text = "I'M RIGHT BEHIND YOU";
                        break;
                    case < 1500:
                        enemyDifficultyInGame.text = "YOU'RE TOO SLOW";
                        break;
                    case >= 1500:
                        enemyDifficultyInGame.text = "HAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAH";
                        break;
                    default:
                        enemyDifficultyInGame.text = "HAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAH";
                        break;
                }
                break;
            default:
                enemyDifficultyInGame.text = "HAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAH";
                break;
        }
    }
}
