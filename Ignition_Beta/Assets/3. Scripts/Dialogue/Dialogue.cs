using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Text/Dialogue", order = int.MaxValue)]
public class Dialogue : ScriptableObject
{
    [SerializeField, Tooltip("여기에 글 쓰면 됨")]
    string[] sentences;
    public string[] Sentences { get { return sentences; } }
}
