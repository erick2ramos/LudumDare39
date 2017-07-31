using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {
    public float startTime = 2;

	IEnumerator Start () {
        yield return new WaitForSeconds(startTime);
        FindObjectOfType<GameManager>().InitializeRun();

    }
	
	void Update () {
		
	}
}
