using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private SimplyLinkedList<Note> noteList = new SimplyLinkedList<Note>();

    public void AddNoteAtEnd(float time, int type)
    {
        Note newNote = new Note(time, type);
        noteList.InsertNodeAtEnd(newNote);
    }

    public void AddNoteAtPosition(float time, int type, int position)
    {
        Note newNote = new Note(time, type);
        noteList.InsertNodeAtPosition(newNote, position);
    }

    public void DeleteNoteAtPosition(int position)
    {
        noteList.DeleteNodeAtPosition(position);
    }

    public void DeleteNoteByValue(Note note)
    {
        noteList.DeleteNode(note);
    }

    public void PrintAllNotes()
    {
        for (int i = 0; i < noteList.GetLength(); i++)
        {
            Note note = noteList.GetNodeAtPosition(i);
            Debug.Log("Time: " + note.time + ", Type: " + note.type);
        }
    }
}

