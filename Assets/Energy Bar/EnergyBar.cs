using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EnergyBar : MonoBehaviour {
    public Transform container;
    public GameObject element;
    public Text energyNumberText;

    public int maxSegments = 10;
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
        int numSegments = Mathf.RoundToInt((debug ? this.percent : this.percent) * maxSegments);

        var children = new List<GameObject>();
        foreach (Transform child in container) children.Add(child.gameObject);
        if (Application.isPlaying) {
            children.ForEach(child => Destroy(child));
        } else {
            children.ForEach(child => DestroyImmediate(child));
        }

        for (int i = 0; i < numSegments; i++) {
            GameObject.Instantiate(element, container, false);
        }

        energyNumberText.text = string.Format("{0}", this.percent * maxEnergy);
    }


}
