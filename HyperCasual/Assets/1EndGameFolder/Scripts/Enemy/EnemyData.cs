using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "Spawner/Enemies")]
public class EnemyData : SpawnData
{
    public int Attack;
    public int Defence;
    public int Health;
    public float TimeToDestroy;
    public override float Damage()
    {
        return Attack;
    }
}
