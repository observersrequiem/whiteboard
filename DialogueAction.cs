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
        ShowButtons  
    }
    [SerializeField]
    public DialogueActionType type;
    public string key;
    public object value;
    public string[] ButtonLabels;   // For ShowButtons, add stuff later
}