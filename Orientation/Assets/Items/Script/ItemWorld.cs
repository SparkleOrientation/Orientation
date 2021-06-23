using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld: MonoBehaviour
{
    public Item item;
    public Item GetItem()
    {
        return item;
    }
    
    [PunRPC] public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
 