using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public string gameSceneName;
    public Animator mainMenuAnimator;


	void Start () {
		
	}
	

	void Update () {
		
	}

    public void OnStartGame () {
        mainMenuAnimator.SetTrigger("Play");
    }

    public void OnStartGameAnimationEnd() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }

    public void OnExitGame() {
        Application.Quit();
    }

    public void ShowCredits() {
        mainMenuAnimator.SetTrigger("Credits");
    }

    public void ShowHowToPlay() {
        mainMenuAnimator.SetTrigger("HTP");
    }
}
