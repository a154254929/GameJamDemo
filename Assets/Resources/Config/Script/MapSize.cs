using System;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class MapSize : ScriptableObject
{
    public int Column;
    public int Row;
    public int Layer;
}
