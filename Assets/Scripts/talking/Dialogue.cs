using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string level;
    // "thanh:im saying something"
    public string[] sentences;

    #region character gray, cinnamon, broken sword, story, the riverkeeper
    public string[] names = new string[] { "G", "C", "B", "S", "R", "dev" };
    #endregion character

    public string[] getCharName()
    {
        return names;
    }

    public Dialogue()
    {
    }

    public Dialogue(string level, string[] sentences)
    {
        this.level = level;
        this.sentences = sentences;
    }

}
