using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using NUnit.Framework.Internal;
using Unity.Mathematics;
// Locking in.
public class DialogueSystem : MonoBehaviour
{
     [SerializeField]
    Dictionary<string, int> animkeys = new Dictionary<string, int> (){
        { "none", 0 },
        { "defo", 1 },
        { "tagged", 2 },
    };

    public TextDeployerBasic tdb;
    public Animator anim;
    public VarMan varman;
    public SFXDiaManager sfx;
    public List<Dialogue> dialogues = new();

    private int currentIndex;

    public void PushDialogueBasic() 
    {
        // if the bug isnt fixed add a check here :p
        anim.SetInteger("AnimID",animkeys[dialogues[currentIndex].AnimKey]);
        tdb.StartTyping(dialogues[currentIndex].TextKey);tdb.SetName(dialogues[currentIndex].NameKey);
    }

    public void PushDialogue(Dialogue d)
    {
        if (currentIndex < dialogues.Count) 
        {
            currentIndex++;
            anim.SetInteger("AnimID", animkeys[d.AnimKey]); //animation first of all
            switch (d.Action.type)
            {
                case DialogueAction.DialogueActionType.None:
                    tdb.StartTyping(d.TextKey);tdb.SetName(d.NameKey);
                    break;
                case DialogueAction.DialogueActionType.SetVar:
                    varman.SetVarChange(d.Action.vc);
                    tdb.StartTyping(d.TextKey);tdb.SetName(d.NameKey);
                    break;
                case DialogueAction.DialogueActionType.DialogueBranch:
                    tdb.StartTyping(d.TextKey);tdb.SetName(d.NameKey);
                    //buttons buttons buttons lets handle the button system
                    break;
            }
            if (d.Action.sfxkey != "") 
            {sfx.PlaySfx(d.Action.sfxkey); }

        } else {EndSequence();}
    } 

    public void BasicSequence(List<Dialogue> chatter) {
        dialogues.Clear();
        currentIndex = 0;
        dialogues = chatter;
        PushDialogueBasic();
    } 

    public void ComplexSequence(List<Dialogue> chatter) {
        dialogues.Clear();
        currentIndex = 0;
        dialogues = chatter;
        PushDialogue(dialogues[currentIndex]);
    }

    void EndSequence() {
        Debug.Log("SEQUENCE OVER - SEQUENCE OVER - SEQUENCE OVER");
        dialogues.Clear();
        tdb.StartTyping("");
        tdb.SetName("");
        anim.SetInteger("AnimID", 0); // add the UI disabling here (lol)
    }

    public void NextFrame()
    {
        if (!tdb.isTyping){
            PushDialogue(dialogues[currentIndex]);
        } else {tdb.Skip();}
    }

}
