using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData data;
    Rigidbody2D body;
    public static Action<GameObject> OnEnemyOverFly;
    [SerializeField] float distanceToDestroy = 20f;

    public float Attack { get { return data.Attack; } }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        body.velocity = data.DirectionMove * data.MoveSpeed * Time.fixedDeltaTime;
        if (Character.Instance.transform.position.magnitude > transform.position.magnitude + distanceToDestroy)
            OnEnemyOverFly(gameObject);
    }
    public void Init(EnemyData data)
    {
        this.data = data;
    }
}
