using UnityEngine;
using System.Collections.Generic;

public class VarMan : MonoBehaviour
{
    // manages variables
    [SerializeField]
    Dictionary<string,object> kvp = new() // This is the savefile one that gets loaded in and shit
    {
        {"defaultbool", false},
        {"defaultval", 0},
        {"debugvar", 333},
        {"health", 100},
        {"selsys", true}
    };

    private void Start() {
        SetVariable("defaultbool", true);
    }

    public object FindVariableValue(string varname) {
        kvp.TryGetValue(varname, out var value);
        return value;
    }
    public void SetVariable(string varname,object value) {
        object x = kvp[varname];
        if (x != null) 
        {kvp[varname] = value; 
        Debug.Log("set "+varname+" to "+value+" successfully.");}
        else {Debug.Log("Failed!");}
    }

    public void SetVarChange(List<VarChange> varchange) {
        varchange.ForEach(VarChange => SetVariable(VarChange.key, VarChange.value));
    }
}
