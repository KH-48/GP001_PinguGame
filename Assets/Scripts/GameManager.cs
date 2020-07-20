using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject platformInfo;
    [Header("Current Mode")]
    private GameMode currentGameMode;
    private enum GameMode {Debug, Test_Load};
    [Header("Game Settings")]
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
    [SerializeField] private LevelSettings levelSelected = null; //The platform combination selected from the available levels
    private List<GameObject> platformReferences = new List<GameObject>();
    [SerializeField] private int selectedPlatformIndex;
    private Vector3[] platformPositions = new Vector3[16];

    

    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        //Makes random selection of a Level Layout before Generating it
        //HeaderLevelDetails.instance.FillDetails(new LevelSettings());
        LoadLevelsFromFile();
        if(currentGameMode == GameMode.Test_Load){
            //SelectRandomLevel();
            //LoadLevelLayout();
        }
         
    }

    void Update(){
        if (Input.GetMouseButtonDown (1)) {
         RaycastHit hit;
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if(Physics.Raycast (ray, out hit)){
                if(hit.transform.tag == "Platform"){
                 for(int i = 0; i < platformReferences.Count; i++){
                        if(platformReferences[i] == hit.transform.gameObject){
                            Debug.Log ("Platform "+ i +" Selected");
                            platformInfo.SetActive(true);
                            PlatformInfoPanelHandler.instance.FillValues(i,levelSelected.layoutPlatforms[i]);
                            selectedPlatformIndex = i;
                        }
                     }
                }
            }
        }
    }

    /* Loads the avaiable level information from a JSON File 
    into the serializable object*/
    public void LoadLevelsFromFile(){
        
        string path = "Assets/Levels/LevelPoolTest.json";
        string[] json = null;
        if(File.Exists(path)){
            json = File.ReadAllLines(path);
        }else{
            Debug.Log("No hay un archivo disponible, creando...");
            File.Create(path);
        }
        if(json.Length > 0){
            availableLevels = JsonUtility.FromJson<LevelPool>(json[0]);
            Debug.Log("There's " + GetAvailableLevels().Length +
             " Available Levels from a Total of "+availableLevels.levels.Count+" Levels");
        }
        
    }

    public void ResetField(){
        for(int i = 0; i < platformReferences.Count; i++){
            Destroy(platformReferences[i]);
        }
        platformReferences.RemoveRange(0, platformReferences.Count);
        levelSelected = null;
    }

    public void SaveOnce(){
        string path = "Assets/Levels/LevelPoolTest.json";
        Debug.Log(path);
        LevelPool objectToSave = availableLevels;
        string json = JsonUtility.ToJson(objectToSave);
        File.WriteAllText(path, json);   
    }

    /* Selects a level information at random from the avaiable levels
     and generates its corresponding objects on the Scene*/
    public void LoadLevelLayout(){

        //////////////////////////////////Random level selection goes here
        //currentSpeed = speedValues[(int) gameSpeedSetting]; //Sets Spped
        if(currentGameMode == GameMode.Debug){
            HeaderLevelDetails.instance.FillDetails(levelSelected);
        }
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

                platformPositions[i] = new Vector3(row, platformsOrigin.y, column);
        
                
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
                platformReferences.Add(g);
                i++;
           }
        }  
    }
    

    ///Stage Editor Methods (Debug only)
    public void SetCurrentLevel(LevelSettings level){
        if(levelSelected != level){
            ResetField();
            levelSelected = level;
            LoadLevelLayout();
        }else{
            Debug.Log("This level is currently Open");
        }
    }

    public void ResetLevel(){
        LevelSettings level = levelSelected;
        ResetField();
        levelSelected = level;
        LoadLevelLayout();
    }

    public void CreateNewLevel(LevelSettings newLevel){
        newLevel.layoutId = availableLevels.levels.Count + 1;
        availableLevels.levels.Add(newLevel);
        SetCurrentLevel(newLevel);
    }

    public void ModifyLevelDetails(LevelSettings newDetails){
      
        for(int i = 0; i < availableLevels.levels.Count; i++){
            if(availableLevels.levels[i].layoutId == newDetails.layoutId){
                int currentId = availableLevels.levels[i].layoutId;
                availableLevels.levels[i] = newDetails;
                availableLevels.levels[i].layoutId = currentId;
                ResetLevel();
                break;
            }
        }

    }

    public void DeleteLevel(LevelSettings level){
        bool found = false;
        for(int i = 0; i < availableLevels.levels.Count; i++){
            if(availableLevels.levels[i] == level && availableLevels.levels[i].active){
                found = true;
                availableLevels.levels[i].active = false;
                HeaderLevelDetails.instance.FillDetails(new LevelSettings());
                Debug.Log("Level Deleted");
                if(levelSelected == level){
                    ResetField();
                }
            }
        }
        if(!found){
            Debug.Log("Level Not Found");
        }
        
    }

    public void CreateNewPlatform(PlatformSettings newPlatformSettings){
        if(platformReferences[selectedPlatformIndex] != null){

            Vector3 position = platformPositions[selectedPlatformIndex];
            Destroy(platformReferences[selectedPlatformIndex]);
            
            /*The Manager creates a platform based on the platform types's enum value, 
                interpreting it as an index from its array of platforms*/
            GameObject g = Instantiate(platformPool[(int) newPlatformSettings.GetPlatformType()],
            position,Quaternion.identity);
            g.transform.localScale = platformsScale;

                //NOTE: Add Object Attached (make g a parent of said object if aplicable)

                //Set the corresponding properties of the platform specified in the config file
            if(newPlatformSettings.GetPlatformType() == PlatformType.DirectionChanger){
                g.GetComponent<DirectionChangerPlatform>().SetSettings(newPlatformSettings); 
            }
                
            //Add the Moving Platform's properties to the created platform if aplicable
            if(newPlatformSettings.IsThePlatformMovable()){
                g.AddComponent<MovingPlatform>().SetSettings(newPlatformSettings);
            }
            g.transform.parent = stage.transform; 
            platformReferences[selectedPlatformIndex] = g;

            levelSelected.layoutPlatforms[selectedPlatformIndex] = newPlatformSettings;

        }else{
            Debug.Log("There is no Platform Selected");
        }
    }

    //////////////Getters / Setters

    public LevelSettings GetCurrentLevel(){
        if(levelSelected == null){
            //return new LayoutSettings();
        }
        return levelSelected;
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

    public LevelSettings[] GetAvailableLevels(){
        List<LevelSettings> availableList = new List<LevelSettings>();
        for(int i = 0; i < availableLevels.levels.Count; i++){
            if(availableLevels.levels[i].active){
                availableList.Add(availableLevels.levels[i]);
            }
        }
        return availableList.ToArray();
    }

    public void StopPlatforms(){
        for(int i = 0; i < platformReferences.Count; i++){
            if(levelSelected.layoutPlatforms[i].IsThePlatformMovable()){
                platformReferences[i].GetComponent<MovingPlatform>().StopMoving();
            }
        }
    }

    public void MovePlatforms(){
        for(int i = 0; i < platformReferences.Count; i++){
            if(levelSelected.layoutPlatforms[i].IsThePlatformMovable()){
                platformReferences[i].GetComponent<MovingPlatform>().StartMoving();
            }
        }
    }

}
