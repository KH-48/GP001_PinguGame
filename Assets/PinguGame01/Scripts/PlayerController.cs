﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguGame01
{
    
    public class PlayerController : MonoBehaviour
    {
    
        public static PlayerController instance;
        public Rigidbody Rb;
        private bool canMove = true;
        [SerializeField] private enum gameState{Controlling,Resolving,Result,End};
        [SerializeField] private gameState currentGameState;
        [SerializeField] public Direction currentDirection;
        [SerializeField] private float moveSpeed = 5;
        [SerializeField] private float jumpTrust = 2.0f;
        [SerializeField] private bool jumping;
        [SerializeField] private Vector3 lastPosition;
        [Header("Constraints")]
        [SerializeField] private float rightConstraint;
        [SerializeField] private float leftConstraint;
        // Start is called before the first frame update
        void Start()
        {
            instance = this;
            Rb = GetComponent<Rigidbody>();
            currentGameState = gameState.Controlling;
        }

        // Update is called once per frame
        void Update()
        {
            ListenInputs();
            if(transform.position.y < -20){
                Respawn();
            }
        }

        private void ListenInputs(){
            switch(currentGameState){
                case gameState.Controlling:
                
                    if(canMove){
                        if(Input.GetKey(KeyCode.LeftArrow) && transform.position.z < leftConstraint){ //Left
                            MoveTo(Direction.Left);
                        }
                        if(Input.GetKey(KeyCode.RightArrow) && transform.position.z > rightConstraint){ //Right
                            MoveTo(Direction.Right);
                        }
                            
                        if(Input.GetKeyDown(KeyCode.Space)){
                            Jump();
                        }
                        if(Input.GetKeyDown(KeyCode.Z)){
                            lastPosition = transform.position;
                            currentDirection = Direction.Up;
                            currentGameState = gameState.Resolving;
                            GameManager.instance.StopPlatforms();
                        }
                    }
                break;
                case gameState.Resolving:
                    MoveTo(currentDirection);
                break;
                case gameState.Result:
                break;
            }
        }

        
        public void ChangeDirection(Direction d){
            currentDirection = d;
        }

        public void MoveTo(Direction d){

            switch(d){
                case Direction.Up:
                    transform.Translate(1*moveSpeed*Time.deltaTime,0f,0f);
                break;
                case Direction.Down:
                    transform.Translate(-1*moveSpeed*Time.deltaTime,0f,0f);
                break;
                case Direction.Right:
                    transform.Translate(0f,0f,-1*moveSpeed*Time.deltaTime);
                break;
                case Direction.Left:
                    transform.Translate(0f,0f,1*moveSpeed*Time.deltaTime);
                break;
            }

        }

        public void StopMoving(){
            currentGameState = gameState.Result;
        }

        IEnumerator ResetPosition(){
            yield return new WaitForSeconds(1.5f);
            transform.position = lastPosition;
            currentGameState = gameState.Controlling;
            GameManager.instance.ResetLevel();
        }

        public void Jump(){
            Rb.velocity = Vector3.up * jumpTrust;
            //jumping = true;
        }


        public void Respawn(){
            StartCoroutine(ResetPosition());
        }

        public void LockControls(bool option){
            canMove = option;
        }
    }
}