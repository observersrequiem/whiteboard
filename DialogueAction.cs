using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueAction
{
    //Does things
    public enum DialogueActionType
    {
        None,        
        SetVar,    
        DialogueBranch  
    }
    [SerializeField]
    public DialogueActionType type;
    [SerializeField]
    public List<VarChange> vc = new();

    
    [SerializeField]
    public List<BranchDialogue> dbs = new();

    public string sfxkey;
    public string bgmkey;

}