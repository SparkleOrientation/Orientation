using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class multi_cam : MonoBehaviour
{
    public Transform playerTransform;
    public CinemachineVirtualCamera vcam;
    public int depth = -20;
 
    // Update is called once per frame
    private void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if(playerTransform != null)
        {
            vcam.Follow = playerTransform;
        }
    }
 
    public void setTarget(Transform target)
    {
        playerTransform = target;
    }
}