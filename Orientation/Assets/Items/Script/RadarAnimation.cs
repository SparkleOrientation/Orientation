using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarAnimation : MonoBehaviour
{
    // Start is called before the first frame update
  
    void Start()
    {
        transform.LeanScale(new Vector2(0,0), 0.8f).setEaseInQuad().setLoopPingPong();
        transform.LeanScale(new Vector2(40,40), 1f).setEaseInQuad().setLoopPingPong();

    }

    // Update is called once per frame
    
    void Update()
    {
    }
}
