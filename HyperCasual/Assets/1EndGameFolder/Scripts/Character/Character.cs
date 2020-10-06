﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    public static Character Instance = null;
    public Joystick Joystick;
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField] private float moveSpeed = 350f;
    public float JumpPower { get { return jumpPower; } }
    [SerializeField] private float jumpPower = 3.5f;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float health;
    public float Health { get { return health; } }

    private bool lookOnLeft = false;
    private bool isGrounded = true;
    private bool canJump = true;
    private float jumpDelay = 0.5f;
    private bool canCheckGround = true;

    private int extraJump = 2;
    private int extraJumpCheck;

    [SerializeField] private float timeAfterDead = 1f;

    Animator animator;
    Rigidbody2D body;

    [SerializeField] Transform groundCheker;

    private void Awake()
    {
        if (Instance == null)
        { // Экземпляр менеджера был найден
            Instance = this; // Задаем ссылку на экземпляр объекта
        }
        else if (Instance == this)
        { // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (isGrounded)
            Move();
        else
            SeparatorAnimation(body.velocity.y, "JumpUp");

        if (lookOnLeft == true && body.velocity.x > 0)
            Flip();
        else if (lookOnLeft == false && body.velocity.x < 0)
            Flip();
    }
    private void Update()
    {
        CheckGround();

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }
    }
    IEnumerator JumpTimer()
    {
        canJump = false;
        canCheckGround = false;
        yield return new WaitForSeconds(jumpDelay / 2);
        canCheckGround = true;

        yield return new WaitForSeconds(jumpDelay / 2);
        extraJumpCheck--;
        if (extraJumpCheck > 0)
            canJump = true;
    }
    private void Jump()
    {
        Vector2 moveInput = new Vector2(Joystick.Horizontal, Joystick.Vertical);
        body.AddForce(moveInput.normalized * JumpPower, ForceMode2D.Impulse);
        animator.SetBool("IsGrounded", false);
        animator.SetBool("Moving", false);
        StartCoroutine(JumpTimer());
    }
    private void Move()
    {
        body.velocity = new Vector2(Joystick.Horizontal * MoveSpeed * Time.fixedDeltaTime, body.velocity.y);
        if (body.velocity.x == 0)
        {
            animator.SetBool("Moving", false);
        }
        else
            if (isGrounded && canCheckGround)
            animator.SetBool("Moving", true);
    }
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheker.position, 0.1f);

        isGrounded = colliders.Length > 1;
        /// сделать через касание ножками, каждый фрейм вызывать - это бред
        /// UPD странно, все делают так...
        if (isGrounded && canCheckGround)
        {
            animator.SetBool("IsGrounded", true);
            canJump = true;
            extraJumpCheck = extraJump;
        }
    }
    private void SeparatorAnimation(float velocityDirection, string boolValue)
    {
        if (velocityDirection > 0)
            animator.SetBool(boolValue, true);
        else
            animator.SetBool(boolValue, false);
    }
    private void Flip()
    {
        lookOnLeft = !lookOnLeft;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    private void TakeDamage(float damage)
    {
        if (damage > 0)
            health -= damage;
        else
            health += damage;
    }
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(timeAfterDead);
        Debug.Log("HAHA u dead");
        transform.position = new Vector2(0,0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var trigger = collision.gameObject;
        if (Spawner.Enemies.ContainsKey(trigger)) 
        {
            float damage = Spawner.Enemies[trigger].Attack;
            TakeDamage(damage);
            if (Health <= 0)
            {
                StartCoroutine(Dead());
            }
            else
                Debug.Log("take " + damage + " damage");
        }
    }
}
