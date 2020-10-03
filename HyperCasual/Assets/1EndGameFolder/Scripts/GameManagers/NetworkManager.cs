using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//=============================================
//    Network Manager
//=============================================
public class NetworkManager : MonoBehaviour
{
    // Публичные параметры
    public static NetworkManager instance = null; // Экземпляр менеджера
    public static string server = "https://mysite.com/api"; // URL сервера

    // Публичные ссылки на подобъекты менеджера
    public APIAuth auth; // Объект авторизации
    public APIUtils utils; // Объект утилит

    // Инициализация менеджера
    void Awake()
    {
        // Проверяем экземпляр объекта
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        // Даем понять движку, что его не нужно уничтожать
        DontDestroyOnLoad(gameObject);

        // Инициализируем нашего менеджера
        InitializeManager();
    }

    // Инициализация менеджера
    public void InitializeManager()
    {
        auth = new APIAuth(server + "/auth/"); // Подключаем подобъект авторизации
        utils = new APIUtils(server + "/utils/"); // Подключаем подобъект утилит
    }
}

//=============================================
//    API Auth Manager
//=============================================
public class APIAuth
{
    // Приватные параметры
    private string controllerURL = ""; // Controller URL

    //=============================================
    //   Конструктор объекта
    //=============================================
    public APIAuth(string controller)
    {
        controllerURL = controller;
    }

    //=============================================
    //    Метод для авторизации
    //=============================================
    public delegate void OnLoginComplete();
    public delegate void OnLoginError(string message);
    public IEnumerator SingIn(string login, string password, OnLoginComplete complete, OnLoginError error)
    {
        // Формируем данные для отправки
        WWWForm data = new WWWForm();
        data.AddField("login", login);
        data.AddField("password", password);
        data.AddField("lang", LangManager.language);

        // Отправляем запрос на сервер
        WWW request = new WWW(controllerURL + "/login/", data);
        yield return request;

        // Обрабатываем ответ сервера
        if (request.error != null)
        { // Ошибка отправки запроса
            error("Не удалось отправить запрос на сервер");
        }
        else
        { // Ошибок при отправке не было
            try
            {
                responceModel responce = JsonUtility.FromJson<responceModel>(request.text);
                if (responce.complete)
                {
                    complete(); // Вызываем Success Callback
                }
                else
                {
                    error(responce.message); // Do error
                    Debug.Log("API Error: " + responce.message);
                }
            }
            catch
            {
                error("Не удалось обработать ответ сервера");
                Debug.Log("Ошибка обработки ответа сервера. Данные ответа: " + request.text);
            }
        }
    }


    /* TODO: Здесь будут остальные методы для работы с авторизацией */
}

//=============================================
//    Теперь создаем подобъект утилит по образу и подобию авторизации
//=============================================
public class APIUtils
{
    private string controllerURL = "";

    // Аналогичный конструктор класса
    public APIUtils(string controller)
    {
        controllerURL = controller;
    }

    //=============================================
    //    Проверка версии клиента игры
    //=============================================
    public delegate void OnClientVersionChecked();
    public delegate void OnClientVersionError(string message);
    public IEnumerator CheckClientVersion(string version, OnClientVersionChecked complete, OnClientVersionError error)
    {
        // Создаем данные
        WWWForm data = new WWWForm();
        data.AddField("version", version);
        data.AddField("lang", LangManager.language);

        // Отправляем запрос
        WWW request = new WWW(controllerURL + "/checkVersion/", data);
        yield return request;

        // Обрабатываем ответ
        if (request.error != null)
        {
            error("Не удалось отправить запрос на сервер");
        }
        else
        {
            try
            {
                responceModel responce = JsonUtility.FromJson<responceModel>(request.text);
                if (responce.complete)
                {
                    complete();
                }
                else
                {
                    error(responce.message);
                    Debug.Log("API Error: " + responce.message);
                }
            }
            catch
            {
                error("Не удалось обработать ответ сервера");
                Debug.Log("Ошибка обработки ответа сервера. Данные ответа: " + request.text);
            }
        }
    }
}
//// Простая функция для вызова проверки
//public void checkMyGame()
//{
//    StartCoroutine(NetworkManager.instance.utils.CheckClientVersion(Application.version,
//    (() =>
//    { // Если все прошло успешно
//        /* TODO: Здесь мы выполняем загрузку игры после успешной проверки версии игры */
//    }), ((string msg) =>
//    { // Если возникла ошибка
//        /* TODO: Здесь мы просим пользователя обновить версию клиента игры */
//        Debug.Log(msg);
//    })));
//}