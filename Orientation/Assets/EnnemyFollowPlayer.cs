using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnnemyFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public GameObject bullet;
    public GameObject bulletParent;
    public Transform player;
    public Vector3 posDepart;
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[Random.Range(0,GameObject.FindGameObjectsWithTag("Player").Length)].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

            if (Math.Abs(player.position.x) < Math.Abs(posDepart.x * 1.3) &&
                Math.Abs(player.position.x) > Math.Abs(posDepart.x / 1.3) &&
                Math.Abs(player.position.y) < Math.Abs(posDepart.y * 1.3) &&
                Math.Abs(player.position.y) > Math.Abs(posDepart.y / 1.3))
            {
                if (distanceFromPlayer < lineOfSite )
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, posDepart, speed * Time.deltaTime);
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(this.transform.position, posDepart, speed * Time.deltaTime);
            }
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3((float) (transform.position.x*0.3),(float) (transform.position.y*0.3),0));
    }
}
