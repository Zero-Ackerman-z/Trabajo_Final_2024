using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ArrowSpriteController : MonoBehaviour
{
    public Image arrowImage;
    public ArrowSprites arrowSprites;

    private void Awake()
    {
        if (arrowImage == null)
        {
            arrowImage = GetComponent<Image>();
        }
        SetNormalSprite();
    }

    public void SetNormalSprite()
    {
        if (arrowImage != null && arrowSprites != null)
        {
            arrowImage.sprite = arrowSprites.normalSprite;
        }
    }
    public void SetPressedSprite()
    {
        if (arrowImage != null && arrowSprites != null)
        {
            arrowImage.sprite = arrowSprites.pressedSprite;
        }
    }
    public void SetHitSprite()
    {
        if (arrowImage != null && arrowSprites != null)
        {
            arrowImage.sprite = arrowSprites.hitSprite;
        }
    }
}

