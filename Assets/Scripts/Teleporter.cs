using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public bool Activated = false;
    public bool isRunning = false;
    public bool levelDone = false;
    public int heightAdded = 1550;
    public float initialHeight = 0;
    public float maxHeight = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Setting up some variables before we start the game
        initialHeight = this.gameObject.transform.position.y;
        maxHeight = initialHeight + heightAdded;
    }

    // Update is called once per frame
    void Update()
    {
        //Once the teleporter is activated (and it's not running and/or level is not completed)
        //Activate it
        if(Activated && !isRunning && !levelDone)
        {
            TeleporterActivation();
        }
        //While it's running, increase the height of the hammer by a certain amount per second.
        //Once it reached (or exceeds) max height, stop it and level is done
        if (isRunning)
        {
            if(this.gameObject.transform.position.y >= maxHeight)
            {
                Activated = false;
                isRunning = false;
                levelDone = true;
                FindObjectOfType<GameManager>().LevelDone();
                return;
            }
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.transform.position.y + (Time.deltaTime * 12), this.gameObject.transform.position.z);
        }
    }
    void TeleporterActivation()
    {
        //Activates the teleporter by running it
        isRunning = true;
    }
}
