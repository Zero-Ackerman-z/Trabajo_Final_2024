using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BeatRate { Quarter, Half, Full, Double, Quadruple }

public class CharacterSpriteData : MonoBehaviour
{
    public Animator Anim;
    public BeatRate BopRate = BeatRate.Full;

    private void Start()
    {
        switch (BopRate)
        {
            case BeatRate.Quarter: ChartSpawner.BeatQuarter += BeatResponse; break;
            case BeatRate.Half: ChartSpawner.BeatHalf += BeatResponse; break;
            case BeatRate.Full: ChartSpawner.Beat += BeatResponse; break;
            case BeatRate.Double: ChartSpawner.BeatDouble += BeatResponse; break;
            case BeatRate.Quadruple: ChartSpawner.BeatQuadruple += BeatResponse; break;
        }
    }

    private void OnDestroy()
    {
        switch (BopRate)
        {
            case BeatRate.Quarter: ChartSpawner.BeatQuarter -= BeatResponse; break;
            case BeatRate.Half: ChartSpawner.BeatHalf -= BeatResponse; break;
            case BeatRate.Full: ChartSpawner.Beat -= BeatResponse; break;
            case BeatRate.Double: ChartSpawner.BeatDouble -= BeatResponse; break;
            case BeatRate.Quadruple: ChartSpawner.BeatQuadruple -= BeatResponse; break;
        }
    }

    public void PlayHitAnimation(Direction dir, bool hit)
    {
        if (hit)
        {
            switch (dir)
            {
                case Direction.Up: Anim.SetTrigger("Hit_Up"); break;
                case Direction.Down: Anim.SetTrigger("Hit_Down"); break;
                case Direction.Left: Anim.SetTrigger("Hit_Left"); break;
                case Direction.Right: Anim.SetTrigger("Hit_Right"); break;
            }
        }
        else
        {
            switch (dir)
            {
                case Direction.Up: Anim.SetTrigger("Miss_Up"); break;
                case Direction.Down: Anim.SetTrigger("Miss_Down"); break;
                case Direction.Left: Anim.SetTrigger("Miss_Left"); break;
                case Direction.Right: Anim.SetTrigger("Miss_Right"); break;
            }
        }

    }

    public void BeatResponse()
    {
        Anim.SetTrigger("Beat");
    }

}
