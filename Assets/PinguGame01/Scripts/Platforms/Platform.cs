using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguGame01
{
    public class Platform : MonoBehaviour
    {

        public PlatformSettings settings;
        public Vector3 initialPosition;

        public GameObject[] ObjectPool;

        [SerializeField] GameObject directionChanger;
        
        public void SetSettings(PlatformSettings settings){

            this.settings = settings;

            switch(settings.GetPlatformType()){

                case PlatformType.DirectionChanger:
                    directionChanger.SetActive(true);
                    directionChanger.GetComponent<DirectionChangerPlatform>()
                    .SetSettings(settings.GetDirectionToChange());
                    break;
            }

            if(settings.GetObjectAttached() != PlatformObjectIndex.None){
                Vector3 offset = new Vector3(0,0.5f,0);
                GameObject o = Instantiate(ObjectPool[(int) settings.GetObjectAttached()],
                 transform.position+offset, Quaternion.identity);
                
                 o.transform.parent = this.transform;
                 
            }

            if(settings.IsThePlatformMovable()){
                gameObject.AddComponent<MovingPlatform>()
                .SetSettings(settings.GetDirectionToMove(), settings.GetUnitsToMove(),
                settings.HasSpeedVariation(),settings.GetSpeedVariation());
            }

        }

        public PlatformSettings GetSettings(){
            return settings;
        }

        private void OnCollisionEnter(Collision other) {
            if(other.transform.name == "Player" && other.transform.position.y < 0.5){
                PlayerController.instance.StopMoving();
            }
        }
    }
}
