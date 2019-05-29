using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RichRoomCollider : MonoBehaviour
{
    #region Singleton
    // 싱글턴에 접근하기 위한 Static 변수 선언
    public static RichRoomCollider instance = null;
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
    [SerializeField]
    public State state;
    Transform player; 
    public enum State
    {
        toBalcony, toDoor, toLobbyDoor, toRoom, toTelephone, toBed, toDoctor, toTakeDoctor, toTakeDoctorToRoom, toGoToLobbyDoor, PhaseEnd
    }
    // Start is called before the first frame update
    void Start()
    {
        state = State.toBalcony;
        if(SceneManager.GetActiveScene().name == "Lobby")
        {
            state = State.toLobbyDoor;
        }
        player =  GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.toBalcony)
        {
            if (collision.name == "Balcony")
            {
                state = State.toDoor;
                DialogueManager.instance.ShowDialogue("R1-ROOM2");
            }
        }
        else if(state == State.toDoor)
        {
            if (collision.name == "Door")
            {
                state = State.toLobbyDoor;
                Vector2 pos = new Vector2(-0.4f, -2.62f);
                instance.transform.localPosition = pos;
                SceneManager.LoadScene("Lobby");
            }
        }
        else if(state == State.toLobbyDoor && collision.name == "Lobby_Door")
        {
            
             DialogueManager.instance.ShowDialogue("R1-LOBBY1");
            StartCoroutine(WaitTillSpeakingEnd());
            state = State.toRoom;
        }
        else if(state == State.toRoom && SceneManager.GetActiveScene().name == "Lobby" && collision.name == "DoorCollider (1)")
        {

            state = State.toTelephone;
            Vector2 pos = new Vector2(1.95f, 6.86f);
            instance.transform.localPosition = pos;

            SceneManager.LoadScene("Room");
        }
        else if(SceneManager.GetActiveScene().name == "Room" && state == State.toDoctor && collision.name == "Door")
        {
            // 부자의 위치를 로비의 입구쪽으로 옮겨야 함.
            Vector2 tmp = new Vector2(-0.4f, -2.62f);
            RichRoomCollider.instance.transform.position = tmp;
            SceneManager.LoadScene("Lobby");

        }
        else if(SceneManager.GetActiveScene().name == "Lobby" && state == State.toDoctor && collision.name == "DoorCollider")
        {
            state = State.toTakeDoctor;
            DialogueManager.instance.ShowDialogue("R2-LOBBY3");
        }
        else if(state == State.toTakeDoctor && collision.name == "DoorCollider (1)")
        {
            state = State.toTakeDoctorToRoom;
            Colider col = GameObject.Find("InteractionPoints").GetComponent<Colider>();
            StartCoroutine(col.FadeInAndOut("out"));
        }
        else if(state == State.toGoToLobbyDoor && collision.name == "DoorCollider")
        {
            state = State.PhaseEnd;
            DialogueManager.instance.ShowDialogue("R2-LOBBY4");
        }
        else if (state == State.toGoToLobbyDoor && collision.name == "Door")
        {
            // 부자의 위치를 로비의 입구쪽으로 옮겨야 함.
            Vector2 tmp = new Vector2(-0.4f, -2.62f);
            RichRoomCollider.instance.transform.position = tmp;
            SceneManager.LoadScene("Lobby");
        }
        // 여기에 아무 컨디션 상관 없을때 (조건에 걸리지 않았을 때) 방 이라면 로비로, 로비라면 방으로 이동할 수 있게끔 해줘야 함.
        /*
        else {
            if (SceneManager.GetActiveScene().name == "Room")
                SceneManager.LoadScene("Lobby");
            else
            {
                SceneManager.LoadScene("Room");
            }
        }
        */


    }


    IEnumerator WaitTillSpeakingEnd()
    {
        while(DialogueManager.instance.isSpeaking == true){
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.1f);
        DialogueManager.instance.ShowDialogue("RICH-CHOICE");
        while (DialogueManager.instance.isSpeaking == true)
        {
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.1f);
        DialogueManager.instance.ShowDialogue("R2-LOBBY1");

        Vector2 tmp = player.position;
        // 5발자국 걸어갈 때 까지 기다림.
        while (true)
        {

            if (Vector2.Distance(tmp, player.localPosition ) > 3f)
            {
                break;
            }
            yield return new WaitForSeconds(0.02f);
        }
        DialogueManager.instance.ShowDialogue("R2-LOBBY2");


    }


   public string NowState()
    {
        return state.ToString();
    }
    
}
