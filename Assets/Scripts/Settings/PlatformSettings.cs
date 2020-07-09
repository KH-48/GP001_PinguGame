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

    public PlatformSettings(PlatformType pt, Direction d, bool im, Direction dtm, PlatformObjectIndex poi){

        platformType = pt;
        directionToChange = d;
        isMovable = im;
        directionToMove = dtm;
        objectAttached = poi;

    }

    public PlatformType GetPlatformType(){
        return platformType;
    }

    public Direction GetDirectionToChange(){
        return directionToChange;
    }

    public bool IsThePlatformMovable(){
        return isMovable;
    }

    public int GetUnitsToMove(){
        return unitsToMove;
    }

    public Direction GetDirectionToMove(){
        return directionToMove;
    }

    public PlatformObjectIndex GetObjectAttached(){
        return objectAttached;
    }
}