using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    GameObject obj;
    public float minWait;
    public float maxWait;
    public float rangex;
    public float rangey;
    public float rangez;
    public float projectileSpeed = 20;
    public GameObject bullet;
    // Start is called before the first frame update
    public GameObject anchor;

    public Camera cam;

    private Vector3 destination;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    //Fixed Update
    void FixedUpdate()
    {
        if (Input.GetAxisRaw("Fire1") >= 0.5)
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;

        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        InstantiateProjectile();
    }

    void InstantiateProjectile(){
        var projectileObj = Instantiate(bullet,transform.position,Quaternion.identity);
    
        projectileObj.GetComponent<Rigidbody>().velocity=(destination-transform.position).normalized*projectileSpeed;
    }

    // private IEnumerator SpawnSingle()
    // {
    //     yield return new WaitForSeconds(Random.Range(minWait, maxWait));
    //     obj = ObjectPool.SharedInstance.GetPooledObject();
    //     if (obj != null)
    //     {
    //         //obj.transform.position = Randomizer(transform.position);  // Radomize starting position
    //         obj.transform.position = transform.position;
    //         obj.transform.rotation = Quaternion.identity;
    //         obj.SetActive(true);
    //     }
    // }
    // // Looping Spawn
    // private IEnumerator SpawnLoop()
    // {
    //     yield return new WaitForSeconds(Random.Range(minWait, maxWait));
    //     obj = ObjectPool.SharedInstance.GetPooledObject();
    //     if (obj != null)
    //     {
    //         //obj.transform.position = Randomizer(transform.position);  // Radomize starting position
    //         obj.transform.position = transform.position;
    //         obj.transform.rotation = Quaternion.identity;
    //         obj.SetActive(true);
    //     }
    //     StartCoroutine("Spawn");
    // }


    //Randomize Spawing Location
    // Vector3 Randomizer(Vector3 v)
    // {
    //     float randx = Random.Range(v.x - rangex, v.x + rangex);
    //     float randy = Random.Range(v.y - rangey, v.y + rangey);
    //     float randz = Random.Range(v.z - rangex, v.z + rangex);
    //     return new Vector3(randx, randy, randz);
    // }
}
