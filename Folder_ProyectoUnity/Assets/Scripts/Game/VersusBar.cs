using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersusBar : MonoBehaviour
{
    public static VersusBar CurrentBar;

    [Header("References")]
    public PlayerChart[] PlayerCharts;
    public Image RightSprite;
    public RectTransform RightBar;
    public Image LeftSprite;
    public RectTransform LeftBar;
    public RectTransform DownscrollBarPosition;

    [Header("Images")]
    public Sprite RightLosing;
    public Sprite RightMiddle;
    public Sprite RightWinning;
    public Sprite LeftLosing;
    public Sprite LeftMiddle;
    public Sprite LeftWinning;

    [Header("Parameters")]
    public float IconsPositionMultiplier = 0.1f;
    public float BarMaxSize = 250;
    public float BarPosition = 0;

    float BarLSize;
    float BarRSize;

    private void Start()
    {
        CurrentBar = this;
        SetBar(0, 0);

        int downscrollAmm = 0;
        for (int i = 0; i < PlayerCharts.Length; i++)
        { if (PlayerCharts[i].DownScroll) downscrollAmm += 1; }

        if (downscrollAmm > 1 && DownscrollBarPosition)
        {
            transform.position = DownscrollBarPosition.position;
        }
    }

    public void SetBar(float value, int ID)
    {
        if (ID == 0)
        {
            value = value * Mathf.Lerp(0, 1, BarPosition + 0.5f);
            BarPosition -= value;
        }
        else
        {
            value = value * Mathf.Lerp(1, 0, BarPosition - 0.5f);
            BarPosition += value;
        }

        Debug.Log("BARTEST: " + value);

        BarRSize = Mathf.Lerp(0, BarMaxSize, BarPosition);
        BarLSize = Mathf.Lerp(BarMaxSize, 0, BarPosition);
        RightBar.sizeDelta = new Vector2(BarRSize, RightBar.sizeDelta.y);
        LeftBar.sizeDelta = new Vector2(BarLSize, LeftBar.sizeDelta.y);

        RightSprite.rectTransform.position =
            new Vector2(RightBar.position.x + (BarRSize * IconsPositionMultiplier * transform.localScale.x), RightSprite.rectTransform.position.y);

        LeftSprite.rectTransform.position =
            new Vector2(LeftBar.position.x - (BarLSize * IconsPositionMultiplier * transform.localScale.x), LeftSprite.rectTransform.position.y);

        if (BarPosition < 0.25f)
        {
            RightSprite.sprite = RightLosing;
            LeftSprite.sprite = LeftWinning;
        }
        else if (BarPosition > 0.25f && BarPosition < 0.75f)
        {
            RightSprite.sprite = RightMiddle;
            LeftSprite.sprite = LeftMiddle;
        }
        else if (BarPosition > 0.75f)
        {
            RightSprite.sprite = RightWinning;
            LeftSprite.sprite = LeftLosing;
        }
    }




}


