using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    public static bool InfiniteTimeEnable;
    
    public void setFullscreen(bool fullscreen)
    {
        if(fullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
    public void InfiniteTimeToggle(bool tickOn)
    {
        if(tickOn)
        {
            InfiniteTimeEnable = true;
        }
        else{
            InfiniteTimeEnable = false;
        }
    }
}
