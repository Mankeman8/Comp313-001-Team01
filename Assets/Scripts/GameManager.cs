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
    public static float timeAlive = 0; // Time player has stayed alive
    public static int enemiesKilled = 0; // Number of enemies killed by the player
    public static int levelsBeaten = 0; // Number of levels beaten by the player
    public static int damageDealt = 0; // Damage dealt by the player
    public static int damageTaken = 0; // Damage taken by the player
    private static float difficultyModifier = 1f; // Initial difficulty modifier
    private static string difficulty = "Easy";
    public bool playerDead = false;
    //UI Stuff
    public TextMeshProUGUI timerInGame;
    public TextMeshProUGUI difficultyInGame;
    public TextMeshProUGUI enemyDifficultyInGame;
    //Pause Menu
    public GameObject pauseMenu;
    public bool pausedGame;
    //Player Dead
    public GameObject playerDeadGO;
    private bool statScreen = false;

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
        if (SceneManager.GetActiveScene().name == mainMenu)
        {
            return;
        }
        Time.timeScale = 1.0f;
        
        timerInGame = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        difficultyInGame = GameObject.Find("Difficulty").GetComponent<TextMeshProUGUI>();
        enemyDifficultyInGame = GameObject.Find("EnemyDifficulty").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == mainMenu)
        {
            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
        levelDone = false;
        startTeleporter = false;
        difficultyInGame.text = difficulty;
        pauseMenu = GameObject.Find("Pause");
        pausedGame = false;
        pauseMenu.SetActive(false);
        playerDeadGO.SetActive(false);
        statScreen = false;
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
        timeAlive = 0f;
        enemiesKilled = 0;
        levelsBeaten = 0;
        damageDealt = 0;
        damageTaken = 0;
        difficultyModifier = 1f;
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
        levelsBeaten++;
        FindObjectOfType<GenerateEnemies>().spawn = false;
        FindObjectOfType<GenerateEnemies>().levelDone = true;
    }

    public void EnemiesKilled(int value)
    {
        enemiesKilled += value;
    }

    public void DamageDealt(int damage)
    {
        damageDealt += damage;
    }

    public void DamageTaken(int damage)
    {
        damageTaken += damage;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.0f;
        pausedGame = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        pausedGame = false;
    }

    public void PlayerDead()
    {
        double totalScore;
        totalScore = (timeAlive * 1.5f) + (enemiesKilled * 2) + (levelsBeaten * 1000) + (damageDealt * 1.75) + difficultyModifier - (damageTaken * 2);
        playerDeadGO.SetActive(true);
        GameObject.Find("TimeAlive").GetComponent<TextMeshProUGUI>().text = "Time Alive: " + timeAlive.ToString("00.00");
        GameObject.Find("EnemiesKilled").GetComponent<TextMeshProUGUI>().text = "Enemies Killed: " + enemiesKilled;
        GameObject.Find("LevelsBeaten").GetComponent<TextMeshProUGUI>().text = "Levels Beaten: " + levelsBeaten;
        GameObject.Find("DamageDealt").GetComponent<TextMeshProUGUI>().text = "Damage Dealt: " + damageDealt;
        GameObject.Find("DifficultyModifier").GetComponent<TextMeshProUGUI>().text = "Difficulty: " + difficulty;
        GameObject.Find("DamageTaken").GetComponent<TextMeshProUGUI>().text = "Damage Taken: " + damageTaken;
        GameObject.Find("TotalScore").GetComponent<TextMeshProUGUI>().text = "Total Score: " + totalScore.ToString("00.00");
        Cursor.lockState = CursorLockMode.None;
        statScreen = true;
    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == mainMenu)
        {
            return;
        }
        if (playerDead && statScreen)
        {
            return;
        }
        if (playerDead)
        {
            Time.timeScale = 0.0f;
            PlayerDead();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausedGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
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

    public void SetDifficulty(Toggle toggle)
    {
        if (toggle.isOn)
        {
            difficulty = toggle.name.ToString();
            Debug.Log("Toggle: " + toggle.name.ToString());
            Debug.Log("Difficulty: " + difficulty);
        }
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
