using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    private JsonParser dialogue;

    // Start is called before the first frame update
    void Start()
    {
        // 대화창을 GameManager에 저장되어 있는 캐릭터 이름, 현재 분기 정보들에 따라 로드 한다.
        LoadDialogue();
        // 로드했다면 dialogue에 정보가 저장되어있음. 이것을 순차적으로 화면에 출력해주면 됨.
        OutputDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OutputDialogue()
    {
        // 이게 대화창이라면, UI는 Dialogue Window로 출력해줘야 함
        if(dialogue.isDialogue == "dialogue")
        {
            for (int i = 0; i < dialogue.length; i++)
            {
                Debug.Log(dialogue.dialogues[i]);
             }
        }
        // 대화창이 아니고 선택지 창이라면, UI는 Choosable Window로 출력해야 함.
        else
        {
            Debug.Log("선택지 창 오픈");
        }
        
    }
    void LoadDialogue()
    {
        
        string fileName = GameManager.instance.playerName + GameManager.instance.pair.phaseNum.ToString() + "-" + GameManager.instance.pair.choiceNum.ToString() + GameManager.instance.type;
       // Debug.Log(fileName);
        string jsonFile = Resources.Load<TextAsset>("Json/" + fileName).ToString();
       // Debug.Log(jsonFile);
        dialogue = JsonUtility.FromJson<JsonParser>(jsonFile);
        //Debug.Log(dialogue.length);
        dialogue.DialogueParser();
    }


}
