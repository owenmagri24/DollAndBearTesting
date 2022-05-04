using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ObjectPaneling : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTargetGroup cinemachineTargetGroup;
    private bool firstTime = true;
    [SerializeField] private Transform target;
    int playerLayer;

    private void Start() {
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineTargetGroup = FindObjectOfType<CinemachineTargetGroup>();
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!firstTime) return;
        firstTime = false;

        if(other.gameObject.layer == playerLayer)
        {
            Debug.Log("Triggered with Player");
            StartCoroutine(panelToTarget(other.transform));
        }
    }

    IEnumerator panelToTarget(Transform player)
    {
        cinemachineVirtualCamera.m_Follow = cinemachineTargetGroup.transform;

        for (int i = 0; i < cinemachineTargetGroup.m_Targets.Length; i++)
        {
            cinemachineTargetGroup.m_Targets[i].weight = 0;
        }
        
        cinemachineTargetGroup.AddMember(target, 1, 0);
        yield return new WaitForSeconds(2f);
        cinemachineTargetGroup.RemoveMember(target);

        for (int i = 0; i < cinemachineTargetGroup.m_Targets.Length; i++)
        {
            cinemachineTargetGroup.m_Targets[i].weight = 1;
        }

        cinemachineVirtualCamera.m_Follow = player;
    }
}
