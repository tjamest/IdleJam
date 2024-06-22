using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Resources", order = 1)]
public class ResourceScriptableObject : ScriptableObject
{
    public ResourceType type;
    public float amount;

    public bool automated;
    public float cycleDuration = 60;
    public float increase;
    public float multiplier;
}

public enum ResourceType
{
    Money,
    Other
}