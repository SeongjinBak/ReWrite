using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JsonParser 
{
    public int length;
    public string isDialogue;
    public string[] dialogues;
    // 화자 이미지 출력할 장소
    public string direction;
    // 화자 이름
    public string name;

    public void DialogueParser()
    {
        for(int i = 0; i < length; i++)
        {
           
            string parsedStr = dialogues[i];
            for(int j = 0; j < parsedStr.Length; j++)
            {
                if(parsedStr[j] == '(')
                {
                    string speakerName = "";
                    string direction = "";
                    while(parsedStr[++j] != ',')
                    {
                        speakerName += parsedStr[j];
                    }
                    while(parsedStr[++j] != ')')
                    {
                        direction = parsedStr[j].ToString();
                    }
                    name = speakerName;
                    this.direction = direction;
                    string tempStr = "";
                    for(int index = j + 1; index < parsedStr.Length;index++)
                    {
                        tempStr += parsedStr[index];
                    }
                    dialogues[i] = tempStr;
                    break;
                }
            }
            




        }
    }
}


