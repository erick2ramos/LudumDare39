using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public EnergyBar2 energyBar;
    public Slider distance;
    public Image portrait;
    public Text dialogue;
    public Text option1;
    public Text option2;
    public Text answer;
	public Button b1;
	public Button b2;

    public Sprite[] portraits;


    void Start () {
		
	}
	
    public void ChangePortrait(CrewType type)
    {
        switch(type)
        {
            case CrewType.Chef:
                {
                    portrait.sprite = portraits[0];
                    break;
                }
            case CrewType.Medic:
                {
                    portrait.sprite = portraits[1];
                    break;
                }
            case CrewType.Pilot:
                {
                    portrait.sprite = portraits[2];
                    break;
                }
            case CrewType.Engineer:
                {
                    portrait.sprite = portraits[3];
                    break;
                }
            default:
                {
                    portrait.sprite = portraits[Random.Range(0,3)];
                    break;
                }
        }
    }
}
