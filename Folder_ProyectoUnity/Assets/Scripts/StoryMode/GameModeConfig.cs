using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameModeConfig", menuName = " Settings/Game Mode Config", order = 1)]
public class GameModeConfig : ScriptableObject
{
    public string modeName;
    public float noteSpeed;
    public int maxErrorsAllowed;
    public float noteTimingWindow;
    public float healthDrainRate;
}
