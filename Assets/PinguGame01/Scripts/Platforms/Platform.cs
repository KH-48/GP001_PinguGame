using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguGame01
{
    public abstract class Platform : MonoBehaviour
    {

        public PlatformSettings settings;
        public Vector3 initialPosition;
        public abstract void SetSettings(PlatformSettings ps);

        protected abstract void TriggerPlatformEvent();

        public PlatformSettings GetSettings(){
            return settings;
        }

        private void OnCollisionEnter(Collision other) {
            if(other.transform.name == "Player" && other.transform.position.y < 0.5){
                PlayerController.instance.StopMoving();
                //Debug.Log("OH NO!!!");
            }
        }
    }
}
