using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrewType
{
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

[CreateAssetMenu(menuName = "Crew/Skills/Chef Passive")]
public class ChefPassiveSkill : Skill
{
    public int reductionAmount;

    public override void TriggerAbillity(Ship ship)
    {
        base.TriggerAbillity(ship);
        ship.modCrewE -= reductionAmount;
    }
}

[CreateAssetMenu(menuName = "Crew/Skills/Pilot Passive")]
public class PilotPassiveSkill : Skill
{
    public int turnRateSaving;
    public int turnSkipAmount;

    public override void TriggerAbillity(Ship ship)
    {
        base.TriggerAbillity(ship);
        if (MainManager.Get.gameManager.TurnNumber % turnRateSaving == 0)
        {
            MainManager.Get.gameManager.TurnNumber += turnSkipAmount;
        }    
    }
}

[CreateAssetMenu(menuName = "Crew/Skills/Soldier Passive")]
public class SoldierPassiveSkill : Skill
{
    public override void TriggerAbillity(Ship ship)
    {
        base.TriggerAbillity(ship);

    }
}

[CreateAssetMenu(menuName = "Crew/Skills/Chef Active")]
public class ChefActiveSkill : Skill
{
    public override void TriggerAbillity(Ship ship)
    {

    }
}