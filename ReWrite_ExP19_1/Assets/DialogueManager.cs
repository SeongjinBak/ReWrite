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
            //Debug.Log("?");
            instance = this;
        }
        // 아래의 경우, 클래스가 새로 생성되었을 경우를 의미함
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    #endregion Singleton

    private JsonParser dialogue;
    // 현재 대화 스크립드 중 몇번째 스크립트를 가리키는가? 를 나타낼 변수.
    public int count;
    // 화면에 출력할 Text

    public Text txt;
    // 화면에 출력할 화자 이름
    public new Text name;
    // 선택지 출력할
    public Text choice_txt1, choice_txt2;
    // 선택지 클릭용 버튼
    public Button choice_btn1, choice_btn2;
    // 왼쪽, 오른쪽 이미지
    public Image leftImage, rightImage;
    // 대화창 window
    public Image dialogueWindow;
    public Image selectionWindow;
    [SerializeField]
    private int btn;
    [SerializeField]
    // 현재 터치 입력 받았는지
    private bool isTouched;
    [SerializeField]
    // 현재 대화중인지
    private bool isSpeaking;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        btn = -1;
        txt.text = "";
        choice_txt2.text = "";
        choice_txt1.text = "";
        name.text = "";
        choice_btn1.enabled = false;
        choice_btn2.enabled = false;
        leftImage.enabled = false;
        rightImage.enabled = false;
        dialogueWindow.enabled = false;
        selectionWindow.enabled = false;
        isTouched = false;
        isSpeaking = false;
        // 로드했다면 dialogue에 정보가 저장되어있음. 이것을 순차적으로 화면에 출력해주면 됨.
       // OutputDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogue == null) return;
        if(dialogue.isDialogue == "dialogue")
        {
            // 현재 터치가 입력받는중이 아니며 터치 되었고, 혹은 마우스입력을 받았을 경우
            if (isSpeaking == true && isTouched == false && (Input.touchCount > 0 || Input.GetKey(KeyCode.Z) || Input.GetMouseButton(0)))
            {
                isTouched = true;
                Debug.Log("clicked");

                // 현재 화면에 나와있는 텍스트를 없앰
                txt.text = "";
                // 현재 출력할 대화의 넘버를 올리고, 대화출력 시작.

                if (count >= dialogue.length)
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
        else
        {
            if(btn != -1)
            {
                if (count == dialogue.length)
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
    }

    public void ShowDialogue()
    {
        // 대화창을 GameManager에 저장되어 있는 캐릭터 이름, 현재 분기 정보들에 따라 로드 한다.
        LoadDialogue();
        // 대화 시작
        StartCoroutine(StartDialogue());
    }

    public IEnumerator StartDialogue()
    {
        // 선택지라면

        // 대화중임을 설정
        isSpeaking = true;
       
        if(dialogue.isDialogue == "dialogue")
        {
            // 대화창 이미지 활성
            dialogueWindow.enabled = true;
            // 좌측, 우측 이미지
            if (dialogue.direction[count] == "L")
            {
                leftImage.enabled = true;
                rightImage.enabled = false;
                leftImage.sprite = Resources.Load<Sprite>("UI_Images/" + dialogue.name[count] + "_finale");

            }
            else if (dialogue.direction[count] == "R")
            {
                leftImage.enabled = false;
                rightImage.enabled = true;
                rightImage.sprite = Resources.Load<Sprite>("UI_Images/" + dialogue.name[count] + "_finale");
            }
            else
            {
                leftImage.enabled = false;
                rightImage.enabled = false;
            }
            // 대화창에 이름을 적는다,
            name.text = WriteName();
        }
        else
        {
            selectionWindow.enabled = true;
            choice_btn1.enabled = true;
            choice_btn1.image.enabled = true;
            choice_btn2.enabled = true;
            choice_btn2.image.enabled = true;
        }
       
        
        
        if(dialogue.isDialogue == "dialogue")
        {
            for (int i = 0; i < dialogue.dialogues[count].Length; i++)
            {
                txt.text += dialogue.dialogues[count][i];
                yield return new WaitForSeconds(0.02f);
            }
        }
        else
        {
            for (int i = 0; i < dialogue.dialogues[count].Length; i++)
            {
                choice_txt1.text += dialogue.dialogues[count][i];
                //choice_txt2.text += dialogue.dialogues[count + 1][i];
                yield return new WaitForSeconds(0.001f);
            }
            for (int i = 0; i < dialogue.dialogues[count+1].Length; i++)
            {
             
                choice_txt2.text += dialogue.dialogues[count + 1][i];
                yield return new WaitForSeconds(0.001f);
            }
            count++;
        }
        count++;
        // 터치가 입력되었다고 한다면, 대화창 출력이 끝난 다음 터치가 입력되지 않았다고 알려줌.
        if (isTouched == true)
        {
            isTouched = false;
        }
    }

    public string WriteName()
    {
        // 대화창에 이름을 적는다,
        if (dialogue.name[count] == "RICH")
            return "부자";
        else if (dialogue.name[count] == "FARMER")
            return "농부";
        else
        {
            return "";
        }
    }

    public void EndDialogue()
    {
        txt.text = "";
        btn = -1;
        choice_btn2.enabled = false;
        choice_btn1.image.enabled = false;
        choice_btn2.image.enabled = false;
        choice_btn1.enabled = false;
        choice_txt1.text = "";
        choice_txt2.text = "";
        name.text = "";
        count = 0;
        // 대화창 이미지 비활성
        dialogueWindow.enabled = false;
        selectionWindow.enabled = false;
        leftImage.enabled = false;
        rightImage.enabled = false;
        isSpeaking = false;
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

         string fileName = GameManager.instance.playerName + GameManager.instance.pair.phaseNum.ToString() + "-" + GameManager.instance.place + GameManager.instance.pair.choiceNum;
        //string fileName = "R1-ROOM1";
        //Debug.Log(fileName);
        string jsonFile = Resources.Load<TextAsset>("Json/" + fileName).ToString();
       //Debug.Log(jsonFile);
        dialogue = JsonUtility.FromJson<JsonParser>(jsonFile);
        //Debug.Log(dialogue.length);
        dialogue.DialogueParser();
    }


    public void btn2()
    {
        btn = 2;
    }
    public void btn1()
    {
        btn = 1;
    }
    
}
