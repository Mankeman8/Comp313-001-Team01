using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    public bool Activated = false;
    public bool isRunning = false;
    public bool levelDone = false;
    public int heightAdded = 1550;
    public float initialHeight = 0;
    public float maxHeight = 0;

    public GameObject image;

    // Start is called before the first frame update
    void Start()
    {
        //Setting up some variables before we start the game
        initialHeight = this.gameObject.transform.position.y;
        maxHeight = initialHeight + heightAdded;
        image = GameObject.Find("KeyboardButtonE");
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
                FindObjectOfType<GenerateEnemies>().spawn = false;
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

    private void OnTriggerEnter(Collider other)
    {
        //when player enters the radius, activate the image
        //only able to press it if the teleporter isn't running
        //and the level isn't done (a.k.a, only visible before activating it)
        if(other.CompareTag("Player") && !isRunning && !levelDone)
        {
            image.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //when player leaves radius, make image disappear
        if (other.CompareTag("Player"))
        {
            image.SetActive(false);
        }
    }
}
