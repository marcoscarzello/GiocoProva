using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VAControl : MonoBehaviour
{
    Rigidbody2D rb;
    float moveSpeed;
    public GameObject explosion;
    Vector2 direction;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(1f, 3f);
    }

    void Update()
    {
        MoveVirus();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                Destroy(gameObject);
            }
        }
    }


    void MoveVirus()
    {
        direction = new Vector2 (0f,0f);
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
    }
}
