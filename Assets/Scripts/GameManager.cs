using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
   
    [Header("Settings")]
    [SerializeField] private Difficulty difficultySetting; //The current Level's Difficulty
    [SerializeField] private GameSpeed gameSpeedSetting; //The current Level's platform speed

    [Header("Possible Speed Values")]
    [SerializeField] private float[] speedValues = {1f,2f,3f,4f}; //The possible speed values the level can have

    [Header("Game Parameters")]
    [SerializeField] private GameObject stage; //A reference to the stage object
    [SerializeField] private GameObject[] platformPool; //The possible platforms the level can have (in the PlatformType's enum order)
    [SerializeField] private Vector3 platformsOrigin = new Vector3 (1.95f, 0.05f, -2.5f); //The position where the platforms will start to be generated
    [SerializeField] private Vector3 platformsScale = new Vector3 (1.5f, 0.35f, 1.5f); //The Scale of the platforms
    [SerializeField] private float platformXOffset = 1.7f; 
    [SerializeField] private float platformZOffset = 1.7f;
    [SerializeField] private float currentSpeed; //The current speed of the level selected from the possible speed values

    [Header("Level Layouts")]
    private LevelPool availableLevels; //A reference to all the possible platform combinations the stage can have. Stored on a JSON File.
    [SerializeField] private LayoutSettings levelSelected; //The platform combination selected from the available levels
    private List<GameObject> platformReferences = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        //Makes random selection of a Level Layout before Generating it
        LoadAvailableLevels();
        LoadLevelLayout();
        availableLevels = new LevelPool(1);
        for (int i = 0; i < 1; i++)
        {
            availableLevels.levels[i] = levelSelected;
        }
        
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Loads the avaiable level information from a JSON File 
    into the serializable object*/
    public void LoadAvailableLevels(){
        string path = "Assets/LevelPoolTest.txt";
        string[] json = File.ReadAllLines(path);
        availableLevels = JsonUtility.FromJson<LevelPool>(json[0]);
        levelSelected = availableLevels.levels[0];
    }

    public void SaveOnce(){
        string path = "Assets/LevelPoolTest.txt";
        Debug.Log(path);
        LevelPool ots = availableLevels;
        string json = JsonUtility.ToJson(ots);
        File.WriteAllText(path, json);
        
       // Debug.Log("The json string is: "+json);
    }

    /* Selects a level information at random from the avaiable levels
     and generates its corresponding objects on the Scene*/
    public void LoadLevelLayout(){

        //////////////////////////////////Random level selection goes here
        //currentSpeed = speedValues[(int) gameSpeedSetting]; //Sets Spped
        difficultySetting = levelSelected.difficulty;
        gameSpeedSetting = levelSelected.gameSpeed;
        currentSpeed = speedValues[(int) levelSelected.gameSpeed];
        float platformXLimit = platformsOrigin.x + (platformXOffset * 3); //Sets the spaces between rows an columns
        float platformZLimit = platformsOrigin.z + (platformZOffset * 3);
        int i=0; //keeps count of the object's index from the selected level

        /*This loop increments the value from the initial position on an interval equal to the
          spaces between each platform to instantiate them in the correct position*/
        for(float row = platformsOrigin.x; row <= platformXLimit; row += platformXOffset){ 
           for(float column = platformsOrigin.z; column <= platformZLimit; column += platformZOffset){

                /*If there is nothing to instantiate in this spot, the loop
                 continues to the next iterateration*/
                if(levelSelected.GetPlatformType(i) == PlatformType.None){  
                         i++;
                        continue;
                }

                /*The Manager creates a platform based on the platform types's enum value, 
                interpreting it as an index from its array of platforms*/
                GameObject g = Instantiate(platformPool[(int) levelSelected.GetPlatformType(i)],
                new Vector3(row,platformsOrigin.y,column),Quaternion.identity);
                g.transform.localScale = platformsScale;
                
                //NOTE: Add Object Attached (make g a parent of said object if aplicable)

                //Set the corresponding properties of the platform specified in the config file
                if(levelSelected.GetPlatformType(i) == PlatformType.DirectionChanger){
                    g.GetComponent<DirectionChangerPlatform>().SetSettings(levelSelected.GetPlatformSettings(i)); 
                }
                
                //Add the Moving Platform's properties to the created platform if aplicable
                if(levelSelected.GetPlatformSettings(i).IsThePlatformMovable()){
                    g.AddComponent<MovingPlatform>().SetSettings(levelSelected.GetPlatformSettings(i));
                }
                g.transform.parent = stage.transform; 
                i++;
           }
        }  
    }

    
    ///Stage Editor Methods (Debug only)
    public void SetPlatformSetting(PlatformSettings ps){
        
    }

    public float GetXOffset(){
        return platformXOffset;
    }

    public float GetZOffset(){
        return platformZOffset;
    }

    public float GetCurrentSpeed(){
        return currentSpeed;
    }

}
