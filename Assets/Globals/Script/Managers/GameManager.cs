using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Ship currentShip;

    public GameEvent currentEvent;
    public int TurnNumber;
    public int TurnAmount;

    public bool passTurn;
    public bool stateMachineActive;

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
        //for (int i = 0; i < MainManager.Get.optSelectionManager.CrewSelection.Length; i++)
        //{
        //    GameObject crew = MainManager.Get.crewFactory.Create(MainManager.Get.optSelectionManager.CrewSelection[i]);
        //    currentShip.AddCrewMember(i, crew.GetComponent<CrewMember>());
        //}
        MainManager.Get.eventFactory.GenerateNewRunEvents(TurnAmount);
        nextPhase = Phase.Upkeep;
        stateMachineActive = true;
    }
	
	void Update () {
        if(nextPhase != currentPhase)
        {
            currentPhase = nextPhase;
        } else
        {
            if(stateMachineActive)
                phases[(int)currentPhase]();
        }
	}

    public void PHUpkeep() { nextPhase = Phase.DrawEvent; }

    public void PHDrawEvent()
    {
        print("Turn Number: " + TurnNumber);
        print("Energy left: " + currentShip.Energy + "/" + currentShip.MaxEnergy);
        // Check for event to resolve or fail then continue to next fase
        currentEvent = MainManager.Get.eventFactory.NextEvent();
        if (currentEvent == null)
        {
            stateMachineActive = false;
            return;
        }
        print(currentEvent.premise);
        for (int i = 0; i < currentEvent.options.Length; i++)
        {
            print(i + ". " + currentEvent.options[i].option);
        }
        print("What you do?");
        nextPhase = Phase.MainPhase;
    }

    public void PHMainPhase()
    {
        // Let the player play cards can pass phase until the player selects to finish his turn
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            // Selecting first option
            EventSelectOption(currentEvent.options[0]);
        }

        if (Input.GetKeyUp(KeyCode.Keypad1))
        {
            // Selecting second option.
            EventSelectOption(currentEvent.options[1]);
        }
    }

    public void PHPass()
    {
        // Reduce ship energy applying all modifiers and continue to next fase
        //currentShip.CalculateEnergy();
        currentEvent = null;
        nextPhase = Phase.End;
    }

    public void PHEnd() { nextPhase = Phase.Upkeep; }

    public void TurnPass()
    {
        nextPhase = Phase.Pass;
        TurnNumber++;
    }

    public void EventSelectOption(EventOption option)
    {
        if (Random.value * 100 < option.successPercent)
        {
            print(option.successText);
            currentShip.InstantEnergy(option.successEnergy);
        } else
        {
            print(option.failureText);
            currentShip.InstantEnergy(option.successEnergy);
        }

        TurnPass();
    }
}
