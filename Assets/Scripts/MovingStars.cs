using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingStars : MonoBehaviour {
    [Range(0, 1)]
    public float stardensity;
    public Color starColor;
    public float speed;

    #region references
    public Image image;
    public Texture2D texture;
    #endregion


    void Start () {
		
	}
	
	void Update () {
		
	}
}
