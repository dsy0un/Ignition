using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = int.MaxValue)]
public class Dialogue : ScriptableObject
{
    [SerializeField]
    string[] sentences;
    public string[] Sentences { get { return sentences; } }
}
