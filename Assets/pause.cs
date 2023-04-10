using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject pausemenu;
    public static bool Paused;
    // Start is called before the first frame update
    void Start()
    {
        Paused = false;
        pausemenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1.0f;
        Paused= false;
    }
    public void Pause()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0.0f;
        Paused = true;
    }
    public void SeetingMenu()
    {
        
    }
    public void MainMenu()
    {

    }
    public void Continue()
    {
        Resume();
    }
}
