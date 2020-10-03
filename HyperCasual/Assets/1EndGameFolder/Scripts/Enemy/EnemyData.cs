using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Default")]
public class EnemyData : ScriptableObject
{
    public string Name;
    public string Description;

    public Sprite Sprite;

    public int Attack;
    public int Defence;
    public int Health;
    public int Value;

    public float MoveSpeed;
    public Vector2 DirectionMove;
    

    public void PrintInfo()
    {
        Debug.Log("Name: " + Name + " - " + Description);
    }
}
