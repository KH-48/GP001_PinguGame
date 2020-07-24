using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguGame01
{
    [System.Serializable]
    public class LevelPool 
    {
        //public LevelSettings[] levels;
        public List<LevelSettings> levels = new List<LevelSettings>();

        public LevelPool(){
            
        }

    }
}
