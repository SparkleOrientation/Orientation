using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Photon.MonoBehaviour
{
    [PunRPC]
    public bool daggerActive = false;
    public GameObject Player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (daggerActive)
        {
            if (!photonView.isMine) 
                return;
            PhotonView target = collision.gameObject.GetComponent<PhotonView>();
            if (target != null && (!target.isMine || target.isSceneView))
            {
                if (target.gameObject.GetComponent<Player_Move>().gotDagger)
                {
                    SoundManagerScript.PlaySound("kill");
                    target.gameObject.GetComponent<PhotonView>().RPC("Kill",PhotonTargets.AllBuffered);
                }
                else
                {
                    Player.gameObject.SetActive(false);
                }
            }
        }
    }

    public void DestroySelf()
    {
        this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
    }

    [PunRPC] public void SetActive(bool b)
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled= b;
        daggerActive = b;
    }
    
}
