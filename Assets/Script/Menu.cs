using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnStartButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
