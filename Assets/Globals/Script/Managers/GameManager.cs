﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //TODO: Quitar esto!!!!!!!!!!!!!!
    public Text phaseText;

    public UIManager uiManager;
    public Animator mainAnimator;

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
        mainAnimator.SetTrigger("Next");
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
        uiManager.energyBar.SetBarPercent(currentShip.Energy / currentShip.MaxEnergy);
        //print("Energy left: " + currentShip.Energy + "/" + currentShip.MaxEnergy);
        // Check for event to resolve or fail then continue to next fase
        currentEvent = MainManager.Get.eventFactory.NextEvent();
        if (currentEvent == null)
        {
            stateMachineActive = false;
            return;
        }
        uiManager.dialogue.text = currentEvent.premise + "\nWhat you do?";
        //print(currentEvent.premise);
        uiManager.option1.text = string.Format("{0}. {1}", 1, currentEvent.options[0].option);
        uiManager.option2.text = string.Format("{0}. {1}", 2, currentEvent.options[1].option);

        /*for (int i = 0; i < currentEvent.options.Length; i++)
        {
            print(i + ". " + currentEvent.options[i].option);
        }*/
        //print("What you do?");
        nextPhase = Phase.MainPhase;
        
    }

    public void GetAnswer (int option) {
        EventSelectOption(currentEvent.options[option]);
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

    public void PHEnd() {
        //nextPhase = Phase.Upkeep;
    }

    public void NextQuestion () {
        mainAnimator.SetTrigger("Next");
        nextPhase = Phase.Upkeep;
    }

    public void TurnPass()
    {
        nextPhase = Phase.Pass;
        TurnNumber++;
        uiManager.distance.value = TurnNumber / TurnAmount;
    }

    public void EventSelectOption(EventOption option)
    {
        if (Random.value * 100 < option.successPercent)
        {
            uiManager.answer.text = option.successText;
            //print(option.successText);
            uiManager.energyBar.SetBarPercent( currentShip.InstantEnergy(option.successEnergy) );
        } else
        {
            uiManager.answer.text = option.failureText.ToString();
            //print(option.failureText);
            uiManager.energyBar.SetBarPercent(currentShip.InstantEnergy(option.successEnergy) );
        }
        mainAnimator.SetTrigger("Answer");
        TurnPass();
    }
}
