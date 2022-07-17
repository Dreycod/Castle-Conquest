using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    [SerializeField] float enemyRunSpeed = 5f;
    [SerializeField] AudioClip DyingSFX;
    Rigidbody2D enemyRigidBody;
    CapsuleCollider2D enemyCapsuleCollider;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        enemyCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyCapsuleCollider.enabled)
        {
            EnemyMovement();
        } 
    }

    public void Dying()
    {
        myAnimator.SetTrigger("Die");
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        enemyRigidBody.bodyType = RigidbodyType2D.Static;
        
        StartCoroutine(DeleteBody());
    }

    IEnumerator DeleteBody()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        FlipSprite();
    }
    private void EnemyMovement()
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

    void EnemyDyingSFX()
    {
        AudioSource.PlayClipAtPoint(DyingSFX, Camera.main.transform.position);  
    }
}
