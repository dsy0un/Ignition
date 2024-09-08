using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonManager : MonoBehaviour
{
    void Start()
    {
        // JSON 파일에서 대화 리스트 가져오기

        string path = Application.dataPath + "/Json/NPC/dialogues.json";
        string json = File.ReadAllText(path);
        dialogueList = JsonUtility.FromJson<DialogueList>(json);
    }
