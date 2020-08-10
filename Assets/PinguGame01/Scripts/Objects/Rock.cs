using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguGame01
{
    public class Rock : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other) {
            PlayerController.instance.StopMoving();
            PlayerController.instance.Jump();
            PlayerController.instance.Respawn();
            Debug.Log("Oh no!!");
        }
    }
    
}
