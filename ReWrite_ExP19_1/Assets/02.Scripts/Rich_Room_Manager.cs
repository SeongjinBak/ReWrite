using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rich_Room_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(RichRoomCollider.instance.NowState() == "toBalcony")
        {

            GameManager.instance.pair.phaseNum = 1;
            GameManager.instance.place = "ROOM";
            GameManager.instance.pair.choiceNum = 1;
            DialogueManager.instance.ShowDialogue();

        }
        else if(RichRoomCollider.instance.NowState() == "toTakeDoctorToRoom")
        {

            // 부자의 위치를 의자쪽으로 옮겨야 함.
            Vector2 tmp = new Vector2(-3.26f, 1.92f);
            RichRoomCollider.instance.transform.position = tmp;
            StartCoroutine(FadeIn());

        }
    }
    IEnumerator FadeIn()
    {
        Image image = GameObject.Find("Fader").GetComponent<Image>();
        image.color = Color.black;
        image.enabled = true;
        Color tmp = image.color;
        tmp.a = 1f;
        while (tmp.a >= 0f)
        {
            yield return new WaitForSeconds(0.05f);
            tmp.a -= 0.02f;
            image.color = tmp;

        }
        image.enabled = false;
        RichRoomCollider.instance.state = RichRoomCollider.State.toGoToLobbyDoor;
    }
   

}
