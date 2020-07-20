using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlatformInfoPanelHandler : MonoBehaviour
{

    public static PlatformInfoPanelHandler instance;
    [Header("UI Elements")]
    [SerializeField] private GameObject Panel;
    [SerializeField] private TextMeshProUGUI platformIndexText;
    [SerializeField] private Dropdown platformType;
    [SerializeField] private Toggle isMovable;
    [SerializeField] private Dropdown directionToChange;
    [SerializeField] private Dropdown directionToMove;
    [SerializeField] private Dropdown unitsToMove;
    [SerializeField] private Dropdown objectAttached;

    [SerializeField] private GameObject directionToChangePanel;
    [SerializeField] private GameObject directionToMovePanel;
 
    private string[] platformTypeOptions = {"Normal","Cambia-Direccion","Ninguna"};
    private string[] directionOptions = {"Arriba","Abajo","Derecha","Izquierda"};
    private string[] unitsToMoveOptions = {"Uno","Dos","Tres"};
    private string[] objectAttachedOptions = {"Ninguno"};
    // Start is called before the first frame update
    void Start()
    {

        instance = this;
        
        platformType.options.Clear();
        directionToChange.options.Clear();
        directionToMove.options.Clear();
        unitsToMove.options.Clear();
        objectAttached.options.Clear();

        for(int i = 0; i < platformTypeOptions.Length; i++){
            platformType.options.Add(new Dropdown.OptionData(platformTypeOptions[i]));
        }
        for(int i = 0; i < directionOptions.Length; i++){
            directionToChange.options.Add(new Dropdown.OptionData(directionOptions[i]));
        }
        for(int i = 0; i < directionOptions.Length; i++){
            directionToMove.options.Add(new Dropdown.OptionData(directionOptions[i]));
        }
        for(int i = 0; i < unitsToMoveOptions.Length; i++){
            unitsToMove.options.Add(new Dropdown.OptionData(unitsToMoveOptions[i]));
        }
        for(int i = 0; i < objectAttachedOptions.Length; i++){
            objectAttached.options.Add(new Dropdown.OptionData(objectAttachedOptions[i]));
        }

        platformType.value = 1;
        directionToChange.value = 1;
        directionToMove.value = 1;
        unitsToMove.value = 1;
        objectAttached.value = 1;

        platformType.value = 0;
        directionToChange.value = 0;
        directionToMove.value = 0;
        unitsToMove.value = 0;
        objectAttached.value = 0;
    }

    public void ActivatePanel(){
        Panel.SetActive(true);
    }

    public void FillValues(int index, PlatformSettings platformInfo){
        platformIndexText.text = "#"+ index;
        platformType.value = (int) platformInfo.GetPlatformType();
        isMovable.isOn = platformInfo.IsThePlatformMovable();
        directionToChange.value = (int) platformInfo.GetDirectionToChange();
        directionToMove.value = (int) platformInfo.GetDirectionToMove();
        unitsToMove.value = platformInfo.GetUnitsToMove() - 1; //1-3 to 0-2 Range conversion
        objectAttached.value = (int) platformInfo.GetObjectAttached();

        UpdatePanels();
    }

    public void UpdatePanels(){

    
        switch(platformType.value){ 

            case 0:
                isMovable.gameObject.SetActive(true);
                directionToChangePanel.SetActive(false);
                objectAttached.gameObject.SetActive(true);
                break;

            case 1: //Activate Direction-changer Settings Panel
                isMovable.gameObject.SetActive(true);
                directionToChangePanel.SetActive(true);
                objectAttached.gameObject.SetActive(false);
                break;

            case 2: //If 'None' is selected, deactivate all options

                isMovable.gameObject.SetActive(false);
                directionToChangePanel.SetActive(false);
                objectAttached.gameObject.SetActive(false);
                break;
        }

        if(isMovable.isOn && platformType.value != 2){ //Activate Moving Platform Settings Panel
           directionToMovePanel.SetActive(true);
        }else{
            isMovable.isOn = false;
           directionToMovePanel.SetActive(false);
        }
   }

    public void CreateNewSettings(){
        PlatformSettings newSettings = new PlatformSettings();
        newSettings.SetPlatformType((PlatformType) platformType.value);
        newSettings.SetMovable(isMovable.isOn);
        newSettings.SetDirectionToChange((Direction) directionToChange.value);
        newSettings.SetDirectionToMove((Direction) directionToMove.value);
        newSettings.SetUnitsToMove(unitsToMove.value + 1);
        newSettings.SetObjectAttached((PlatformObjectIndex) objectAttached.value);

        GameManager.instance.CreateNewPlatform(newSettings);
        Panel.SetActive(false);

    }
}
