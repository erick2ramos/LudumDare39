using UnityEngine;
using System.Collections;

public class OptionSelectionManager : MonoBehaviour
{
    // Esto es para poner las nave y los miembros que selecionaste
    // para poder iniciar la partida
    public int ShipSelected;
    public int[] CrewSelection;

    private void Start()
    {
        ShipSelected = 0;
        CrewSelection = new int[1] { 0 };
    }
}
