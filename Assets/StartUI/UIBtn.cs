using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIBtn : MonoBehaviour
{
    public int mode = 3;
    public bool isOn { get; private set; }
    private Material mat;

    void Start()
    {
        isOn = false;
        mat = GetComponent<Renderer>().material;
        mat.color = Color.gray;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOn)
        {
            on();
        }

    }

    public void on()
    {
        isOn = true;
        mat.color = Color.green;
        UIBoard.Instance.previousMode = UIBoard.Instance.currentMode;
        UIBoard.Instance.currentMode = mode;
        if(UIBoard.Instance.previousMode != UIBoard.Instance.currentMode)
        {
            UIBoard.Instance.switchBtn();
        }
        
    }

    public void off()
    {
        isOn = false;
        mat.color = Color.gray;
    }
}
