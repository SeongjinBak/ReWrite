using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    // 싱글턴에 접근하기 위한 Static 변수 선언
    public static DialogueManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // 아래의 경우, 클래스가 새로 생성되었을 경우를 의미함
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton

    private JsonParser dialogue;
    // 현재 대화 스크립드 중 몇번째 스크립트를 가리키는가? 를 나타낼 변수.
    public int count;
    // 화면에 출력할 Text
    public Text txt;

    // 현재 터치 입력 받았는지
    private bool isTouched;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        txt.text = "";
        isTouched = false;

        // 로드했다면 dialogue에 정보가 저장되어있음. 이것을 순차적으로 화면에 출력해주면 됨.
       // OutputDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // 현재 터치가 입력받는중이 아니며 터치 되었고, 혹은 마우스입력을 받았을 경우
        if(isTouched == false && (Input.touchCount > 0 || Input.GetKey(KeyCode.Z)))
        {
            // 현재 화면에 나와있는 텍스트를 없앰
            txt.text = "";
            // 현재 출력할 대화의 넘버를 올리고, 대화출력 시작.
            
            if(count == dialogue.length )
            {
                StopAllCoroutines();
                EndDialogue();
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(StartDialogue());
            }
            
        }    
    }

    public void ShowDialogue()
    {
        // 대화창을 GameManager에 저장되어 있는 캐릭터 이름, 현재 분기 정보들에 따라 로드 한다.
        LoadDialogue();
        StartCoroutine(StartDialogue());
    }

    public IEnumerator StartDialogue()
    {
        
        for(int i=0;i <dialogue.dialogues[count].Length; i++)
        {
            txt.text += dialogue.dialogues[count][i];
            yield return new WaitForSeconds(0.02f);
        }
        count++;
        // 터치가 입력되었다고 한다면, 대화창 출력이 끝난 다음 터치가 입력되지 않았다고 알려줌.
        if (isTouched == true)
        {
            isTouched = false;
        }
    }

    public void EndDialogue()
    {
        txt.text = "";
        count = 0;
    }


    public void OutputDialogue()
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
    public void LoadDialogue()
    {
        
        string fileName = GameManager.instance.playerName + GameManager.instance.pair.phaseNum.ToString() + "-" + GameManager.instance.pair.choiceNum.ToString() + GameManager.instance.type;
       // Debug.Log(fileName);
        string jsonFile = Resources.Load<TextAsset>("Json/" + fileName).ToString();
       //Debug.Log(jsonFile);
        dialogue = JsonUtility.FromJson<JsonParser>(jsonFile);
        //Debug.Log(dialogue.length);
        dialogue.DialogueParser();
    }

    
}
