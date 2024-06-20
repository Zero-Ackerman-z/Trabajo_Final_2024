using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class ChartSpawner : MonoBehaviour
{

    public string FilePath = "/ChartTest.chart";
    public PlayerChart Player;
    public Chart chart;
    public bool SendBeatCalls = false;
    public float Offset = 1;
    public float BeatsModifier = 1;
    float timer = 0;
    bool Playing = false;

    public static float CurrentOffset;
    public static float CurrentNotesSpeed;

    [Header("Audio Sources")]
    public AudioSource Vox;
    public AudioSource Instruments;
    public List<Coroutine> NotesQueue = new List<Coroutine>();
    public List<Coroutine> Beats = new List<Coroutine>();

    private void Start()
    {
        if (Vox) Vox.clip.LoadAudioData();
        if (Instruments) Instruments.clip.LoadAudioData();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void Update()
    {

        // CREATE CHART
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartChart(FilePath);
        }

        if (Playing)
        {
            timer += Time.deltaTime;
        }


        //Debug.Log(Beats.Count);
    }

    private void OnApplicationFocus(bool focus)
    {
        // RESYNC SONG
        if (Playing)
        {
            //Debug.Log("Refocused!");
            //Vox.time = timer;
            //Instruments.time = timer;
        }
    }

    void StartChart(string filepath)
    {
        // CLEAR ALL PREVIOUS NOTES IF ANY
        StopAllCoroutines();
        Beats.Clear();
        NotesQueue.Clear();

        // LOAD CHART, STOP FUNCTION "return;" IF NO CHART FOUND;
        chart = Load(FilePath);
        if (chart == null) { Debug.LogError("Could not play chart"); return; }

        // PLAY SOURCES
        if (Vox) Vox.PlayScheduled(AudioSettings.dspTime + (Offset / Player.NotesSpeed));
        if (Instruments) Instruments.PlayScheduled(AudioSettings.dspTime + (Offset / Player.NotesSpeed));
        Playing = true;

        // QUEUE UP ALL NOTES AND ADD THEM TO A LIST
        for (int i = 0; i < chart.Notes.Count; i++)
        {
            if (chart.Notes[i] != null)
            {
                NotesQueue.Add(StartCoroutine(SpawnNotesInTime(chart.Notes[i])));
            }
        }

        // QUEUE UP ALL BEAT CALLS SO CHARACTERS CAN BOP TO THE BEAT
        // ONLY ONE CHART SHOULD DO THOSE
        if (SendBeatCalls)
        {
            CurrentOffset = Offset;
            CurrentNotesSpeed = Player.NotesSpeed;
            ChartStart();

            for (float i = 0; i < Vox.clip.length; i += (chart.BPS * BeatsModifier) * 0.25f)
            { Beats.Add(StartCoroutine(BeatBeatQuater(i))); }

            for (float i = 0; i < Vox.clip.length; i += (chart.BPS * BeatsModifier) * 0.5f)
            { Beats.Add(StartCoroutine(BeatBeatHalf(i))); }

            for (float i = 0; i < Vox.clip.length; i += chart.BPS * BeatsModifier)
            { Beats.Add(StartCoroutine(BeatBeat(i))); }

            for (float i = 0; i < Vox.clip.length; i += (chart.BPS * BeatsModifier) * 2)
            { Beats.Add(StartCoroutine(BeatBeatDouble(i))); }

            for (float i = 0; i < Vox.clip.length; i += (chart.BPS * BeatsModifier) * 4)
            { Beats.Add(StartCoroutine(BeatBeatQuadruple(i))); }
        }
    }

    IEnumerator SpawnNotesInTime(Note note)
    {
        yield return new WaitForSeconds(GetOffset(note.SpawnTime));
        Player.CreateFallingArrow(note.Dir, note.Type, note.TrailLenght);
    }

    IEnumerator BeatBeatQuater(float time)
    {
        yield return new WaitForSeconds(time + (Offset / Player.NotesSpeed));
        if (BeatQuarter != null) BeatQuarter();
    }

    IEnumerator BeatBeatHalf(float time)
    {
        yield return new WaitForSeconds(time + (Offset / Player.NotesSpeed));
        if (BeatHalf != null) BeatHalf();
    }

    IEnumerator BeatBeat(float time)
    {
        yield return new WaitForSeconds(time + (Offset / Player.NotesSpeed));
        if (Beat != null) Beat();
    }

    IEnumerator BeatBeatDouble(float time)
    {
        yield return new WaitForSeconds(time + (Offset / Player.NotesSpeed));
        if (BeatDouble != null) BeatDouble();
    }

    IEnumerator BeatBeatQuadruple(float time)
    {
        yield return new WaitForSeconds(time + (Offset / Player.NotesSpeed));
        if (BeatQuadruple != null) BeatQuadruple();
    }

    float GetOffset(float time)
    {
        return time/* - (Offset * Player.NotesSpeed)*/;
    }

    // SAVE AND LOAD

    public static void Save(Chart chart, string filepath)
    {
        string f = Application.dataPath + "/" + filepath + ".chart";

        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log("Saved To: " + f);
        FileStream file = File.Open(f, FileMode.OpenOrCreate);
        bf.Serialize(file, chart);
        file.Close();
    }

    public static Chart Load(string filepath)
    {
        string f = Application.dataPath + "/" + filepath + ".chart";

        if (File.Exists(f))
        {
            BinaryFormatter bf = new BinaryFormatter();
            Debug.Log("Loaded From: " + f);
            FileStream file = File.Open(f, FileMode.Open);
            return (Chart)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.LogError("No chart file found! " + f);
            return null;
        }
    }

    // EXTRA FUNCTIONS

    public static Note NewNote(float t, Direction dir, NoteType type, float mod)
    {
        Note n = new Note();
        n.SpawnTime = t;
        n.Dir = dir;
        n.Type = type;
        n.TrailLenght = mod;
        return n;
    }

    int InFrames(float t) { return (int)(t * 60); }

    // BEAT CALL
    public delegate void BeatAction();
    public static event BeatAction Beat;
    public delegate void BeatAction1();
    public static event BeatAction1 BeatQuarter;
    public delegate void BeatAction2();
    public static event BeatAction2 BeatHalf;
    public delegate void BeatAction4();
    public static event BeatAction4 BeatDouble;
    public delegate void BeatAction5();
    public static event BeatAction5 BeatQuadruple;

    // CHART STARTED CALL
    public delegate void ChartStarted();
    public static event ChartStarted ChartStart;


}


// SERIALIZABLE STUFF

[Serializable]
public class Note
{
    public float SpawnTime = 1;
    public Direction Dir = Direction.Right;
    public NoteType Type = NoteType.Normal;
    public float TrailLenght = 0.1f;
}

[Serializable]
public class Chart
{
    public float BPS = 0.6f;
    public float Subdivisions = 16;
    public List<Note> Notes = new List<Note>(10);
}

