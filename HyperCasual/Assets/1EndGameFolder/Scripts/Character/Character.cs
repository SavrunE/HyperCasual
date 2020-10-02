using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    public Joystick Joystick;
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField] private float moveSpeed = 5f;
    public float JumpPower { get { return jumpPower; } }
    [SerializeField] private float jumpPower = 3.5f;

    private bool lookOnLeft = false;
    private bool isGrounded = true;
    private bool canJump = true;
    private float jumpDelay = 1f;
    private int extraJump = 2;
    private int extraJumpCheck;
    private bool jumpDirection;

    Animator animator;
    Rigidbody2D body;

    [SerializeField] Transform groundCheker;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (isGrounded)
            Move();

    }
    private void Update()
    {
        if (lookOnLeft == true && body.velocity.x > 0)
            Flip();
        else if (lookOnLeft == false && body.velocity.x < 0)
            Flip();

        CheckGround();
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            StartCoroutine(JumpWaiter());
            Jump();
        }
    }
    IEnumerator JumpWaiter()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpDelay);
        extraJumpCheck--;
        if (extraJumpCheck > 0)
            canJump = true;

    }


    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheker.position, 0.1f);

        isGrounded = colliders.Length > 1;
        /// сделать через касание ножками, каждый фрейм вызывать - это бред
        /// UPD странно, все так делают...
        if (isGrounded)
        {
            canJump = true;
            extraJumpCheck = extraJump;
            animator.SetBool("IsGrounded", true);
        }
        else
        {
            SeparatorAnimation(body.velocity.y, "JumpUp");
            animator.SetBool("IsGrounded", false);
            animator.SetBool("Moving", false);
        }
    }

    private void SeparatorAnimation(float velocityDirection, string boolValue)
    {
        if (velocityDirection > 0)
        {
            animator.SetBool(boolValue, true);
        }
        else
        {
            animator.SetBool(boolValue, false);
        }

    }
    private void Move()
    {
        body.velocity = new Vector2(Joystick.Horizontal * MoveSpeed, body.velocity.y);
        if (body.velocity.x == 0)
        {
            animator.SetBool("Moving", false);
        }
        else
            animator.SetBool("Moving", true);
    }
    private void Jump()
    {
        Vector2 moveInput = new Vector2(Joystick.Horizontal, Joystick.Vertical);
        body.AddForce(moveInput.normalized * JumpPower, ForceMode2D.Impulse);
        
        isGrounded = false;

    }
    private void Flip()
    {
        lookOnLeft = !lookOnLeft;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
