using UnityEngine;
using System.Collections.Generic;

public class CrewFactory : MonoBehaviour
{
    public GameObject[] AllCrew;
    Dictionary<CrewType, GameObject> crewMap;

    private void Start()
    {
        crewMap = new Dictionary<CrewType, GameObject>();
        for (int i = 0; i < AllCrew.Length; i++)
        {
            crewMap[AllCrew[i].GetComponent<CrewMember>().type] = AllCrew[i];
        }
    }

    public GameObject Create(int index)
    {
        return Instantiate(AllCrew[index]);
    }

    public GameObject GetByType(CrewType ctype)
    {
        GameObject go;
        crewMap.TryGetValue(ctype, out go);
        return go;
    }
}
