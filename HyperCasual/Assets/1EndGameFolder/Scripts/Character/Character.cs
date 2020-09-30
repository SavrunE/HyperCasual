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

    private bool lookOnLeft  = false;
    private bool isGrounded = true;
    private bool canJump = true;
    private float jumpDelay = 1f;
    private float checkJumpDelay = 1f;
    [SerializeField] private int extraJumpValue = 2;
    private int extraJump;
    private bool jumpDirection;

    Animator Animator;
    Rigidbody2D body;

    [SerializeField] Transform groundCheker;

    void Start()
    {
        Animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        extraJump = extraJumpValue;
        /// заменить на куратину
        if (checkJumpDelay > 0)
            checkJumpDelay -= Time.fixedDeltaTime;
        else
            canJump = true;
        ///
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            checkJumpDelay = jumpDelay;
            canJump = false;
            Jump();

        }
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheker.position, 0.3f);

        isGrounded = colliders.Length > 1;
        /// сделать через касание ножками, каждый фрейм вызывать - это бред
        if (isGrounded)
        {
            extraJump = extraJumpValue;
            Animator.SetBool("IsGrounded", true);
        }
        else
        {
            SeparatorAnimation(body.velocity.y, "JumpUp");
            Animator.SetBool("IsGrounded", false);
            Animator.SetBool("Moving", false);
        }
    }

    private void SeparatorAnimation(float velocityDirection, string boolValue)
    {
        if (velocityDirection > 0)
        {
            Animator.SetBool(boolValue, true);
        }
        else
        {
            Animator.SetBool(boolValue, false);
        }

    }
    private void Move()
    {
        body.velocity = new Vector2(Joystick.Horizontal * MoveSpeed, body.velocity.y);
        if (body.velocity.x == 0)
        {
            Animator.SetBool("Moving", false);
        }
        else
            Animator.SetBool("Moving", true);
    }
    private void Jump()
    {
        Vector2 moveInput = new Vector2(Joystick.Horizontal, Joystick.Vertical);
        body.AddForce(moveInput.normalized * JumpPower, ForceMode2D.Impulse);
        canJump = false;
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
