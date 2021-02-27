using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller_Events : MonoBehaviour
{
    [Header("Gun")]
    [SerializeField] Transform gunPosition;
    [SerializeField] GameObject bullet;
    [SerializeField] bool useGun;
    [SerializeField] int bulletNum;

    [Header("Info")]
    [SerializeField] Info_Manager info;
    [SerializeField] SaveLoad saveload;

    VRTK_ControllerEvents _events;
    VRTK_InteractGrab _grabEvent;

    // Start is called before the first frame update
    public void OnEnable()
    {
        useGun = false;

        info = GameObject.Find("[INFO]").GetComponent<Info_Manager>();
        saveload = GameObject.Find("[SAVELOAD]").GetComponent<SaveLoad>();

        // Controller Events
        _events = GetComponent<VRTK_ControllerEvents>();
        _events.TriggerPressed += onTriggerPressed;
        _events.TriggerReleased += onTriggerReleased;

        _events.GripPressed += onGripPressed;
        _events.GripReleased += onGripReleased;

        // Grab Events
        _grabEvent = GetComponent<VRTK_InteractGrab>();
        _grabEvent.ControllerGrabInteractableObject += onGrabObject;
        _grabEvent.ControllerStartUngrabInteractableObject += onGrabObjectRelease;

        Debug.Log("events start");
    }

    public void onTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        
        if (useGun) {
            if (bulletNum <= 0)
            {
                Debug.Log("No bullet");
                saveload.update_user_info(info.accurateShot,info.missShot);
                SceneManager.LoadScene(sceneName: "ScoreScene");
                return;
            }
            Debug.Log("Trigger 键被按下！");
            GameObject go = Instantiate(bullet, gunPosition.position, gunPosition.rotation);
            //go.GetComponent<Rigidbody>().AddForce(gunPosition.position*1000f);
            bulletNum -= 1;
            info.update_when_shot(bulletNum);
        } 
    }

    public void onTriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("Trigger 键松开！");
    }

    public void onGripPressed(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("Grip Pressed!");
    }

    public void onGripReleased(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("Grip Released!");
    }

    private void onGrabObject(object sender, ObjectInteractEventArgs e)
    {
        Debug.Log("Controller" + e.controllerReference.scriptAlias.name + " Grabed Item " + e.target.name);
        if (e.target.name == "gun")
        {
            this.gun_Follow(e.target);
        }
    }
    private void onGrabObjectRelease(object seder, ObjectInteractEventArgs e)
    {
        if (e.target.name == "gun")
        {
            this.gun_Drop(e.target);
        }
    }

    private void gun_Follow(GameObject gun)
    {
        Debug.Log("Grab the Gun");
        useGun = true;
        gun.GetComponent<VRTK_TransformFollow>().enabled = true;
        gun.GetComponent<VRTK_InteractObjectHighlighter>().enabled = false;
        gunPosition = gun.transform.Find("GunPos").transform;
    }

    private void gun_Drop(GameObject gun)
    {
        useGun = false;
        Debug.Log("Release the Gun");
        gun.GetComponent<VRTK_TransformFollow>().enabled = false;
        gun.GetComponent<VRTK_InteractObjectHighlighter>().enabled = true;
    }
}
