using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float speed = 5f;
    Rigidbody2D rb;
    private Vector2 dir;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        dir.x = Input.GetAxisRaw("Horizontal") ;
        dir.y = Input.GetAxis("Vertical");
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }
}
