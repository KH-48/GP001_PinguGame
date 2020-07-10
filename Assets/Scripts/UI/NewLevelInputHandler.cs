using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewLevelInputHandler : MonoBehaviour
{

    [SerializeField] private Dropdown difficultyDropdown;
    [SerializeField] private Dropdown speedDropdown;
    [SerializeField] private InputField descrptionInputField;
    [SerializeField] private Text placeHolderText;

    [SerializeField] private string[] difficultyTextOptions = {"Fácil", "Normal", "Difícil"};
    [SerializeField] private string[] speedTextOptions = {"Lenta", "Normal", "Rápida", "Insana"};
    void Start() {

        difficultyDropdown.options.Clear();
        speedDropdown.options.Clear();

        for(int i = 0; i < difficultyTextOptions.Length; i++){
            difficultyDropdown.options.Add(new Dropdown.OptionData(difficultyTextOptions[i]));
        }
        for(int i = 0; i < speedTextOptions.Length; i++){
            speedDropdown.options.Add(new Dropdown.OptionData(speedTextOptions[i]));
        }
        difficultyDropdown.value = 1;
        speedDropdown.value = 1;
        difficultyDropdown.value = 0;
        speedDropdown.value = 0;
    }

    public void CreateNewLevel(){

        LayoutSettings newLevel = new LayoutSettings();

        newLevel.difficulty = (Difficulty) difficultyDropdown.value;
        newLevel.gameSpeed = (GameSpeed) speedDropdown.value;
        newLevel.description = descrptionInputField.text;

        GameManager.instance.CreateNewLevel(newLevel);
    }

    public void ResetInputs(){
        difficultyDropdown.value = 0;
        speedDropdown.value = 0;
        descrptionInputField.text = placeHolderText.text; 
    }

}
