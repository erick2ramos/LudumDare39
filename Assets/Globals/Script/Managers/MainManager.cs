using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {
    private static MainManager instance;

    public static MainManager Get
    {
        get
        {
            return instance;
        }
    }

    public GameManager gameManager;
    public OptionSelectionManager optSelectionManager;

    public CrewFactory crewFactory;
    public ShipFactory shipFactory;
    public EventFactory eventFactory;

    void Start () {
		if(instance == null)
        {
            instance = this;
            gameManager = GetComponent<GameManager>();
            optSelectionManager = GetComponent<OptionSelectionManager>();
            crewFactory = GetComponent<CrewFactory>();
            shipFactory = GetComponent<ShipFactory>();
            eventFactory = GetComponent<EventFactory>();
            //DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
	}
	
	void Update () {
		
	}
}
