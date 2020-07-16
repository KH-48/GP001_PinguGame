using UnityEngine;

public class MovingPlatform : Platform {


    [SerializeField] private enum PlatformState{Stopped, Moving};
    [SerializeField] private PlatformState platformState;
    [SerializeField] private int unitsToMove=0;
    [SerializeField] private Direction directionToMove;
    [SerializeField] private float speed;
    [SerializeField] private bool goingInReverse = false;
    private float basePosition;
    private float targetPosition;
    private float lastTargetPosition;

    void Update(){

        switch(platformState){

            case PlatformState.Stopped:
            break;

            case PlatformState.Moving:
                
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

            break;
        }

    }

    public override void SetSettings(PlatformSettings ps){

        settings = ps;
        
        unitsToMove = ps.GetUnitsToMove();
        directionToMove = ps.GetDirectionToMove();
        speed = GameManager.instance.GetCurrentSpeed();
        switch(unitsToMove){

            case 2:
                speed = speed + (0.25f * speed);
                break;

            case 3:
                speed = speed + (0.75f * speed); 
                break;
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

    protected override void TriggerPlatformEvent(){

    }
    
    public void Stop(){
        platformState = PlatformState.Stopped;
    }

    public void Move(){
        platformState = PlatformState.Moving;
    }
}