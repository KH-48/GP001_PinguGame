using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
       if(other.CompareTag("Player")){
         PlayerController.instance.StopMoving();
         Debug.Log("Fell!");
         PlayerController.instance.Respawn();
       }
   }
}
