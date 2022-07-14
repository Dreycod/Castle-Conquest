using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float JumpSpeed = 15f;
    [SerializeField] float ClimbSpeed = 10f;
    [SerializeField] float AttackRadius = 3f;
    [SerializeField] Vector2 hitKick = new Vector2(50f,50f);
    [SerializeField] Transform HurtBox;

   // public float Velocity = 300.0f;
    Rigidbody2D myRigidBody2D;
    Animator myAnimator;
    BoxCollider2D myBoxCollider2D;
    PolygonCollider2D myPolygonCollider2D;

    float StartingGravityScale;
    bool IsHurting = false;
// Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myPolygonCollider2D = GetComponent<PolygonCollider2D>();
        StartingGravityScale = myRigidBody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!IsHurting)
        {
            Run();
            Jump();
            Climb();
            Attack();
            if(myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                PlayerHit();  
            }
        }
    }

    private void Attack()
    {
       if(CrossPlatformInputManager.GetButtonDown("Fire1"))
       {
         myAnimator.SetTrigger("Attacking");
         Collider2D[] Enemies = Physics2D.OverlapCircleAll(HurtBox.position, AttackRadius, LayerMask.GetMask("Enemy"));
         foreach (Collider2D enemy in Enemies)
         {
            enemy.GetComponent<Enemy>().Dying();
         }
       } 
    }
    public void PlayerHit()
    {
        myRigidBody2D.velocity = hitKick * new Vector2(-transform.localScale.x, 1f);

        myAnimator.SetTrigger("Hitting");
        IsHurting = true; 
        
        StartCoroutine(StopHurting());
    }    
    
    IEnumerator StopHurting()
    {
        yield return new WaitForSeconds(2f);
        IsHurting = false;
    }
    private void Climb()
    {
        int ClimbLayer = LayerMask.GetMask("Climb");
        bool IsTouchingClimb = myBoxCollider2D.IsTouchingLayers(ClimbLayer);

        if (IsTouchingClimb) { 
            myAnimator.SetBool("Climbing",true);
            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");

            Vector2 PlayerVerticalVelocity = new Vector2(myRigidBody2D.velocity.x, controlThrow * ClimbSpeed);
            myRigidBody2D.gravityScale = 0f;
            myRigidBody2D.velocity = PlayerVerticalVelocity;
        }
        else{
             myAnimator.SetBool("Climbing",false); 
            myRigidBody2D.gravityScale = StartingGravityScale;
        }
    }   

    private void ChangingToClimbingState()
    {
        bool Climbing = Mathf.Abs(myRigidBody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing",Climbing);
    }
    private void Jump()
    {
        int GroundLayer = LayerMask.GetMask("Ground");
        bool IsTouchingGround = myPolygonCollider2D.IsTouchingLayers(GroundLayer);

        if (!IsTouchingGround){return;};

        bool IsJumping = CrossPlatformInputManager.GetButtonDown("Jump");

        if (IsJumping)
        {
            Vector2 jumpVelocity = new Vector2(myRigidBody2D.velocity.x, JumpSpeed);
            myRigidBody2D.velocity = jumpVelocity;
        }
    }
    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2D.velocity.y);
        myRigidBody2D.velocity = playerVelocity;
        FlipSprite();
        ChangingToRunningState();
        // myRigidBody2D.velocity += new Vector2(Velocity, 0.0f);
    }

    private void ChangingToRunningState()
    {
        bool RunningHorizontaly = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running",RunningHorizontaly);
    }
    private void FlipSprite()
    {
        bool RunningHorizontaly = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        if (RunningHorizontaly){
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x), 1f);
        }

    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(HurtBox.position,AttackRadius);
    }
}
