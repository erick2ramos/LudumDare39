using UnityEngine;
using System.Collections.Generic;

public class EventFactory : MonoBehaviour
{
    public GameEvent[] allEvents;
    Stack<int> runEvents;

    public void GenerateNewRunEvents(int runEventsAmount)
    {
        runEvents = new Stack<int>();
        float chanceToEnter = (float)runEventsAmount / allEvents.Length;
        for (int i = 0; i < allEvents.Length; i++)
        {
            if(Random.value < chanceToEnter)
            {
                runEvents.Push(i);
            }
            chanceToEnter = ((float)runEventsAmount - runEvents.Count) / (allEvents.Length - i + 1);
        }
    }

    public GameEvent NextEvent()
    {
        if (runEvents.Count <= 0)
            return null;
        return allEvents[runEvents.Pop()];
    }
}
