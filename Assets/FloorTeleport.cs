using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTeleport : MonoBehaviour
{
    private void FixedUpdate()
    {
        if(this.transform.position.y <= -750f)
        {
            Vector3 temp = this.transform.position;
            this.transform.position = new Vector3(temp.x, 1000f, temp.z);
        }
    }
}
