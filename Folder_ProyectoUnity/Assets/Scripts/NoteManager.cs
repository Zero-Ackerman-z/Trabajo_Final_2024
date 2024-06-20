using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    private SimplyLinkedList<Nota> noteList = new SimplyLinkedList<Nota>();

    public void AddNoteAtEnd(float time, int type)
    {
       // Note newNote = new Nota(time, type);
        //noteList.InsertNodeAtEnd(newNote);
    }

    public void AddNoteAtPosition(float time, int type, int position)
    {
        //Note newNote = new Nota(time, type);
        //noteList.InsertNodeAtPosition(newNote, position);
    }

    public void DeleteNoteAtPosition(int position)
    {
        noteList.DeleteNodeAtPosition(position);
    }

    public void DeleteNoteByValue(Nota note)
    {
       // noteList.DeleteNode(nota);
    }

    public void PrintAllNotes()
    {
        for (int i = 0; i < noteList.GetLength(); i++)
        {
          //  Note note = noteList.GetNodeAtPosition(i);
          //  Debug.Log("Time: " + note.time + ", Type: " + note.type);
        }
    }
}

