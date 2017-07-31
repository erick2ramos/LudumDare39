using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EnergyBar2 : MonoBehaviour {
    public Text energyNumberText;
    public Image energyBar;

    public int maxEnergy = 100;
    public bool debug;
    [Range(0, 1)]
    public float percent;


	void Start () {
		
	}
	
	void Update () {
        if (debug) SetBarPercent(0);
    }

    public void SetBarPercent(float percent) {
        energyBar.fillAmount = debug? this.percent : percent;
    }


}
