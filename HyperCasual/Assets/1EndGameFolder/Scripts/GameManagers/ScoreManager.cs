using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoresManager : MonoBehaviour
{
	public static ScoresManager Instance { get; private set; }
	public int Scores;
	public void Awake()
	{
		Instance = this;
	}
}