using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public Joystick Joystick;
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField] private float moveSpeed = 5f;
    public float JumpPower { get { return jumpPower; } }
    [SerializeField] private float jumpPower = 10f;

    private bool isGrounded = true;
    private bool canJump = true;
    private float jumpDelay = 1f;
    private float checkJumpDelay = 1f;



    [SerializeField] Transform groundCheker;
    Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheker.position,0.1f);

        isGrounded = colliders.Length > 1;
    }

    private void Move()
    {
        body.velocity = new Vector2(Joystick.Horizontal * MoveSpeed , body.velocity.y);
    }
    private void Jump()
    {
        Vector2 moveInput = new Vector2(Joystick.Horizontal, Joystick.Vertical);
        body.AddForce(moveInput.normalized * JumpPower, ForceMode2D.Impulse);
        canJump = false;
    }
}
