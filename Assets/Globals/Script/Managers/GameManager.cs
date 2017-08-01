using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Ship currentShip;

    public GameEvent currentEvent;
    public int TurnNumber;
    public int TurnAmount;

    public bool passTurn;
    public int energyPerTurn;
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

    public UIManager uiManager;
    public Animator mainAnimator;

    public string mainMenuSceneName;
    public AudioSource succesSound;
    public AudioSource failSound;

    void Start () {
        TurnNumber = 0;
        phases = new GamePhase[(int)Phase.Count];
        phases[(int)Phase.Upkeep] = PHUpkeep;
        phases[(int)Phase.DrawEvent] = PHDrawEvent;
        phases[(int)Phase.MainPhase] = PHMainPhase;
        phases[(int)Phase.Pass] = PHPass;
        phases[(int)Phase.End] = PHEnd;
        
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
        //mainAnimator.SetTrigger("Next");
        uiManager.distance.value = 0;
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

    public void PHUpkeep() {
        if(TurnAmount == TurnNumber)
        {
            // Gratz you win the game
            mainAnimator.SetTrigger("Win");
        } else if(currentShip.Energy <= 0)
        {
            // Boom game over :(
            mainAnimator.SetTrigger("Lose");
        } else {
            mainAnimator.SetTrigger("Next");
        }

        nextPhase = Phase.DrawEvent;
    }

    public void Restart () {
        InitializeRun();
        mainAnimator.SetTrigger("Restart");
    }

    public void ExitToMenu () {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
    }

    public void PHDrawEvent()
    {
		uiManager.b1.interactable= true;
		uiManager.b2.interactable= true;
        print("Turn Number: " + TurnNumber);
        uiManager.energyBar.SetBarPercent((float) currentShip.Energy / currentShip.MaxEnergy);
        //print("Energy left: " + currentShip.Energy + "/" + currentShip.MaxEnergy);
        // Check for event to resolve or fail then continue to next fase
        currentEvent = MainManager.Get.eventFactory.NextEvent();
        if (currentEvent == null)
        {
            stateMachineActive = false;
            return;
        }
        uiManager.dialogue.text = currentEvent.premise + "\nWhat do you do?";
        //print(currentEvent.premise);
        uiManager.option1.text = currentEvent.options[0].option;
        uiManager.option2.text = currentEvent.options[1].option;

        /*for (int i = 0; i < currentEvent.options.Length; i++)
        {
            print(i + ". " + currentEvent.options[i].option);
        }*/
        //print("What you do?");
        nextPhase = Phase.MainPhase;
        
    }

    public void GetAnswer (int option) {
		if (currentEvent != null) {
			EventSelectOption(currentEvent.options[option]);
		}        
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
		if (nextPhase == Phase.End) {
			nextPhase = Phase.Upkeep;
		}
        
    }

    public void TurnPass()
    {
        nextPhase = Phase.Pass;
        TurnNumber++;
        uiManager.distance.value = (float)TurnNumber / TurnAmount;
    }

    public void EventSelectOption(EventOption option)
    {
		uiManager.b1.interactable = false;
		uiManager.b2.interactable = false;
		mainAnimator.SetTrigger("Answer");
        int finalConsumption = 0;
        if (Random.value * 100 < option.successPercent)
        {
            uiManager.answer.text = option.successText;
            succesSound.Play();
            finalConsumption = option.successEnergy;
        } else
        {
            uiManager.answer.text = option.failureText.ToString();
            failSound.Play();
            finalConsumption = option.failureEnergy;
        }
        finalConsumption -= energyPerTurn;
        uiManager.energyBar.SetBarPercent(currentShip.InstantEnergy(finalConsumption));
        
        TurnPass();
    }
}
