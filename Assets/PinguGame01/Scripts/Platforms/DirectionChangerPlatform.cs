using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguGame01
{
    public class DirectionChangerPlatform : MonoBehaviour
    {

        [SerializeField] private Direction newDirection;
        private float orientation;
        
        public void SetSettings(Direction newDirection){
            
            this.newDirection = newDirection;
            
            switch(newDirection){ //Change all of this

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

        private void TriggerPlatformEvent(){
            PlayerController.instance.ChangeDirection(newDirection);
            Debug.Log("Changing Direction!");
        }
    }
}
