using System;
using System.Collections.Generic;

[Serializable]
public class LevelsData
{
    /// <summary>
    /// int  - Number of level
    /// bool - State of level (Completed or not)
    /// </summary>
    public Dictionary<int, bool> Levels;
}
