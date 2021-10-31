using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWindow : MonoBehaviour
{
    public GameObject tips;
    

    public void ToggleTips()
    {
        tips.SetActive(!tips.activeSelf);
    }
}
