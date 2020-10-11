using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsText : MonoBehaviour
{
    [SerializeField] Text coinText;
    void Start()
    {
        Character.Instance.OnCoinTake += OnPlayerCoinsChanged;

    }
    public void OnPlayerCoinsChanged(int coins)
    {
        coinText.text = coins.ToString();
    }
}
