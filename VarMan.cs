using UnityEngine;
using System.Collections.Generic;

public class VarMan : MonoBehaviour
{
    // manages variables
    [SerializeField]
    Dictionary<string,object> kvp = new() 
    {
        {"defaultbool", false},
        {"defaultval", 0},
        {"debugvar", 333}
    };

    public object FindVariableValue(string varname) {
        kvp.TryGetValue(varname, out var value);
        return value;
    }
    public void SetVariable(string varname,object value) {
        object x = kvp[varname]; if (x != null) {kvp[varname] = value;}
    }
}
