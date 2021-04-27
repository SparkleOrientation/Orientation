using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : Photon.MonoBehaviour
{
    public PhotonView photonView;
    public GameObject PlayerCamera;
    public GameObject PlayerVCam;
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 dir;
    private Inventory inventory;
    public SpriteRenderer sr;
    public bool IsGrounded = false;

    [SerializeField] private UIInventory uiInventory;

    private void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        sr = GetComponent<SpriteRenderer>();
        if (photonView.isMine)
        {
            PlayerCamera.SetActive(true);
            PlayerVCam.SetActive(true);
        }
    }

    private void OnTriggerEnter2D (Collider2D collider)
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            //toucher l'item
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
            RandomSpawn.enableDague();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (photonView.isMine)
        {
            PlayerCamera.SetActive(true);
        }
    }

    private void Update()
    {
        if (photonView.isMine)
        {
            dir.x = Input.GetAxisRaw("Horizontal") ;
            dir.y = Input.GetAxisRaw("Vertical");

            if (dir.x != 0)
            {
                dir.y = 0;
            }
            SetParam();
        }
       
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
