using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour{
    public int MaxEnergy;
    public int Energy;

    public int baseShipE;
    public int baseCrewE;
    public int baseRoomsE;

    public int modShipE;
    public int modCrewE;
    public int modRoomsE;

    public int initialHandSize;
    public int initialScrapAmount;
    public int initialMaxScrapAmount;
    public int maxAmountOfRooms;

    public bool DiscoverMoreItems;
    public bool DoScry;

    [SerializeField]
    Storage storageRoom;
    [SerializeField]
    List<Room> roomSlots;
    [SerializeField]
    List<CrewMember> shipCrew;

    void Start()
    {
        storageRoom = new Storage()
        {
            maxHandSize = initialHandSize,
            maxScrapAmount = initialMaxScrapAmount,
            scrapAmount = initialScrapAmount
        };

        roomSlots = new List<Room>();
        for (int i = 0; i < maxAmountOfRooms; i++)
        {
            roomSlots.Add(new Room()); 
        }
    }

    public void AddCrewMember(int at, CrewMember crew)
    {
        shipCrew.Insert(at, crew);
    }

    public int StorageSize()
    {
        return storageRoom.maxScrapAmount;
    }

    public int HandSize()
    {
        return storageRoom.maxHandSize;
    }

    public int ScrapAmount()
    {
        return storageRoom.scrapAmount;
    }

    public void CalculateEnergy()
    {
        int aux = 0;
        foreach(Room r in roomSlots)
        {
            if (r.active)
            {
                aux += r.energyPerTurn;
            }
        }
        baseShipE = aux;

        aux = 0;
        foreach (CrewMember c in shipCrew)
        {
            aux += c.energyPerTurn;
        }
        baseCrewE = aux;

        // TODO: Calculate slotchannelingDelta
    }

    public float InstantEnergy(int amount)
    {
        Energy = Mathf.Clamp((Energy + amount), 0, MaxEnergy);
        return Energy / MaxEnergy;

    }

    public void ConsumeEnergy()
    {
        Energy = Mathf.Clamp((Energy - (baseShipE + modShipE + baseCrewE + modCrewE)), 0, MaxEnergy);
    }
}