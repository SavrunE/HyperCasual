using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class responceModel
{
	public bool complete = false; // Статус операции
	public string message = ""; // Сообщение об ошибке (в случае если complete = false)
}