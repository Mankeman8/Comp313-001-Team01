using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    public float lifetimemax=30;
    public float lifetime=30;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("Timeout");
    }

    // Update is called once per frame
    void Update()
    {
        if(lifetime > 0)
        {
            lifetime = lifetime - Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
            lifetime = lifetimemax;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
            
    }

    IEnumerable Timeout()
    {
        yield return new WaitForSeconds(5);
        lifetime = lifetimemax;
        gameObject.SetActive(false);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision!=null)
        {
            lifetime = lifetimemax;
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
        //Debug.Log("This object is colliding");
    }
}
