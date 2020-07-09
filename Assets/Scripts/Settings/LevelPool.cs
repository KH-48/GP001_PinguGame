using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelPool 
{
    public LayoutSettings[] levels;

    public LevelPool(int length){
        levels = new LayoutSettings[length];
    }
}
