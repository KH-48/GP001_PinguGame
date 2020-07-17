using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelListContentDisplayer : MonoBehaviour
{

    public GameObject levelDetails;
    [SerializeField] private List<GameObject> createdObjectRefs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    
    }

    public void DisplayList(){
        LevelSettings[] levelsToDisplay = GameManager.instance.GetAvailableLevels();
        for(int i = 0; i < levelsToDisplay.Length; i++){
            GameObject details = Instantiate(levelDetails, transform);
            details.GetComponent<LevelDetails>().FillDetails(levelsToDisplay[i]);
            createdObjectRefs.Add(details);
        }
    }

    public void DestroyExistingElements(){
        for(int i = 0; i < createdObjectRefs.Count; i++){
            Destroy(createdObjectRefs[i]);//If it already Exists, destroys it
        }
        createdObjectRefs.RemoveRange(0,createdObjectRefs.Count); 
        
    }
}
