using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string nickname;
    public long money;


    public int stageNum;
    //챕터의 클리어 여부
    public bool[] haveCharacter = new bool[3];


}

