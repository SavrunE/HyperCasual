using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coin", menuName = "Spawner/Coin")]
public class CoinData : ScriptableObject
{
    public string Name;
    public string Description;

    public Sprite Sprite;

    public int Value;

    public float MoveSpeed;
    public Vector2 DirectionMove;
}