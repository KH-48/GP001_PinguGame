using UnityEngine;
using System.Collections.Generic;
namespace PinguGame01
{
    public class MovingPlatform : MonoBehaviour {

        [SerializeField] private enum PlatformState{Stopped, Moving};
        [SerializeField] private PlatformState platformState;
        [SerializeField] private MovingPath path;
        [SerializeField] private int unitsToMove=0;
        [SerializeField] private Direction directionToMove;
        
        [SerializeField] private float speed;

        private bool goingInReverse = false;
        private float speedVariation=0;
        private float basePosition;
        private float targetPosition;
        private float lastTargetPosition;

        [Header("Path Testing")]
        private float[] pathPositions;
        private Vector3 currentPosition;

        private int currentPositionIndex=0;

        void Update(){

            switch(platformState){

                case PlatformState.Stopped:
                break;

                case PlatformState.Moving:
                    Move();
                break;
            }

        }

        public void Move(){
            
             float step = speed * Time.deltaTime;

                    switch(directionToMove){

                        case Direction.Up:
                        case Direction.Down:
                            transform.position = Vector3.MoveTowards(
                                transform.position,
                                new Vector3 (targetPosition,
                                            transform.position.y,
                                            transform.position.z),
                                step);
                            
                            if(!goingInReverse && transform.position.x == targetPosition){
                                lastTargetPosition = targetPosition;
                                targetPosition = basePosition;
                                goingInReverse = true;
                            }
                            if(goingInReverse && transform.position.x == basePosition){
                                targetPosition = lastTargetPosition;
                                goingInReverse = false;
                            }     
                        break;

                        case Direction.Right:
                        case Direction.Left:
                            transform.position = Vector3.MoveTowards(
                                transform.position,
                                new Vector3 (transform.position.x,
                                            transform.position.y,
                                            targetPosition),
                                step);
                            
                            if(!goingInReverse && transform.position.z == targetPosition){
                                lastTargetPosition = targetPosition;
                                targetPosition = basePosition;
                                goingInReverse = true;
                            }
                            if(goingInReverse && transform.position.z == basePosition){
                                targetPosition = lastTargetPosition;
                                goingInReverse = false;
                            }     
                        break;
                    }
        }
      
       /*Fix Algorithm*/
        public void Move(bool mode){

            float step = speed * Time.deltaTime;

            if(currentPositionIndex == path.GetPathLength()){
                if(path.IsPathLooping()){
                    goingInReverse = true;
                    currentPositionIndex = path.GetPathLength()-1;
                }else{
                    transform.position = GetComponent<Platform>().initialPosition;
                    currentPositionIndex = 0;
                }
            }

            if(currentPositionIndex < 0){ //looping
                goingInReverse = false;
                currentPositionIndex = 0;
            }
            switch(path.GetPathSegment(currentPositionIndex).GetDirectionToMove()){

                    case Direction.Up:
                    case Direction.Down:
                        transform.position = Vector3.MoveTowards(
                            transform.position,
                            new Vector3 (pathPositions[currentPositionIndex],
                                transform.position.y,
                                transform.position.z),
                                step);    
                        break;

                    case Direction.Right:
                    case Direction.Left:
                        transform.position = Vector3.MoveTowards(
                            transform.position,
                            new Vector3 (transform.position.x,
                                transform.position.y,
                                pathPositions[currentPositionIndex]),
                                step);    
                        break;
            }
            if(!goingInReverse){
                currentPositionIndex+=1;
            }else{
                currentPositionIndex-=1;
            }
        }
        public void SetSettings(Direction directionToMove, int unitsToMove, bool hasSpeedVariation, float speedVariation){ //add each attribute
            
            this.unitsToMove = unitsToMove;
            this.directionToMove = directionToMove;
            speed = GameManager.instance.GetCurrentSpeed();
            switch(unitsToMove){ 

                case 2:
                    speed = speed + (0.25f *speed);
                    break;

                case 3:
                    speed = speed + (0.75f * speed); 
                    break;
            }

            if(hasSpeedVariation){
                speed = speed + (speed*(speedVariation*0.01f));
            }
            
            
            switch(directionToMove){

                case Direction.Up:
                    basePosition = transform.position.x;
                    targetPosition = basePosition + (unitsToMove * GameManager.instance.GetXOffset());
                break;

                case Direction.Down:
                    basePosition = transform.position.x;
                    targetPosition = basePosition - (unitsToMove * GameManager.instance.GetXOffset());
                break;

                case Direction.Right:
                    basePosition = transform.position.z;
                    targetPosition = basePosition - (unitsToMove * GameManager.instance.GetZOffset());
                break;

                case Direction.Left: 
                    basePosition = transform.position.z;
                    targetPosition = basePosition + (unitsToMove * GameManager.instance.GetZOffset());      
                break;
            }
            
            platformState = PlatformState.Moving;
            
        }

        /*Keep developing*/
        public void SetSettings(MovingPath path, float speedVariation){
            
            this.path = path;
            speed = GameManager.instance.GetCurrentSpeed();
            currentPosition = GetComponent<Platform>().initialPosition;

            if(speedVariation > 0){
                speed = speed + (speed*(speedVariation*0.01f));
            }        

            pathPositions = new float[path.GetPathLength()];

            for(int i = 0; i < path.GetPathLength(); i++){
                switch(path.GetPathSegment(i).GetDirectionToMove()){

                    case Direction.Up:
                        currentPosition.x += (path.GetPathSegment(i).GetUnitsToMove() * GameManager.instance.GetXOffset());
                        pathPositions[i] = currentPosition.x;
                    break;

                    case Direction.Down:
                        currentPosition.x -= (path.GetPathSegment(i).GetUnitsToMove() * GameManager.instance.GetXOffset());
                        pathPositions[i] = currentPosition.x;
                        
                    break;

                    case Direction.Right:
                        currentPosition.z -= (path.GetPathSegment(i).GetUnitsToMove() * GameManager.instance.GetZOffset());
                        pathPositions[i] = currentPosition.z;
                    break;

                    case Direction.Left: 
                        currentPosition.z += (path.GetPathSegment(i).GetUnitsToMove() * GameManager.instance.GetZOffset());
                        pathPositions[i] = currentPosition.z;     
                    break;
                }
            }
            
            platformState = PlatformState.Moving;
        }
        
        public void StopMoving(){
            platformState = PlatformState.Stopped;
        }

        public void StartMoving(){
            platformState = PlatformState.Moving;
        }
    }   
}