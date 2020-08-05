using UnityEngine;

namespace PinguGame01
{
    [System.Serializable]
    public class PathSegment {
        [SerializeField] private Direction directionToMove;
        [SerializeField] private int unitsToMove;

        public Direction GetDirectionToMove(){
            return directionToMove;
        }

        public int GetUnitsToMove(){
            return unitsToMove;
        }
    }
}