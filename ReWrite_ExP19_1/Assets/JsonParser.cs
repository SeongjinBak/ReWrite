using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JsonParser 
{
    public int length;
    public string isDialogue;
    public string[] dialogues;
    // 화자 이미지 출력할 장소
    public string [] direction;
    // 화자 이름
    public string [] name;
    
    public void DialogueParser()
    {
        name = new string[length];
        direction = new string[length];

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
                    name[i] = speakerName;
                    this.direction[i] = direction;
                    string tempStr = "";
                    int modular = 1;
                    int limit = 15;
                    if(isDialogue == "choice")
                    {
                        limit = 11;
                    }
                    for(int index = j + 1; index < parsedStr.Length;index++)
                    {
                        tempStr += parsedStr[index];
                        if(modular++%limit == 0)
                        {
                            // 17개 이상이 되면 개행.
                            tempStr += "\n";
                        }
                    }
                    dialogues[i] = tempStr;
                    break;
                }
            }
            




        }
    }
}


