using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SaveLoad : MonoBehaviour
{
    [SerializeField] SaveObject saveObj;
    [SerializeField] UserInfo currentUser;

    [Header("UI elements")]
    [SerializeField] InputField username_field;
    [SerializeField] InputField passwd_field;
    [SerializeField] Text info_text;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        json_load();
    }

    public SaveObject _testSaveLoad()
    {
        // test Save
        Result res1 = new Result();
        res1.accurateshot = 10;
        res1.missshot = 10;
        UserInfo u1 = new UserInfo();
        u1.username = "Tom";
        u1.passwd = "123";
        u1.results = new List<Result>();
        u1.results.Add(res1);
        SaveObject saveObj = new SaveObject();
        saveObj.userInfos = new List<UserInfo>();
        saveObj.userInfos.Add(u1);

        string json = JsonUtility.ToJson(saveObj);
        Debug.Log(json);

        // test Load
        SaveObject loaded = JsonUtility.FromJson<SaveObject>(json);
        Debug.Log("Loaded obj: " + loaded.userInfos[0].username);

        Debug.Log(check_user("Tom", "1234", loaded));
        return loaded;
    }



    private string check_user(string username, string passwd, SaveObject saveObj)
    {
        List<UserInfo> userList = saveObj.userInfos;
        int userIndex = userList.FindIndex(x => x.username == username);
        if (userIndex != -1)
        {
            UserInfo user = userList[userIndex];
            if (passwd == user.passwd)
            {
                return "passwd_correct";
            }
            else
            {
                return "passwd_incorrect";
            }
        }
        else
        {
            return "no_user";
        }
    }

    public void login_submit()
    {
        string username = username_field.text;
        string passwd = passwd_field.text;
        string login_info = check_user(username, passwd, saveObj);
        info_text.text = login_info;
        if (login_info == "passwd_correct")
        {
            currentUser = saveObj.userInfos.Find(x => x.username == username);
            SceneManager.LoadScene(sceneName: "VRScene");
        }
    }

    public void new_user()
    {
        string username = username_field.text;
        string passwd = passwd_field.text;

        if(saveObj.userInfos.Exists(x => x.username == username))
        {
            info_text.text = "user existed";
            return;
        }

        // Generate New User
        UserInfo new_user = new UserInfo();
        new_user.username = username;
        new_user.passwd = passwd;
        new_user.results = new List<Result>();
        saveObj.userInfos.Add(new_user);
        currentUser = new_user;
        info_text.text = new_user.username;

        // Save
        json_save();
        SceneManager.LoadScene(sceneName: "VRScene");
    }

    private void json_save()
    {
        if (saveObj!=null)
        {
            string json = JsonUtility.ToJson(saveObj);
            Debug.Log("Save at" + Application.dataPath);
            File.WriteAllText(Application.dataPath + "/save.txt", json);
        }
        else
        {
            Debug.Log("No Items to Save");
        }
    }

    private void json_load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveStr = File.ReadAllText(Application.dataPath + "/save.txt");
            saveObj = JsonUtility.FromJson<SaveObject>(saveStr);
            info_text.text = "Login or regist as New Player?";
        }
        else
        {
            saveObj = new SaveObject();
            info_text.text = "Become the first Player";
        }
    }

    public void update_user_info(int accuracteshot, int missedshot)
    {
        Result res = new Result();
        res.accurateshot = accuracteshot;
        res.missshot = missedshot;
        int index = saveObj.userInfos.FindIndex(x => x == currentUser);
        saveObj.userInfos[index].results.Add(res);

        json_save();
    }

    public string show_user_info()
    {
        string res = "Player Name: " + currentUser.username;
        for (int i = 0; i < currentUser.results.Count; i++)
        {
            res += "\n"+ i + ". Accurate Shot: " + currentUser.results[i].accurateshot;
            res += " | " + "Missed Shot: " + currentUser.results[i].missshot;
        }
        Debug.Log(res);
        return res;
    }



}

