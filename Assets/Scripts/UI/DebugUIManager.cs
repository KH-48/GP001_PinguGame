using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUIManager : MonoBehaviour
{

    [SerializeField] private UIElement[] UIElements;   

    public void OpenMenuElement(int id){
        UIElementId element = (UIElementId) id;
        for(int i = 0; i < UIElements.Length; i++){
            if(UIElements[i].elementId == element){
                UIElements[i].gameObject.SetActive(true);
            }else{
                UIElements[i].gameObject.SetActive(false);
            }
        }
    }

    public void CloseMenuElement(UIElementId element){
        for(int i = 0; i < UIElements.Length; i++){
            if(UIElements[i].elementId == element){
                UIElements[i].gameObject.SetActive(false);
            }
        }
    }
}
