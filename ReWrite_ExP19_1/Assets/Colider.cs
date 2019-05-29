using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Colider : MonoBehaviour
{

    public List<Transform> list;
    public Transform playerTr;

    // Start is called before the first frame update
    void Start()
    {
        var group = GameObject.Find("InteractionPoints");
        if(group != null)
        {
            group.GetComponentsInChildren<Transform>(list);
            list.RemoveAt(0);
        }

        playerTr = GameObject.Find("GameObjectPlayer").GetComponent<Transform>(); 
    }


    public void Interaction()
    {
        Debug.Log("클릭됨");
        // 상호작용 눌렸다면



        for (int i = 0; i < list.Count; i++)
        {

            if (playerTr.position.x < list[i].position.x + 1f && playerTr.position.x > list[i].position.x - 1f)
            {
                if (playerTr.position.y < list[i].position.y + 1f && playerTr.position.y > list[i].position.y - 1f)
                {

                    Debug.Log(i);
                    // 현재 씬이 ROOM 일때
                    if (SceneManager.GetActiveScene().name == "Room")
                    {
                        if(RichRoomCollider.instance.NowState() == "toRoom")
                        {
                            
                            // 전화기와 상호작용시
                            DialogueManager.instance.ShowDialogue("OBJ/R2-ROOM1OBJ");
                        }
                        else if(RichRoomCollider.instance.NowState() == "toDoor")
                        {
                            DialogueManager.instance.ShowDialogue("OBJ/R1-ROOM2OBJ");
                        }
                        else if(RichRoomCollider.instance.NowState() == "toTelephone")
                        {
                            // 전화기와 상호작용 했을 때
                            if(i == 10 || i == 9 || i == 1)
                            {
                                RichRoomCollider.instance.state = RichRoomCollider.State.toBed;
                                DialogueManager.instance.ShowDialogue("R2-ROOM1");
                            }
                            else
                                DialogueManager.instance.ShowDialogue("OBJ/R2-LOBBY2OBJ");
                        }
                        else if(RichRoomCollider.instance.NowState() == "toBed")
                        {
                            // 침대와 상호작용
                            if( i == 2 || i == 3 || i == 4 || i == 6 || i == 7)
                            {
                                StartCoroutine(FadeInAndOut("out"));
                            }
                            else
                                // 침대로 가라는 상호작용
                                DialogueManager.instance.ShowDialogue("OBJ/R2-ROOM1OBJ");
                        }
                        else if(RichRoomCollider.instance.NowState() == "toDoctor")
                        {
                            // 의사가 왔으니 문으로 나가라는 상호작용
                            DialogueManager.instance.ShowDialogue("OBJ/R2-ROOM2OBJ");
                        }
                        else if(RichRoomCollider.instance.NowState() == "toGoToLobbyDoor")
                        {
                            // 누군가 왔으니 로비 문으로 나가보라는 상호작용
                            DialogueManager.instance.ShowDialogue("OBJ/R2-ROOM3OBJ");
                        }
                        else
                            DialogueManager.instance.ShowDialogue("OBJ/R1-ROOM1OBJ");
                        break;
                    }
                    else if (SceneManager.GetActiveScene().name == "Lobby")
                    {
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<RichRoomCollider>().NowState() == "toRoom")
                        {
                            DialogueManager.instance.ShowDialogue("OBJ/R2-LOBBY2OBJ");
                            break;
                        }
                        else if (RichRoomCollider.instance.NowState() == "toDoctor")
                        {
                            // 의사가 왔으니 문으로 나가라는 상호작용
                            DialogueManager.instance.ShowDialogue("OBJ/R2-ROOM2OBJ");
                        }
                        else if (RichRoomCollider.instance.NowState() == "toTakeDoctor")
                        {
                            // 의사를 방으로 안내하라는 상호작용
                            DialogueManager.instance.ShowDialogue("OBJ/R2-LOBBY3OBJ");
                        }
                        else if (RichRoomCollider.instance.NowState() == "toGoToLobbyDoor")
                        {
                            // 누군가 왔으니 로비 문으로 나가보라는 상호작용
                            DialogueManager.instance.ShowDialogue("OBJ/R2-ROOM3OBJ");
                        }
                        else 
                            DialogueManager.instance.ShowDialogue("OBJ/R1-ROOM2OBJ");
                        break;
                    }
                   



                }
            }
        }
    }

    public IEnumerator FadeInAndOut(string str)
    {
        Image image = GameObject.Find("Fader").GetComponent<Image>();
        Color tmp = image.color;
        if (str == "in")
        {
            image.enabled = true;
            
            tmp.a = 1f;
            while (tmp.a >= 0f)
            {
                yield return new WaitForSeconds(0.05f);
                tmp.a -= 0.02f;
                image.color = tmp;

            }
            image.enabled = false;
        }
        else if(str == "out")
        {
            image.color = new Color(0f, 0f, 0f, 0f);
            image.enabled = true;
            tmp.a = 0f;
            while (tmp.a <= 1f)
            {
                yield return new WaitForSeconds(0.05f);
                tmp.a += 0.02f;
                image.color = tmp;

            }
            
        }
        if (RichRoomCollider.instance.NowState() == "toBed")
        {
            RichRoomCollider.instance.state = RichRoomCollider.State.toDoctor;
            yield return new WaitForSeconds(1f);
            DialogueManager.instance.ShowDialogue("R2-ROOM2");
            // 대화창이 보이지 않을 우려 있음. - > 대화창의 캔버스의 소팅오더를 1로 설정.
            while (DialogueManager.instance.isSpeaking == true)
            {
                yield return new WaitForSeconds(0.02f);
            }
            StartCoroutine(FadeInAndOut("in"));
        }
        else if(RichRoomCollider.instance.NowState() == "toTakeDoctorToRoom"){
            yield return new WaitForSeconds(1f);
            DialogueManager.instance.ShowDialogue("R2-ROOM3");
            while (DialogueManager.instance.isSpeaking == true)
            {
                yield return new WaitForSeconds(0.02f);
            }
            // 룸 씬으로 이동
            SceneManager.LoadScene("Room");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

       
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
