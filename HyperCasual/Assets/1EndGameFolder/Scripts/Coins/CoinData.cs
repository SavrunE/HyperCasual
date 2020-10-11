using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Coin", menuName = "Spawner/Coin")]
public class CoinData : SpawnData
{
    public int Value;

    public override int Coins()
    {
        return Value;
    }
}