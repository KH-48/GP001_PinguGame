using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class LayoutSettings {
    
    public bool active=true;
    public int layoutId;
    public Difficulty difficulty;
    public GameSpeed gameSpeed;
    public PlatformSettings[] layoutPlatforms;
    public string description;
    
    public LayoutSettings(){
        layoutPlatforms = new PlatformSettings[16]; //Initialize
        for(int i=0;i<layoutPlatforms.Length; i++){
            layoutPlatforms[i] = new PlatformSettings();
        }
        description = "n/a";
    }

    public PlatformType GetPlatformType(int index){
        return layoutPlatforms[index].GetPlatformType();
    }
    public PlatformSettings GetPlatformSettings(int index){
        return layoutPlatforms[index];
    }
}