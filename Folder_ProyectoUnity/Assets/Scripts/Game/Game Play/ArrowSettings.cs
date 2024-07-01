using UnityEngine;
[CreateAssetMenu(fileName = "ArrowSprites", menuName = "GameSettings/ArrowSprites", order = 1)]
public class ArrowSprites : ScriptableObject
{
    public Sprite normalSprite;      // Sprite en estado normal
    public Sprite pressedSprite;     // Sprite en estado presionado
    public Sprite hitSprite;               //sprite cuando acierta en flecha

}