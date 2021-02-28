using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowRes : MonoBehaviour
{
    SaveLoad saveload;
    Text res_info;
    // Start is called before the first frame update
    void Start()
    {
        saveload = GameObject.Find("[SAVELOAD]").GetComponent<SaveLoad>();
        res_info = GameObject.Find("ResInfo").GetComponent<Text>();
        res_info.text = saveload.show_user_info();
        Cursor.visible = true;
        Cursor.lockState = 0;
    }

    public void exit_game()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
              Application.Quit();
        #endif
    }

    public void replay_game()
    {
        Destroy(saveload.gameObject);
        SceneManager.LoadScene("LoginScene");
    }
}
