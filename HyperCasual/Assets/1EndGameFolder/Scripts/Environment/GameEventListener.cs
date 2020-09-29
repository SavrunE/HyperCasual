using UnityEngine;
using UnityEngine.Events; // 1

public class GameEventListener : MonoBehaviour
{
    [SerializeField]
    private GameEvent gameEvent; // 2
    [SerializeField]
    private UnityEvent response; // 3

    private void OnEnable() // 4
    {
        gameEvent.RegisterListener(this);
    }

    private void OnDisable() // 5
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised() // 6
    {
        response.Invoke();
    }
}
//Требование использования класса UnityEvent.
//GameEvent, на который будет подписан этот GameEventListener.
//Отклик UnityEvent, который будет вызываться, при генерации событием GameEvent этого GameEventListener.
//Привязка GameEvent к GameEventListener, когда этот GameObject включен.
//Отвязка GameEvent от GameEventListener, когда этот GameObject отключен.
//Вызывается при генерации GameEvent, приводящей к вызову слушателем GameEventListener события UnityEvent.