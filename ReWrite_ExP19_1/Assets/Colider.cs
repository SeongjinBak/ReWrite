using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
          
            if(playerTr.position.x < list[i].position.x + 1f && playerTr.position.x > list[i].position.x - 1f)
            {
                if (playerTr.position.y < list[i].position.y + 1f && playerTr.position.y > list[i].position.y - 1f)
                {
                    // 해당되는 텍스트 출력.
                    //Debug.Log("상호작용 성공");
                    if(list[i].name == "point (2)")
                    {
                        Debug.Log("침대당");
                    }
                }
            }
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
