using UnityEngine;
using System.Collections;


public abstract class Skill : ScriptableObject
{
    public string skillName;
    public string description;
    public int cost;
    public bool isPassive;
    public string flavorText;
    public bool onCooldown;

    public virtual void TriggerAbillity(Ship ship)
    {
        ship.baseShipE += cost;
    }
}