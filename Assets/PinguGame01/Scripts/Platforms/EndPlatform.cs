using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguGame01
{
  public class EndPlatform : MonoBehaviour
  {
      private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
          PlayerController.instance.StopMoving();
          Debug.Log("Reached!!!!!");
          PlayerController.instance.Respawn();
        }
    }
  }
 
}
