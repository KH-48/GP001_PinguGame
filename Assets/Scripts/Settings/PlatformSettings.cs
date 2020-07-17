using UnityEngine;

[System.Serializable]
public class PlatformSettings {

    [SerializeField] private PlatformType platformType;
    
    [Header("Direction Changer Settings")]
    [SerializeField] private Direction directionToChange;
    
    [Header("Moving Settings (Optional)")]
    [SerializeField] private bool isMovable;
    [SerializeField] private int unitsToMove;
    [SerializeField] private Direction directionToMove;
    
    [Header("Object Attached (Optional)")]
    [SerializeField] private PlatformObjectIndex objectAttached;


    public PlatformSettings(){
        platformType = PlatformType.Normal;
        isMovable = false;
        unitsToMove = 0;
        directionToMove = Direction.Up;
        objectAttached = PlatformObjectIndex.None;

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

    public void SetObjectAttached(PlatformObjectIndex objectAttached){
        this.objectAttached = objectAttached;
    }

    public PlatformObjectIndex GetObjectAttached(){
        return objectAttached;
    }
}