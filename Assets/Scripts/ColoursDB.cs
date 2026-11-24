using System;
using System.Collections.Generic;
using UnityEngine;

/// <remarks>
/// Accessed with <b>starting index 1</b>.
/// Access colours using positive-negative pairs: e.g. colour 1 is opposite to colour -1.
/// </remarks>
[CreateAssetMenu(fileName = "ColoursDB", menuName = "Scriptable Objects/ColoursDB")]
public class ColoursDB : ScriptableObject
{
    [Serializable]
    public struct ColourPair
    {
        public Color colour;
        public Color opposite;
        public Color? this[int index]
        {
            get
            {
                switch(index)
                {
                    case 1: return colour;
                    case -1: return opposite;
                    default:
                    #if UNITY_EDITOR
                        Debug.LogError("Invalid index at ColourPair access!");
                    #endif
                        return null;
                }
            }
        }
    }
    [SerializeField] private List<ColourPair> colours;
    public Color? this[int index] 
    {
        get
        {
            try
            {
                return colours[Mathf.Abs(index) - 1][(int)Mathf.Sign(index)];
            } 
            catch
            {
            #if UNITY_EDITOR
                Debug.LogError("Out of range exception at ColourDB access!");
            #endif
                return null;
            }
        }
    }
}
