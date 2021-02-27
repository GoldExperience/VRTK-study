using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObject
{
    public List<UserInfo> userInfos;
}

[System.Serializable]
public class UserInfo
{
    public string username;
    public string passwd;
    public List<Result> results;
}
[System.Serializable]
public class Result
{
    public int accurateshot;
    public int missshot;
}
