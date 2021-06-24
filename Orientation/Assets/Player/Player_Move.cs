using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    [PunRPC] private bool daggerActive = false;
    [PunRPC] public bool gotDagger = false;
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
        if (!(itemWorld is null))
        {
            //toucher l'item
            
            if (!gotDagger)
            {
                foreach (var item in inventory.GetItemList())
                {
                    if (item.itemType == Item.ItemType.Dague) gotDagger = true;
                }
            }
            
            if (!(gotDagger && itemWorld.GetItem().itemType == Item.ItemType.Dague))
            {
                inventory.AddItem(itemWorld.GetItem());
                SoundManagerScript.PlaySound("getItem");
                if (itemWorld.item.itemType == Item.ItemType.Dague) gotDagger = true;
                if (itemWorld.item.itemType == Item.ItemType.Indice) // On trouve un indice donc on fait apparaitre les couteaux sur la minimap
                {
                    var dagueSurMapL = GameObject.FindGameObjectsWithTag("DagueMiniMap");
                        var dagueSurMap = dagueSurMapL.ToList();

                        if (dagueSurMap.Count > 0)
                        {
                            var randomdague = Random.Range(0, dagueSurMap.Count);
                            ;
                            GameObject rectTransformDague = dagueSurMap[randomdague];
                            Debug.Log((rectTransformDague.name) + rectTransformDague.tag);
                            if (photonView.isMine)
                            {
                                rectTransformDague.GetComponent<Renderer>().enabled = true;
                            }
                            dagueSurMap.Remove(dagueSurMap[randomdague]);
                        }
                }
                itemWorld.DestroySelf();
            }
        }

        if (collider.CompareTag("Ennemy"))
        {
            Kill();
        }

        if (collider.CompareTag("Zone"))
        {
            collider.gameObject.GetComponent<zone>().AI.GetComponent<EnnemyFollowPlayer>().player = transform;
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
        var runSound = false;
        if (photonView.isMine)
        {
            dir.x = Input.GetAxisRaw("Horizontal") ;
            dir.y = Input.GetAxisRaw("Vertical");

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                if (!SoundManagerScript.audioSrc.isPlaying)
                {
                    SoundManagerScript.PlaySound("walk");
                }
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                runSound = true;
                speed = 10;
                
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 3;
                runSound = false;
            }
            
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                if (!SoundManagerScript.audioSrc.isPlaying)
                {
                    if (runSound)
                    {
                        SoundManagerScript.PlaySound("run");

                    }
                    else
                    {
                        SoundManagerScript.PlaySound("walk");

                    }
                }
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (gotDagger)
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
                
            }
            if (dir.x != 0)
            {
                dir.y = 0;
            }
            SetParam(runSound);
        }
       
    }

    private void Shoot()
    {
        
        GameObject obj = PhotonNetwork.Instantiate(DaggerObject.name, new Vector2(FirePos.transform.position.x, FirePos.transform.position.y), Quaternion.identity, 0);
        
    }
    
    void SetParam(bool runSound)
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
        SoundManagerScript.PlaySound("kill");
        Destroy(this);
        this.gameObject.SetActive(false);
    }
    
}
