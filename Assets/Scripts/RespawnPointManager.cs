using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointManager : MonoBehaviour
{
    public List<RespawnPoint> RespawnPointsLists = new List<RespawnPoint>();

    public static RespawnPointManager instance;
    
    private void Awake() {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject); //if there is already an audio manager, destroy and return to not run any more code
            return;
        }
    }
}
