using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float radius = 3f;
    [SerializeField] Vector2 explosionForce = new Vector2(10000f,100f);
    BoxCollider2D myBoxCollider2D;
    Animator myAnimator;   
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();  
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    void ExplodeBomb()
    {
      Collider2D playerCollider = Physics2D.OverlapCircle(transform.position,radius, LayerMask.GetMask("Player"));  
      
      if (playerCollider)
      {
        print("alr");
        playerCollider.GetComponent<Rigidbody2D>().AddForce(explosionForce);
        playerCollider.GetComponent<Player>().PlayerHit();
      }
    }
      
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Foreground"){return;}
        myAnimator.SetTrigger("Burn");
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
