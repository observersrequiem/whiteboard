using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
// Locking in.
public class DialogueSystem : MonoBehaviour
{
     [SerializeField]
    Dictionary<string, int> animkeys = new Dictionary<string, int> (){
        { "none", 0 },
        { "defo", 1 },
        { "tagged", 2 }
    };

    public TextDeployerBasic tdb;
    public Animator anim;
    public VarMan varman;
    public SFXDiaManager sfx;
    public DSButtonHandler dsbh;
    public List<Dialogue> dialogues = new();
    public bool CanNextFrame = true;
    [SerializeField] Button move;
    [SerializeField] private int currentIndex;

    public void PushDialogueBasic() 
    {
        // if the bug isnt fixed add a check here :p
        anim.SetInteger("AnimID",animkeys[dialogues[currentIndex].AnimKey]);
        tdb.StartTyping(dialogues[currentIndex].TextKey);tdb.SetName(dialogues[currentIndex].NameKey);
    }

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
        PushDialogue(currentIndex);
    }

    void EndSequence() {
        Debug.Log("SEQUENCE OVER - SEQUENCE OVER - SEQUENCE OVER");
        dialogues.Clear();
        tdb.StartTyping("");
        tdb.SetName("");
        anim.SetInteger("AnimID", 0); // add the UI disabling here (lol)
    }

    public void NextFrame() // U have to couple this with some inputmanager thingy
    {
        if (CanNextFrame)
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
