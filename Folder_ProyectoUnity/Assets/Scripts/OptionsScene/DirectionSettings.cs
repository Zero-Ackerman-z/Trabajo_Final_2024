using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DirectionSettings", menuName = "GameSettings/DirectionSettings", order = 1)]
public class DirectionSettings : ScriptableObject
{
    public int currentDirectionIndex = 0; // 0 para Up-Scroll, 1 para Down-Scroll

    public string[] arrowDirections = { "Up-Scroll", "Down-Scroll" };

    public void ChangeDirection(int direction)
    {
        currentDirectionIndex = (currentDirectionIndex + direction + arrowDirections.Length) % arrowDirections.Length;
    }

    public string GetCurrentDirection()
    {
        return arrowDirections[currentDirectionIndex];
    }
}