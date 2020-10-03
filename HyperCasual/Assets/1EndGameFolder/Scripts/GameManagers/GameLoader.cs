using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//	Game Classes Loader
//=============================================
public class GameLoader : MonoBehaviour
{
	// Ссылки на менеджеров
	public GameObject game_manager; // Game Base Manager
	public GameObject audio_manager; // Audio Manager
	public GameObject lang_manager; // Language Manager
	public GameObject net_manager; // Network Manager

	// Метод пробуждения объекта (перед стартом игры)
	void Awake()
	{
		// Инициализация игровой базы
		//if (GameBase.instance == null)
		//{
		//	Instantiate(game_manager);
		//}

		// Инициализация аудио менеджера
		if (AudioManager.instance == null)
		{
			Instantiate(audio_manager);
		}

		// Инициализация менеджера языков
		if (LangManager.instance == null)
		{
			Instantiate(lang_manager);
		}

		// Инициализация сетевого менеджера
		if (NetworkManager.instance == null)
		{
			Instantiate(net_manager);
		}
	}
}