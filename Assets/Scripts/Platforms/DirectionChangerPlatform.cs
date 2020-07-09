using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChangerPlatform : Platform
{

    [SerializeField] private Direction newDirection;
    private float orientation;
    
    public override void SetSettings(PlatformSettings ps){
        
        newDirection = ps.GetDirectionToChange();

        switch(newDirection){

            case Direction.Up:
                orientation = 0;
            break;
            case Direction.Down:
                orientation = 180;
            break;
            case Direction.Right:
                orientation = 90;
            break;
            case Direction.Left:
                orientation = -90;
            break;
        }   
         this.gameObject.transform.eulerAngles = new Vector3(0,orientation,0);
    }


    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            TriggerPlatformEvent();
        }
    }

    protected override void TriggerPlatformEvent(){
        PlayerController.instance.currentDirection = newDirection;
        Debug.Log("Changing Direction!");
    }
}
