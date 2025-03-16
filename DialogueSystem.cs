using UnityEngine;
using System.Collections.Generic;
using System.Linq;
// Locking in.
public class DialogueSystem : MonoBehaviour
{
     [SerializeField]
    Dictionary<string, int> animkeys = new Dictionary<string, int> (){
        { "none", 0 },
        { "defo", 1 }
    };

    public TextDeployerBasic tdb;
    public bool dialogueSystemInUse;
    public Animator anim;
    public VarMan varman;
    public SFXDiaManager sfx;
    public DSButtonHandler dsbh;
    public List<Dialogue> dialogues = new();
    public bool CanNextFrame = true;
    [SerializeField] private int currentIndex;

    public void PushDialogue(int curint)
    {
        Debug.Log("We are at "+currentIndex+" and we have a total of "+dialogues.Count);
        Dialogue d = dialogues.ElementAtOrDefault(curint);
        if (d != null)
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
                    dsbh.PushButtons(d.Action.dbs);
                    CanNextFrame = false;
                    break;
            }
            if (d.Action.sfxkey != "") 
            {sfx.PlaySfx(d.Action.sfxkey);}
            if (d.Action.bgmkey != "")
            {sfx.PlayBGM(d.Action.bgmkey);}
        } else
        {
            EndSequence();
        }
    } 

    public void ComplexSequence(List<Dialogue> chatter) {
        dialogueSystemInUse = true;
        dialogues.Clear();
        currentIndex = 0;
        dialogues = chatter;
        PushDialogue(currentIndex);
    }

    void EndSequence() {
        Debug.Log("SEQUENCE OVER - SEQUENCE OVER - SEQUENCE OVER");
        dialogues.Clear();
        tdb.StartTyping("");
        tdb.SetName("empty");
        anim.SetInteger("AnimID", 0); // add the UI disabling here (lol)
        dialogueSystemInUse = false;
    }

    public void NextFrame() // U have to couple this with some inputmanager thingy
    {
        if (CanNextFrame && dialogueSystemInUse)
        {
            if (!tdb.isTyping){
            PushDialogue(currentIndex);
            } else {tdb.Skip();}
        } else {Debug.Log("Dumbass");}
    }

    public void ButtonRecall(List<Dialogue> d) 
    {
        CanNextFrame = true;
        ComplexSequence(d);
    }

}
