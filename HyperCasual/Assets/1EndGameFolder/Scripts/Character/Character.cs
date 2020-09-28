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
    [SerializeField] private float jumpPower = 400f;
    Vector2 moveInput;

    private bool IsGrounded = true;

    Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        body.AddForce(body.position + new Vector2(Joystick.Horizontal, 0) * MoveSpeed * Time.fixedDeltaTime);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            moveInput = new Vector2(Joystick.Horizontal, Joystick.Vertical);
            body.AddForce(moveInput.normalized * JumpPower, ForceMode2D.Impulse);
           
        }
    }
}
