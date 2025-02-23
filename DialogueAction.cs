using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueAction
{
    //Does things
    public enum DialogueActionType
    {
        None,        
        SetFlag,    
        ChangeValue, 
        ShowButtons  
    }
    [SerializeField]
    public DialogueActionType type;
    public string FlagName;         // For SetFlag
    public bool FlagValue;          
    public string ValueName;        // For ChangeValue
    public int ValueToAdd;
    public string[] ButtonLabels;   // For ShowButtons,

    
}