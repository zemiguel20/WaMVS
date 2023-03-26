using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIBoard : Singleton<UIBoard>
{
    public int initialMode = 3;
    [SerializeField] private List<UIBtn> buttons;

    [HideInInspector] public int currentMode;
    [HideInInspector] public int previousMode;

    void Start()
    {
        currentMode = initialMode;
        for (int i =0; i < buttons.Count; i++)
        {
            buttons[i].mode = i;
        }
        buttons[initialMode].on();
    }

    public void switchBtn()
    {
        buttons[previousMode].off();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
