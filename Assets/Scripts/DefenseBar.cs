using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DefenseBar : MonoBehaviour
{
    public Slider defenseBar; 

    private int maxDefense = 750; 
    private int currDefense; 

    public static DefenseBar instance; 

    public void Awake()
    {
        instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        currDefense = maxDefense; 
        defenseBar.maxValue = maxDefense; 
        defenseBar.value = maxDefense;
    }

    public void UseDefense(int defLoss) 
    {
        if (currDefense - defLoss >= 0) {
            currDefense -= defLoss; 
            defenseBar.value = currDefense;
        }
        else {
            currDefense = 0;
            defenseBar.value = currDefense;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
