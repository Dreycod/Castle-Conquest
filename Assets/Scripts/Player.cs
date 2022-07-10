using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float JumpSpeed = 15f;
   // public float Velocity = 300.0f;
    Rigidbody2D myRigidBody2D;
    Animator myAnimator;
    BoxCollider2D myBoxCollider2D;
    PolygonCollider2D myPolygonCollider2D;

// Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myPolygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
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
}
