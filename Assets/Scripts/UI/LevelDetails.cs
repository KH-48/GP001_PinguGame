using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDetails : MonoBehaviour
{

    [SerializeField] protected LayoutSettings levelInfo;
    [SerializeField] protected TextMeshProUGUI idText;
    [SerializeField] protected TextMeshProUGUI difficultyText;
    [SerializeField] protected TextMeshProUGUI speedText;
    [SerializeField] protected TextMeshProUGUI descriptionText;
 

    public virtual void FillDetails(LayoutSettings levelInfo){
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

    public void SelectLevel(){
        GameManager.instance.SetCurrentLevel(levelInfo);
    }

    public void DeleteLevel(){
        GameManager.instance.DeleteLevel(levelInfo);
        Destroy(this.gameObject);
    }

}
