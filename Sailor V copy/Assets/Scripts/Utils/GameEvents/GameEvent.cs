using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new();

    public void Raise(Component sender = null, object data = null)
    {
        // Debug.Log($"<color=pink>Event raised: {sender}, {data}</color>");
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised(sender, data);
        }

    }
    public void RegisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener)) return;

        listeners.Add(listener);
    }
    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
