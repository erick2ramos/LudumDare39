using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Ship currentShip;
    public int TurnNumber;

    public bool passTurn;

    delegate void GamePhase();
    enum Phase
    {
        Upkeep,
        DrawEvent,
        MainPhase,
        Pass,
        End,
        Count
    }
    GamePhase[] phases;
    Phase currentPhase;
    Phase nextPhase;

    void Start () {
        TurnNumber = 0;
        phases = new GamePhase[(int)Phase.Count];
        phases[(int)Phase.Upkeep] = PHUpkeep;
        phases[(int)Phase.DrawEvent] = PHDrawEvent;
        phases[(int)Phase.MainPhase] = PHMainPhase;
        phases[(int)Phase.Pass] = PHPass;
        phases[(int)Phase.End] = PHEnd;


        //QUITAR ESTO --- ES PARA PROBAR
        Invoke("InitializeRun", 2);
        //---
    }

    public void InitializeRun()
    {
        // Recolect and instantiate selected crew and ship
        // move Start code to here
        currentShip = MainManager.Get.shipFactory.Create(MainManager.Get.optSelectionManager.ShipSelected).GetComponent<Ship>();
        for (int i = 0; i < MainManager.Get.optSelectionManager.CrewSelection.Length; i++)
        {
            GameObject crew = MainManager.Get.crewFactory.Create(MainManager.Get.optSelectionManager.CrewSelection[i]);
            currentShip.AddCrewMember(i, crew.GetComponent<CrewMember>());
        }
        nextPhase = Phase.Upkeep;
    }
	
	void Update () {
        if(nextPhase != currentPhase)
        {
            nextPhase = currentPhase;
        } else
        {
            phases[(int)currentPhase]();
        }
	}

    public void PHUpkeep() { nextPhase = Phase.DrawEvent; }

    public void PHDrawEvent()
    {
        // Check for event to resolve or fail then continue to next fase
        nextPhase = Phase.MainPhase;
    }

    public void PHMainPhase()
    {
        // Let the player play cards can pass phase until the player selects to finish his turn
    }

    public void PHPass()
    {
        // Reduce ship energy applying all modifiers and continue to next fase
        currentShip.CalculateEnergy();
        nextPhase = Phase.End;
    }

    public void PHEnd() { nextPhase = Phase.Upkeep; }

    public void TurnPass()
    {
        nextPhase = Phase.Pass;
        TurnNumber++;
    }
}
