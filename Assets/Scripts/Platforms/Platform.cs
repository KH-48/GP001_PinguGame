using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{

    public PlatformSettings settings;

    public abstract void SetSettings(PlatformSettings ps);

    protected abstract void TriggerPlatformEvent();

    public PlatformSettings GetSettings(){
        return settings;
    }

}
