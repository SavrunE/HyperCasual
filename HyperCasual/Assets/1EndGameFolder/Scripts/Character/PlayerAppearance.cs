//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

////=============================================
////    PLAYER CONTROLLER
////=============================================
//public class PlayerAppearance : MonoBehaviour
//{
//    // Публичные объекты
//    [Header("Player Body Parts")]
//    public GameObject[] hairs;
//    public GameObject[] faces;
//    public GameObject[] special;

//    // Инициализация компонента
//    void Start()
//    {
//    }

//    // Апдейт фрейма
//    void Update()
//    {
//    }

//    // Обновить на игроке его части тела
//    public void updateParts()
//    {
//        // Работа с волосами
//        for (int i = 0; i < hairs.Length; i++)
//        {
//            if (i == NetworkManager.instance.auth.player_data.profile_data.body.hairs)
//            {
//                hairs[i].SetActive(true);
//            }
//            else
//            {
//                hairs[i].SetActive(false);
//            }
//        }

//        /* TODO: Тоже самое для других частей тела */
//    }
//}