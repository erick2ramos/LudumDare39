using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
    public string eventId;
    public string premise;
    public EventOption[] options;
    public CrewType crewEvent;
}

[System.Serializable]
public class EventOption
{
    public string option;
    public float successPercent;
    public string successText;
    public string failureText;
    public int successEnergy;
    public int failureEnergy;
}
