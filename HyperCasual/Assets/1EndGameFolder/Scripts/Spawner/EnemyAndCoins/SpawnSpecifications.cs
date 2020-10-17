using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpawnSpecifications : MonoBehaviour
{
    private SpawnData data;
    public SpawnData Data { get { return data; } }
    Rigidbody2D body;
    public static Action<GameObject> OnOverFly;
    [SerializeField] float distanceToDestroy = 100f;
    private float distance;

    private void Start()
    {
       body = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        body.velocity = data.DirectionMove * data.MoveSpeed * Time.fixedDeltaTime;
        distance = Vector3.Distance(Character.Instance.transform.position, transform.position);
        if (distance > distanceToDestroy)
        {
            OnOverFly(gameObject);
        }
    }
    public void Init(SpawnData data)
    {
        this.data = data;
    }
}
