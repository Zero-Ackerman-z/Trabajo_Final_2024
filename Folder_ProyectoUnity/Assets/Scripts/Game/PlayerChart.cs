using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction { Up, Down, Left, Right }
public enum ControlType { Keys, WASD, DFJK }
public class PlayerChart : MonoBehaviour
{
    [Header("Data")]
    public int ID = 0;
    public List<ControlType> Control;
    public bool Bot = false;

    [Header("Gameplay")]
    public float InputLeaniency = 0.1f;
    public float NotesSpeed = 0.1f;
    public bool DownScroll = true;

    [Header("References")]
    public ChartSpawner Spawner;
    public GameObject ArrowPrefab;
    public GameObject HoldNotePrefab;
    [HideInInspector] public List<FallingArrow> Arrows = new List<FallingArrow>();
    [HideInInspector] public List<FallingArrow> HoldNotes = new List<FallingArrow>();
    public Transform SpawnPosition;
    public Transform RightArrow;
    public Transform UpArrow;
    public Transform DownArrow;
    public Transform LeftArrow;
    public float ArrowShrinkOnPress = 0.9f;

    [Header("Character")]
    public CharacterSpriteData[] Characters;

    [Header("Details")]
    public Color RightArrowColor = Color.red;
    public Color UpArrowColor = Color.green;
    public Color DownArrowColor = Color.blue;
    public Color LeftArrowColor = Color.yellow;
    public float BaseArrowsDesaturation = 0.8f;

    [Header("Effects")]
    public ParticleSystem RightArrowParticle;
    public ParticleSystem UpArrowParticle;
    public ParticleSystem DownArrowParticle;
    public ParticleSystem LeftArrowParticle;
    public ParticleSystem RankTextSick;
    public ParticleSystem RankTextGood;
    public ParticleSystem RankTextBad;
    public ParticleSystem RankTextShizzle;

    [Header("Scoring")]
    public float ScorePerNote = 10;
    public float HoldingScore = 3.5f;
    public float ReleaseCountsAsMissThreshold = 0.5f;
    public float BarGainPerHit = 0.05f;
    public float BarLossPerMiss = 0.1f;

    // === EXTRA ===
    FallingArrow right;
    FallingArrow left;
    FallingArrow up;
    FallingArrow down;
    Vector3[] ArrowScales = new Vector3[4];
    //public float RandomSpawnSpeed = 0.8f;
    bool HoldingRight = false;
    bool HoldingLeft = false;
    bool HoldingUp = false;
    bool HoldingDown = false;
    float HoldingFxCounter = 0;
    [HideInInspector] public float VoxVolume = 1;

    // === TEST ===
    //float c;

    private void Start()
    {
        /*/ SET CONTROLLER TYPE IF IT WAS SET ON CONTROLLER SCREEN
        if (ID == 0 && MainUIAndSettings.Player1ControlType.Count > 0)
        {
            Control = MainUIAndSettings.Player1ControlType;
        }
        else if (ID == 1 && MainUIAndSettings.Player2ControlType.Count > 0)
        {
            Control = MainUIAndSettings.Player2ControlType;
        }

        DownScroll = MainUIAndSettings.UseDownScroll;*/

        // SET INITIAL BASE ARROWS COLOR
        RightArrow.GetComponent<SpriteRenderer>().color =
            Color.Lerp(RightArrowColor, Color.white, BaseArrowsDesaturation);
        UpArrow.GetComponent<SpriteRenderer>().color =
            Color.Lerp(UpArrowColor, Color.white, BaseArrowsDesaturation);
        DownArrow.GetComponent<SpriteRenderer>().color =
            Color.Lerp(DownArrowColor, Color.white, BaseArrowsDesaturation);
        LeftArrow.GetComponent<SpriteRenderer>().color =
            Color.Lerp(LeftArrowColor, Color.white, BaseArrowsDesaturation);

        ParticleSystem.MainModule p;
        p = RightArrowParticle.main; p.startColor = RightArrowColor;
        p = UpArrowParticle.main; p.startColor = UpArrowColor;
        p = DownArrowParticle.main; p.startColor = DownArrowColor;
        p = LeftArrowParticle.main; p.startColor = LeftArrowColor;

        ArrowScales[0] = RightArrow.localScale;
        ArrowScales[1] = UpArrow.localScale;
        ArrowScales[2] = DownArrow.localScale;
        ArrowScales[3] = LeftArrow.localScale;

        // MAKE BOT IF NO CONTROL
        if (Control.Count == 0) { Bot = true; }
        else { Bot = false; }

        // DOWN SCROLL
        if (DownScroll)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
            UpArrow.Rotate(0, 180, 0);
            DownArrow.Rotate(0, 180, 0);
        }
    }

    private void FixedUpdate()
    {
        if (Bot)
        {
            //PRESS ARROWS AUTOMATICALLY
            for (int i = 0; i < Arrows.Count; i++)
            {
                if (Arrows[i].NormalizedPosition >= 0.98f)
                {
                    PressCheck(Arrows[i].direction, true);
                }
            }

            HoldCheck();
        }
    }

    private void Update()
    {
        //c += Time.deltaTime;
        //if(c > RandomSpawnSpeed)
        //{
        //    c = 0; CreateFallingArrow
        //        ((Direction)Random.Range(0, 4), (NoteType)Random.Range(0, 2), Random.Range(0.1f,0.5f));
        //}

        // BOT && INPUT
        if (Bot)
        {
            // ON FIXED UPDATE
        }
        else
        {
            HoldCheck();
        }

        for (int i = 0; i < Control.Count; i++)
        {
            if (Bot) { break; }
            if (Control[i] == ControlType.Keys)
            {
                if (Input.GetButtonDown("Right")) { PressCheck(Direction.Right, false); }
                if (Input.GetButtonDown("Left")) { PressCheck(Direction.Left, false); }
                if (Input.GetButtonDown("Down")) { PressCheck(Direction.Down, false); }
                if (Input.GetButtonDown("Up")) { PressCheck(Direction.Up, false); }
            }
            else if (Control[i] == ControlType.WASD)
            {
                if (Input.GetButtonDown("D")) { PressCheck(Direction.Right, false); }
                if (Input.GetButtonDown("A")) { PressCheck(Direction.Left, false); }
                if (Input.GetButtonDown("S")) { PressCheck(Direction.Down, false); }
                if (Input.GetButtonDown("W")) { PressCheck(Direction.Up, false); }
            }
            else if (Control[i] == ControlType.DFJK)
            {
                if (Input.GetButtonDown("K")) { PressCheck(Direction.Right, false); }
                if (Input.GetButtonDown("D")) { PressCheck(Direction.Left, false); }
                if (Input.GetButtonDown("F")) { PressCheck(Direction.Down, false); }
                if (Input.GetButtonDown("J")) { PressCheck(Direction.Up, false); }
            }
        }

        // VOX VOLUME
        if (Spawner.Vox && Spawner)
        {
            Spawner.Vox.volume = Mathf.Lerp(Spawner.Vox.volume, VoxVolume, Time.deltaTime * 50);
        }

    }

    // CHECK IF ARROW IS HIT AND DO STUFF // 1-Right 2-Down 3-Up 4-Left
    void PressCheck(Direction dir, bool isBot)
    {
        bool Done = false;

        // REMOVE NULLS
        for (var i = Arrows.Count - 1; i > -1; i--)
        {
            if (Arrows[i] == null) { Arrows.RemoveAt(i); }
        }

        // CHECK ARROWS
        for (int i = 0; i < Arrows.Count; i++)
        {
            if (!Done && Arrows[i].direction == dir)
            {
                CheckAccuracy(Arrows[i].NormalizedPosition, Arrows[i], GetArrowTransform(dir), dir);
            }
        }

        // IF ARROW HIT OR MISS, DO...
        float CheckAccuracy(float value, FallingArrow arrow, Transform arrowPos, Direction dir)
        {
            if (value >= 1 - InputLeaniency && value <= 1 + InputLeaniency)
            {
                // MANAGE OBJECT
                Destroy(arrow.gameObject);
                Done = true;

                // EFFECTS
                if (dir == Direction.Right && RightArrowParticle) { RightArrowParticle.Play(); }
                else if (dir == Direction.Down && UpArrowParticle) { UpArrowParticle.Play(); }
                else if (dir == Direction.Left && LeftArrowParticle) { LeftArrowParticle.Play(); }
                else if (dir == Direction.Up && DownArrowParticle) { DownArrowParticle.Play(); }

                float accuracy = 0;
                if (value > 1.0f) { accuracy = 1 - (value - 1); }
                else { accuracy = value; }
                accuracy = Mathf.Clamp01(Remap(accuracy, 0.8f, 1, 0.0f, 1) + 0.1f);
                //Debug.Log(accuracy);

                VoxVolume = 1;
                //PlayerScore.AddScore(ID, this, 1, ScorePerNote, 0, accuracy);

                // CHARACTER ANIMATION
                for (int i = 0; i < Characters.Length; i++) { Characters[i].PlayHitAnimation(dir, true); }

                // VERSUS BAR
                if (VersusBar.CurrentBar)
                {
                    if (!Bot) { VersusBar.CurrentBar.SetBar(BarGainPerHit * accuracy, ID); }
                    else { VersusBar.CurrentBar.SetBar(BarGainPerHit * (accuracy * 0.25f), ID); }
                }

                return accuracy;
            }
            else
            {
                // MISS
                if (Bot) { return 1; }
                VoxVolume = 0;
                //PlayerScore.AddScore(ID, this, 0, 0, 1, 0);
                VersusBar.CurrentBar.SetBar(-BarLossPerMiss, ID);

                // ANIMATION
                for (int i = 0; i < Characters.Length; i++) { Characters[i].PlayHitAnimation(dir, false); }
                return 0;
            }

        }
    }

    void HoldCheck()
    {
        // REMOVE NULLS
        for (var i = HoldNotes.Count - 1; i > -1; i--)
        {
            if (HoldNotes[i] == null) { HoldNotes.RemoveAt(i); }
        }

        //if(HoldNotes.Count <= 0) { return; }

        // CHECK ARROWS
        for (int i = 0; i < HoldNotes.Count; i++)
        {
            if (HoldNotes[i].NormalizedPosition >= 1 - InputLeaniency
                && HoldNotes[i].NormalizedPosition <= 1 + InputLeaniency)
            {
                switch (HoldNotes[i].direction)
                {
                    case Direction.Up: up = HoldNotes[i]; break;
                    case Direction.Down: down = HoldNotes[i]; break;
                    case Direction.Left: left = HoldNotes[i]; break;
                    case Direction.Right: right = HoldNotes[i]; break;
                }
            }
        }

        RightArrow.localScale = Vector3.Lerp(RightArrow.localScale, ArrowScales[0], Time.deltaTime * 40);
        UpArrow.localScale = Vector3.Lerp(UpArrow.localScale, ArrowScales[1], Time.deltaTime * 40);
        DownArrow.localScale = Vector3.Lerp(DownArrow.localScale, ArrowScales[2], Time.deltaTime * 40);
        LeftArrow.localScale = Vector3.Lerp(LeftArrow.localScale, ArrowScales[3], Time.deltaTime * 40);

        for (int i = 0; i < Control.Count; i++)
        {
            if (Bot) { break; }
            if (Control[i] == ControlType.Keys) // PLAYER 1
            {
                if (Input.GetButton("Right"))
                {
                    Holding(right, Direction.Right); HoldingRight = true;
                    RightArrow.localScale = ArrowScales[0] * ArrowShrinkOnPress;
                }
                else if (Input.GetButtonUp("Right"))
                { Release(right); }

                if (Input.GetButton("Left"))
                {
                    Holding(left, Direction.Left); HoldingLeft = true;
                    LeftArrow.localScale = ArrowScales[1] * ArrowShrinkOnPress;
                }
                else if (Input.GetButtonUp("Left"))
                { Release(left); }

                if (Input.GetButton("Down"))
                {
                    Holding(down, Direction.Down); HoldingDown = true;
                    UpArrow.localScale = ArrowScales[2] * ArrowShrinkOnPress;
                }
                else if (Input.GetButtonUp("Down"))
                { Release(down); }

                if (Input.GetButton("Up"))
                {
                    Holding(up, Direction.Up); HoldingUp = true;
                    DownArrow.localScale = ArrowScales[3] * ArrowShrinkOnPress;
                }
                else if (Input.GetButtonUp("Up"))
                { Release(up); }
            }
            else if (Control[i] == ControlType.WASD) // PLAYER 2
            {
                if (Input.GetButton("D"))
                {
                    Holding(right, Direction.Right); HoldingRight = true;
                    RightArrow.localScale = ArrowScales[0] * ArrowShrinkOnPress;
                }
                else if (Input.GetButtonUp("D"))
                { Release(right); }

                if (Input.GetButton("A"))
                {
                    Holding(left, Direction.Left); HoldingLeft = true;
                    LeftArrow.localScale = ArrowScales[1] * ArrowShrinkOnPress;
                }
                else if (Input.GetButtonUp("A"))
                { Release(left); }

                if (Input.GetButton("S"))
                {
                    Holding(down, Direction.Down); HoldingDown = true;
                    UpArrow.localScale = ArrowScales[2] * ArrowShrinkOnPress;
                }
                else if (Input.GetButtonUp("S"))
                { Release(down); }

                if (Input.GetButton("W"))
                {
                    Holding(up, Direction.Up); HoldingUp = true;
                    DownArrow.localScale = ArrowScales[3] * ArrowShrinkOnPress;
                }
                else if (Input.GetButtonUp("W"))
                { Release(up); }
            }
            else if (Control[i] == ControlType.DFJK) // PLAYER 2
            {
                if (Input.GetButton("K"))
                {
                    Holding(right, Direction.Right); HoldingRight = true;
                    RightArrow.localScale = ArrowScales[0] * ArrowShrinkOnPress;
                }
                else if (Input.GetButtonUp("K"))
                { Release(right); }

                if (Input.GetButton("D"))
                {
                    Holding(left, Direction.Left); HoldingLeft = true;
                    LeftArrow.localScale = ArrowScales[1] * ArrowShrinkOnPress;
                }
                else if (Input.GetButtonUp("D"))
                { Release(left); }

                if (Input.GetButton("F"))
                {
                    Holding(down, Direction.Down); HoldingDown = true;
                    UpArrow.localScale = ArrowScales[2] * ArrowShrinkOnPress;
                }
                else if (Input.GetButtonUp("F"))
                { Release(down); }

                if (Input.GetButton("J"))
                {
                    Holding(up, Direction.Up); HoldingUp = true;
                    DownArrow.localScale = ArrowScales[3] * ArrowShrinkOnPress;
                }
                else if (Input.GetButtonUp("J"))
                { Release(up); }
            }
        }

        //ALWAYS HOLD IF BOT
        if (Bot)
        {
            Holding(right, Direction.Right); HoldingRight = true;
            Holding(left, Direction.Left); HoldingLeft = true;
            Holding(down, Direction.Down); HoldingDown = true;
            Holding(up, Direction.Up); HoldingUp = true;
        }

        void Holding(FallingArrow arrow, Direction dir)
        {
            if (arrow)
            {
                //FX AND SCORE
                HoldingFxCounter += Time.deltaTime;
                if (HoldingFxCounter >= 0.1f)
                {
                    // ANIMATION
                    for (int i = 0; i < Characters.Length; i++) { Characters[i].PlayHitAnimation(dir, true); }

                    HoldingFxCounter = 0;
                    switch (dir)
                    {
                        case Direction.Down: UpArrowParticle.Play(); break;
                        case Direction.Up: DownArrowParticle.Play(); break;
                        case Direction.Left: LeftArrowParticle.Play(); break;
                        case Direction.Right: RightArrowParticle.Play(); break;
                    }

                    //SCORE
                    //PlayerScore.AddScore(ID, this, 0, HoldingScore * 0.05f, 0, 0);
                    VoxVolume = 1;
                }
                arrow.TrailBeingConsumed = true;
            }
        }

        void Release(FallingArrow arrow)
        {
            if (arrow)
            {
                // COUNT AS A MISS IF RELEASE TOO EARLY
                if (arrow.NormalizedPosition < 1 + (arrow.TrailLenght / ReleaseCountsAsMissThreshold))
                {
                    //PlayerScore.AddScore(ID, this, 0, 0, 1, 0);
                }

                Destroy(arrow.gameObject);
            }
        }
    }

    public void CreateFallingArrow(Direction dir, NoteType type, float modifier)
    {
        // SETUP
        Color color = Color.white;
        Vector2 position = Vector2.zero;
        Vector2 endposition = Vector2.zero;
        Vector3 rotation = Vector3.zero;
        if (dir == Direction.Right)
        {
            color = RightArrowColor;
            position = new Vector2(RightArrow.localPosition.x, SpawnPosition.localPosition.y);
            endposition.x = position.x; endposition.y = RightArrow.localPosition.y;
            rotation.z = 0;
        }
        else if (dir == Direction.Up)
        {
            color = DownArrowColor;
            position = new Vector2(DownArrow.localPosition.x, SpawnPosition.localPosition.y);
            endposition.x = position.x; endposition.y = DownArrow.localPosition.y;
            rotation.z = 90;
        }
        else if (dir == Direction.Down)
        {
            color = UpArrowColor;
            position = new Vector2(UpArrow.localPosition.x, SpawnPosition.localPosition.y);
            endposition.x = position.x; endposition.y = UpArrow.localPosition.y;
            rotation.z = -90;
        }
        else if (dir == Direction.Left)
        {
            color = LeftArrowColor;
            position = new Vector2(LeftArrow.localPosition.x, SpawnPosition.localPosition.y);
            endposition.x = position.x; endposition.y = LeftArrow.localPosition.y;
            rotation.z = 180;
        }

        GameObject g;
        FallingArrow f;
        if (type == NoteType.Normal)
        {
            g = GameObject.Instantiate(ArrowPrefab, transform);
            f = g.GetComponent<FallingArrow>();
            Arrows.Add(f);
        }
        else if (type == NoteType.Trail)
        {
            g = GameObject.Instantiate(HoldNotePrefab, transform);
            CreateFallingArrow(dir, NoteType.Normal, 0);
            f = g.GetComponent<FallingArrow>();

            Gradient grad = new Gradient();
            GradientColorKey[] key = new GradientColorKey[2];
            GradientAlphaKey[] alpha = new GradientAlphaKey[2];
            key[0].color = color;
            key[0].time = 0;
            key[1].color = color;
            key[1].time = 1;
            alpha[0].alpha = 1;
            alpha[0].time = 0;
            alpha[1].alpha = 1;
            alpha[1].time = 1;
            grad.SetKeys(key, alpha);
            f.Trail.colorGradient = grad;
            f.TrailLenght = modifier;

            HoldNotes.Add(f);
        }
        else // Never true but compiler accepts it
        {
            f = new FallingArrow();
            g = GameObject.Instantiate(ArrowPrefab, transform);
        }

        f.direction = dir;
        f.CurrentChart = this;
        f.ChartSpeed = NotesSpeed;
        f.LocalStartPosition = position;
        f.LocalEndPosition = endposition;
        f.transform.eulerAngles = rotation + transform.rotation.eulerAngles;
        f.FixedUpdate();
        SpriteRenderer s = g.GetComponent<SpriteRenderer>();
        s.color = color;
    }

    public static float Remap(float value, float start1, float stop1, float start2, float stop2)
    {
        return start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
    }

    Transform GetArrowTransform(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up: return DownArrow;
            case Direction.Down: return UpArrow;
            case Direction.Left: return LeftArrow;
            case Direction.Right: return RightArrow;
            default: return DownArrow;
        }
    }

    Color GetArrowColor(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up: return DownArrowColor;
            case Direction.Down: return UpArrowColor;
            case Direction.Left: return LeftArrowColor;
            case Direction.Right: return RightArrowColor;
            default: return DownArrowColor;
        }
    }

}