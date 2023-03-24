using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationSource : MonoBehaviour
{
    private VibrationSystem vibSys;

    private void Awake()
    {
        vibSys = FindObjectOfType<VibrationSystem>();
    }

    public void Play()
    {
        vibSys.PlayVibration(transform);
    }
}
