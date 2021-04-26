using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
public class Player_Move : NetworkBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 dir;
    private Inventory inventory;
    public GameObject DaguePrefab;
    public Transform DagueSpawn;

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

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        dir.x = Input.GetAxisRaw("Horizontal") ;
        dir.y = Input.GetAxisRaw("Vertical");

        if (dir.x != 0)
        {
            dir.y = 0;
        }
        
        SetParam();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var item in inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.Dague)
                {
                        Cmdkill();
                }
            }
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

    [Command]
    void Cmdkill()
    {
        var dague = (GameObject) Instantiate(DaguePrefab, DagueSpawn.position, DagueSpawn.rotation);
        NetworkServer.Spawn(dague);
        Destroy(dague, 3.0f);
    }
    public override void OnStartLocalPlayer()
    {
        var list = GameObject.FindGameObjectsWithTag("vcam");
        foreach (var var in list)
        {
            var.GetComponent<multi_cam>().setTarget(gameObject.transform);
        }
    }
}