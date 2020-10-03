using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//   Audio Manager
//=============================================
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null; // Экземпляр объекта
    public static bool music = true; // Параметр доступности музыки
    public static bool sounds = true; // Параметр доступности звуков

    // Метод, выполняемый при старте игры
    void Start()
    {
        // Теперь, проверяем существование экземпляра
        if (instance == null)
        { // Экземпляр менеджера был найден
            instance = this; // Задаем ссылку на экземпляр объекта
        }
        else if (instance == this)
        { // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }

        // Теперь нам нужно указать, чтобы объект не уничтожался
        // при переходе на другую сцену игры
        DontDestroyOnLoad(gameObject);

        // И запускаем собственно инициализатор
        InitializeManager();
    }

    // Метод инициализации менеджера
    private void InitializeManager()
    {
        // Здесь мы загружаем и конвертируем настройки из PlayerPrefs
        music = System.Convert.ToBoolean(PlayerPrefs.GetString("music", "true"));
        sounds = System.Convert.ToBoolean(PlayerPrefs.GetString("sounds", "true"));
    }

    // Метод для сохранения текущих настроек
    public static void saveSettings()
    {
        PlayerPrefs.SetString("music", music.ToString()); // Применяем параметр музыки
        PlayerPrefs.SetString("sounds", sounds.ToString()); // Применяем параметр звуков
        PlayerPrefs.Save(); // Сохраняем настройки
    }
}