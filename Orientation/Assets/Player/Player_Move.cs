using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 dir;
    private Inventory inventory;

    [SerializeField] private UIInventory uiInventory;

    private void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    private void OnTriggerEnter2D (Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            //toucher l'item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal") ;
        dir.y = Input.GetAxisRaw("Vertical");

        if (dir.x != 0)
        {
            dir.y = 0;
        }
        
        SetParam();
    }

    void SetParam()
    {
        transform.Translate(dir.x*speed*Time.fixedDeltaTime,dir.y*speed*Time.fixedDeltaTime,0);
        anim.SetFloat("moveX", dir.x);
        anim.SetFloat("moveY", dir.y);
        if (dir.x == 1|| dir.x == -1 || dir.y == 1 || dir.y ==-1)
        {
            anim.SetFloat("lastMoveX",dir.x);
            anim.SetFloat("lastMoveY",dir.y);
        }

    }
}
