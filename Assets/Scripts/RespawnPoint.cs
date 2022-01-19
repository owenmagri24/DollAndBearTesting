using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public Vector2 location;
    void Start()
    {
        location = new Vector2(transform.position.x , transform.position.y);
    }

}
