using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ColorUtility = UnityEngine.ColorUtility;

namespace Alf.UI
{
public static class Colors
{
    
    public static readonly Dictionary<string, string> Values = new Dictionary<string, string> {
        {"PRIMARY",   "#ced7e8"},  // Off-white
        {"SECONDARY", "#371f2f"},  // Dark Purple
        {"HIGHLIGHT", "#d88f8f"},  // Pink
    };

    public static Color Value(string colorName)
    {
        if(!Values.ContainsKey(colorName))
            return Color.white;
        
        Color newCol;
        if (ColorUtility.TryParseHtmlString(Values[colorName], out newCol))
            return newCol;
        
        return Color.white;
    }

}
}