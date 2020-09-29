using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event", order = 52)] // 1
public class GameEvent : ScriptableObject // 2
{
    private List<GameEventListener> listeners = new List<GameEventListener>(); // 3

    public void Raise() // 4
    {
        for (int i = listeners.Count - 1; i >= 0; i--) // 5
        {
            listeners[i].OnEventRaised(); // 6
        }
    }

    public void RegisterListener(GameEventListener listener) // 7
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener) // 8
    {
        listeners.Remove(listener);
    }
    
}
//Добавляет GameEvent в качестве ассета в Asset Menu.
//GameEvent является Scriptable Object, поэтому должен наследоваться от ScriptableObject.
//Список GameEventListeners, которые будут подписаны на GameEvent.
//Метод для вызова всех подписчиков GameEvent.
//Последний подписанный GameEventListener будет первым вызываемым (последним пришёл, первым вышел).
//Вызов каждого UnityEvent GameEventListeners.
//Метод, позволяющий GameEventListeners подписаться на этот GameEvent.
//Метод, позволяющий GameEventListeners отписываться от этого GameEvent.
