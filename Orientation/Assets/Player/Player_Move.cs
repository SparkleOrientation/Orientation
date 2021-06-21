using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text PlayerNameText;
    public bool IsGrounded = false;
    public GameObject MinimapIcon;
    public GameObject DaggerObject;
    public Transform FirePos;
    public SpriteRenderer sr;
    public BoxCollider2D br;
    private bool daggerActive = false;

    [SerializeField] private UIInventory uiInventory;

    private void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        sr = GetComponent<SpriteRenderer>();
        br = GetComponent<BoxCollider2D>();
        if (photonView.isMine)
        {
            PlayerCamera.SetActive(true);
            PlayerVCam.SetActive(true);
            MinimapIcon.SetActive(true);
            PlayerNameText.text = PhotonNetwork.playerName;
        }
        else
        {
            PlayerNameText.text = photonView.owner.name;
            PlayerNameText.color = Color.cyan;
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                daggerActive = !daggerActive;
                if (daggerActive)
                {
                    DaggerObject.GetComponent<PhotonView>().RPC("SetActive", PhotonTargets.AllBuffered, true);
                }
                else
                {
                    DaggerObject.GetComponent<PhotonView>().RPC("SetActive", PhotonTargets.AllBuffered, false);
                }
            }
            if (dir.x != 0)
            {
                dir.y = 0;
            }
            SetParam();
        }
       
    }

    private void Shoot()
    {
        
        GameObject obj = PhotonNetwork.Instantiate(DaggerObject.name, new Vector2(FirePos.transform.position.x, FirePos.transform.position.y), Quaternion.identity, 0);
        
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
    
    [PunRPC] public void Kill()
    {
        Destroy(this);
        this.gameObject.SetActive(false);
    }
    
}
