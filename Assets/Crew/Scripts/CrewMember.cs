using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrewType
{
    None,
    Chef,
    Soldier,
    Pilot,
    Medic,
    Engineer,
    Diplomat
}

public class CrewMember : MonoBehaviour
{
    public string name;
    public CrewType type;
    public int energyPerTurn;
    public bool isOcupied;

    public Skill[] skills;
}