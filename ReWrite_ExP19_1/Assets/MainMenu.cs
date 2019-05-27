using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string R1ToLoad;
    public string EndingSceneToLoad;
    public string OptionToLoad;
    public string QuitGame;

    // 게임 스타트 버튼
    public Button gameStart;
    // 캐릭터 선택 이미지
    public GameObject ChselectB;

    public void LoadChse()
    { 
        gameStart.enabled = false;
        ChselectB.SetActive(true);
    }

    public void LoadR1()
    {
        SceneManager.LoadScene(R1ToLoad);
    }

    public void LoadES()
    {
        SceneManager.LoadScene(EndingSceneToLoad);
    }

    public void LoadOp()
    {
        SceneManager.LoadScene(OptionToLoad);
    }

    public void Quitgmae()
    {
        Application.Quit();
    }

    // 캐릭터 선택 버튼
    
}

