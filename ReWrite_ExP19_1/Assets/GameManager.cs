using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글턴에 접근하기 위한 Static 변수 선언
    public static GameManager instance = null;
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
        DontDestroyOnLoad(gameObject);
        // Json 테스트용 디버그 테스트셋
        TestSet();
    }
    [SerializeField]
    public struct Pair{
        public int phaseNum;
        public int choiceNum;
    }

    public Pair pair;
    public string playerName;
    // 대화창인지, 선택지창인지
    public string type;
    // 현재 장소
    public string place;

    void TestSet()
    {
        pair.phaseNum = 1;
        pair.choiceNum = 2;
        playerName = "R";
        type = "D";
        place = "ROOM";
    }
    void Start()
    {/*
        pair.phaseNum = 1;
        pair.choiceNum = 1;
        playerName = "R";
        type = "D";
    */
    }

    
    void Update()
    {
        
    }
}
