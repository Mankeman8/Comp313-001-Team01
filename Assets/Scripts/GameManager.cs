using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string mainMenu = "MainMenu";
    public string tutorial = "Tutorial";
    public string levelOne = "Level01";
    public bool levelDone = false;
    public bool startTeleporter = false;

    private void Start()
    {
        levelDone = false;
        startTeleporter = false;
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
        startTeleporter = true;
    }

    public void LevelDone()
    {
        //The teleporter script will call this function
        //It's used to display to the player that the level is complete and they can proceed to the next level.
        levelDone = true;
    }
}
