using UnityEngine;
using System.Collections;

public class ShipFactory : MonoBehaviour
{
    public GameObject[] AllShips;

    public GameObject Create(int index)
    {
        return Instantiate(AllShips[index]);
    }
}
