using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public Transform[] destinations;
    public Transform playerTr;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Moving());
    }
    
    IEnumerator Moving()
    {
        //yield return new WaitForSeconds(1f);
        while(playerTr.position.x != destinations[0].position.x && playerTr.position.y != destinations[0].position.y)
        {
            Vector2.MoveTowards(playerTr.position, destinations[0].position, 20f * Time.deltaTime);
            yield return new WaitForSeconds(0.02f);
        }
    }

}
