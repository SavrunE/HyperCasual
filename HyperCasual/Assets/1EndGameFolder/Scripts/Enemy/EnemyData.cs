using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyData : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    [SerializeField]
    private GameEvent OnSwordSelected; // 1

    private void OnMouseDown()
    {
        OnSwordSelected.Raise(); // 2
    }

}
//Генерацию Game Event при выборе меча.
//Генерацию события при нажатии на меч.