using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderLevelDetails : LevelDetails
{
    // Start is called before the first frame update
    public static HeaderLevelDetails instance;
    void Start()
    {
        instance = this;
        FillDetails(GameManager.instance.GetCurrentLevel());
    }

    public override void FillDetails(LayoutSettings levelInfo){
        this.levelInfo = levelInfo;
        idText.text = ""+levelInfo.layoutId;
        switch(levelInfo.difficulty){
            case Difficulty.Easy:
                difficultyText.text = "Fácil";
                break;
            case Difficulty.Normal:
                difficultyText.text = "Normal";
                break;
            case Difficulty.Hard:
                difficultyText.text = "Difícil";
                break;
        }
        switch(levelInfo.gameSpeed){
            case GameSpeed.Slow:
                speedText.text = "Lenta";
                break;
            case GameSpeed.Normal:
                speedText.text = "Normal";
                break;
            case GameSpeed.Fast:
                speedText.text = "Rápida";
                break;
            case GameSpeed.Insane:
                speedText.text = "Insana (omg)";
                break;
        }
        descriptionText.text = ""+levelInfo.description;
        
    }

}
