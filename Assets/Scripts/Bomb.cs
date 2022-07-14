using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float radius = 3f;
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
      Physics2D.OverlapCircle(transform.position,radius, LayerMask.GetMask("Player"));  
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.name);
        if(other.name == "Foreground"){return;}
        myAnimator.SetTrigger("Burn");
    }

    void DestroyBomb()
    {
        Destroy(gameObject);
    }
}
