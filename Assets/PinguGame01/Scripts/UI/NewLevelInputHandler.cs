using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PinguGame01
{
    public class NewLevelInputHandler : MonoBehaviour
    {

        [SerializeField] private Dropdown difficultyDropdown;
        [SerializeField] private Dropdown speedDropdown;
        [SerializeField] private InputField descrptionInputField;
        [SerializeField] private Text placeHolderText;

        [SerializeField] private string[] difficultyTextOptions = {"Fácil", "Normal", "Difícil"};
        [SerializeField] private string[] speedTextOptions = {"Lenta", "Normal", "Rápida", "Insana"};
        private LevelSettings levelInfo;
        
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

            levelInfo = new LevelSettings();
            levelInfo.difficulty = (Difficulty) difficultyDropdown.value;
            levelInfo.gameSpeed = (GameSpeed) speedDropdown.value;
            levelInfo.description = descrptionInputField.text;

            GameManager.instance.CreateNewLevel(levelInfo);
        }

        public void FillWithLevelDetails(){
            levelInfo = GameManager.instance.GetCurrentLevel();

            difficultyDropdown.value = (int) levelInfo.difficulty;
            speedDropdown.value = (int) levelInfo.gameSpeed;
            if(levelInfo.description == ""){
                descrptionInputField.text = "";
            }else{
                descrptionInputField.text = levelInfo.description;
            }
        }

        public void EditLevel(){

            levelInfo.difficulty = (Difficulty) difficultyDropdown.value;
            levelInfo.gameSpeed = (GameSpeed) speedDropdown.value;
            levelInfo.description = descrptionInputField.text;

            GameManager.instance.ModifyLevelDetails(levelInfo);
        }
        public void ResetInputs(){
            difficultyDropdown.value = 0;
            speedDropdown.value = 0;
            descrptionInputField.text = ""; 
        }

    }
}
