using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rich_Room_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.pair.phaseNum = 1;
        GameManager.instance.place = "ROOM";
        GameManager.instance.pair.choiceNum = 1;
        DialogueManager.instance.ShowDialogue();
    }


}
