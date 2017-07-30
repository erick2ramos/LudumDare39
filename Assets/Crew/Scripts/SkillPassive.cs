using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Crew/Skills/Chef Passive")]
public class SkillPassive : Skill
{
    public int reductionAmount;

    public override void TriggerAbillity(Ship ship)
    {
        base.TriggerAbillity(ship);
        ship.modCrewE -= reductionAmount;
    }
}
