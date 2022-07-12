using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    [SerializeField] float enemyRunSpeed = 5f;
    Rigidbody2D enemyRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        FlipSprite();
    }
    private void Run()
    {
        if (IsFacingLeft())
        {
            enemyRigidBody.velocity = new Vector2(-enemyRunSpeed, 0f);
        }
        else {
            enemyRigidBody.velocity = new Vector2(enemyRunSpeed, 0f);
        }
    }
    private void FlipSprite()
    {
      
            transform.localScale = new Vector2(Mathf.Sign(enemyRigidBody.velocity.x), 1f);

    }

    private bool IsFacingLeft()
    {
        return transform.localScale.x > 0;
    }
}
