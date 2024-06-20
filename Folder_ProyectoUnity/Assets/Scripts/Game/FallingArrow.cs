using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteType { Normal, Trail }

public class FallingArrow : MonoBehaviour
{
    public NoteType Type = NoteType.Normal;
    public SpriteRenderer ArrowSprite;
    public Animator Anim;
    public LineRenderer Trail;

    [HideInInspector] public PlayerChart CurrentChart;
    [HideInInspector] public Direction direction;
    [HideInInspector] public float NormalizedPosition;
    [HideInInspector] public Vector2 LocalStartPosition;
    [HideInInspector] public Vector2 LocalEndPosition;
    [HideInInspector] public float ChartSpeed = 5;
    [HideInInspector] public float TrailLenght = 0.5f;

    // MISC
    [HideInInspector] public bool TrailBeingConsumed = false;
    //[HideInInspector] public float TrailConsuption = 0;
    Vector3 TrailEnd;
    Vector3 TrailWorldEnd;

    private void Start()
    {
        if (Type == NoteType.Trail)
        {
            Trail.enabled = true;
        }
    }

    public void FixedUpdate()
    {
        // MOVEMENT
        NormalizedPosition += ChartSpeed * Time.fixedDeltaTime;
        transform.localPosition =
            Vector3.LerpUnclamped(LocalStartPosition, LocalEndPosition, NormalizedPosition);

        if (Type == NoteType.Normal)
        {
            // MISS
            if (NormalizedPosition > 1.1) { CurrentChart.VoxVolume = 0; }
            if (NormalizedPosition > 1.5f)
            {
          //      PlayerScore.AddScore(CurrentChart.ID, CurrentChart, 0, 0, 1, 0);
                Destroy(gameObject);
            }
        }
        else if (Type == NoteType.Trail)
        {
            TrailEnd = Vector3.LerpUnclamped(LocalStartPosition, LocalEndPosition, NormalizedPosition - TrailLenght);
            TrailWorldEnd = CurrentChart.transform.TransformPoint(TrailEnd);

            transform.rotation = Quaternion.identity;
            if (!TrailBeingConsumed)
            {
                Trail.enabled = true;
                Trail.SetPosition(0, transform.position);
                Trail.SetPosition(1, TrailWorldEnd);
                if (NormalizedPosition > 1.1f)
                {
                    CurrentChart.VoxVolume = 0;
                    Destroy(gameObject);
                }
            }
            else
            {
                Vector3 endpos = CurrentChart.transform.TransformPoint(LocalEndPosition);
                Trail.SetPosition(0, endpos);
                Trail.SetPosition(1, TrailWorldEnd);
                if (NormalizedPosition > 1f + TrailLenght)
                {
                    Destroy(gameObject);
                }
            }
        }

        if (Anim)
        {
            Anim.SetFloat("Speed", ChartSpeed);
        }
    }
}

