using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsHUD : MonoBehaviour
{
    [SerializeField] TMP_Text pointText;

    int points = 0;

    private void Awake()
    {
        UpdateHUD();
    }
    public int Points
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
            UpdateHUD();
        }
    }

    private void UpdateHUD()
    {
        pointText.text = "Coins: " + points.ToString();
    }
}
