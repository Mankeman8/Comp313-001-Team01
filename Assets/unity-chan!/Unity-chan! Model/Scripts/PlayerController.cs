using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //public CharacterController controller;
    //[SerializeField] float speed = 6f;
    GameObject player;
    GameObject apple;
    GameController gameController;
    //[SerializeField] Terrain curTerrain;
    //Vector3 playerPos;
    //Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        //playerPos = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerMovementControl();
        //playerPos = player.transform.position;
        //playerPos.y = Terrain.activeTerrain.SampleHeight(player.transform.position);
        //player.transform.position = playerPos ;
    }

    //private void PlayerMovementControl()
    //{
    //    float horizontal = Input.GetAxisRaw("Horizontal");
    //    float vertical = Input.GetAxisRaw("Vertical");
    //    Vector3 direction = new Vector3(horizontal,-9.8f,vertical).normalized;
    //    if(direction.magnitude >=0.1f){
    //        controller.Move(direction*speed*Time.deltaTime);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collision detected");
        Debug.Log(collision.collider.gameObject.tag);
        if (collision.collider.gameObject.tag == "NormalApple")
        {
            
            Debug.Log("Unity chan apple get!");
            gameController.Score += 50;
        }

        if (collision.collider.gameObject.tag == "EvilApple")
        {

            Debug.Log("Unity chan owie!!!");
            gameController.Lives -= 1;
        }
    }
}
