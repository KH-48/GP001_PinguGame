using UnityEngine;
using System.Collections.Generic;

namespace PinguGame01
{
    [System.Serializable]
    public class MovingPath {
         [SerializeField] private List<PathSegment> movingPath = new List<PathSegment>();
         [SerializeField] private bool loopPath = false;

         public bool IsPathLooping(){
             return loopPath;
         }

         public void SetLoopPath(bool loopPath){
            this.loopPath = loopPath;
         }

         public PathSegment GetPathSegment(int index){
             return movingPath[index];
         }

         public void SetPathSegment(PathSegment newSegment, int index){
             movingPath[index] = newSegment;
         }

         public void AddNewSegment(PathSegment newSegment){
             movingPath.Add(newSegment);
         }

         public void RemoveSegment(int index){
             movingPath.RemoveAt(index);
         }

         public int GetPathLength(){
             return movingPath.Count;
         }
    }
}
