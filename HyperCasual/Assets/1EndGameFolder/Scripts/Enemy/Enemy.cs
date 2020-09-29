using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Default")]
public class Enemy : ScriptableObject
{
    public string Name;
    public string Description;

    public Sprite Sprite;

    public int Attack;
    public int Defence;
    public int Health;
    public int Value;
    public void PrintInfo()
    {
        Debug.Log("Name: " + Name + " - " + Description);
    }
}
