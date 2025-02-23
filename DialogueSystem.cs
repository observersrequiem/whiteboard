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
        
    }

    public void BasicSequence(List<Dialogue> chatter) {
        dialogues.Clear();
        currentIndex = 0;
        dialogues = chatter;
        PushDialogueBasic();
    } // No translation, no buttons, straight PUSH

    public void ComplexSequence(List<Dialogue> chatter) {
        dialogues.Clear();
        currentIndex = 0;
    }

    void EndSequence() {
        dialogues.Clear();
        tdb.StartTyping("");
        tdb.SetName("");
        anim.SetInteger("AnimID", 0); // add the UI disabling here (lol)
    }

    public void NextFrame()
    {
        if (currentIndex > dialogues.Count && !tdb.isTyping) {
            Debug.Log("Sequence over");
            EndSequence();
        } else {
            if(currentIndex <= dialogues.Count && !tdb.isTyping) {
                Debug.Log("One frame ahead");
                currentIndex += 1;
                PushDialogueBasic(); //add relevant information!
            } else {tdb.Skip(); Debug.Log("Skipped");}
        }
    }

}
