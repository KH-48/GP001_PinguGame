using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguGame01
{
    public class Wall : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other) {
            if(other.transform.tag == "Player"){
                PlayerController.instance.StopMoving();
            }
        }
    }    
}
