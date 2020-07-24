using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PinguGame01
{
    public class StandardPlatform : Platform
    {

        public override void SetSettings(PlatformSettings ps){
            this.settings = ps;
        }

        protected override void TriggerPlatformEvent(){
            
        }
    }
}
