using UnityEngine;
using System.Collections;

public class CrewFactory : MonoBehaviour
{
    public GameObject[] AllCrew;

    public GameObject Create(int index)
    {
        return Instantiate(AllCrew[index]);
    }
}
