using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LayoutSettings {
    
    public bool active=true;
    public int layoutId;
    public Difficulty difficulty;
    public GameSpeed gameSpeed;
    public PlatformSettings[] layoutPlatforms;


    public PlatformType GetPlatformType(int index){
        return layoutPlatforms[index].GetPlatformType();
    }
    public PlatformSettings GetPlatformSettings(int index){
        return layoutPlatforms[index];
    }
}