using UnityEngine;

namespace PinguGame01
{
    
    [System.Serializable]
    public class PlatformSettings {

        [SerializeField] private PlatformType platformType;
        
        [Header("Direction Changer Settings")]
        [SerializeField] private Direction directionToChange;
        
        [Header("Moving Settings (Optional)")]
        [SerializeField] private bool isMovable;
        [SerializeField] private int unitsToMove;
        [SerializeField] private Direction directionToMove;
        [SerializeField] private bool hasSpeedVariation;
        [SerializeField] private float speedVariation;
        
        [Header("Object Attached (Optional)")]
        [SerializeField] private PlatformObjectIndex objectAttached;


        public PlatformSettings(){
            platformType = PlatformType.Normal;
            isMovable = false;
            unitsToMove = 0;
            directionToMove = Direction.Up;
            objectAttached = PlatformObjectIndex.None;
            hasSpeedVariation = false;
            speedVariation = 0;
        }
        

        public void SetPlatformType(PlatformType platformType){
            this.platformType = platformType;
        }

        public PlatformType GetPlatformType(){
            return platformType;
        }

        public void SetDirectionToChange(Direction directionToChange){
            this.directionToChange = directionToChange;
        }

        public Direction GetDirectionToChange(){
            return directionToChange;
        }

        public void SetMovable(bool isMovable){
            this.isMovable = isMovable;
        }

        public bool IsThePlatformMovable(){
            return isMovable;
        }

        public void SetUnitsToMove(int unitsToMove){
            this.unitsToMove = unitsToMove;
        }

        public int GetUnitsToMove(){
            return unitsToMove;
        }

        public void SetDirectionToMove(Direction directionToMove){
            this.directionToMove = directionToMove;
        }

        public Direction GetDirectionToMove(){
            return directionToMove;
        }

        public bool HasSpeedVariation(){
            return hasSpeedVariation;
        }

        public void ToggleSpeedVariation(bool variation){
            hasSpeedVariation = variation;
        }

        public float GetSpeedVariation(){
            return speedVariation;
        }

        public void SetSpeedVariation(float variation){
            speedVariation = variation;
        }

        public void SetObjectAttached(PlatformObjectIndex objectAttached){
            this.objectAttached = objectAttached;
        }

        public PlatformObjectIndex GetObjectAttached(){
            return objectAttached;
        }
    }
}