using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class listenGrab : MonoBehaviour
{
    VRTK_InteractGrab _grabEvent;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Grab listener Start!");
        _grabEvent = GetComponent<VRTK_InteractGrab>();
        _grabEvent.ControllerGrabInteractableObject += onGrabObject;
        _grabEvent.ControllerStartUngrabInteractableObject += onGrabObjectRelease;
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
        Debug.Log("捡起枪");
        gun.GetComponent<VRTK_TransformFollow>().enabled = true;
        gun.GetComponent<VRTK_InteractObjectHighlighter>().enabled = false;
    }

    private void gun_Drop(GameObject gun)
    {
        Debug.Log("放下枪");
        gun.GetComponent<VRTK_TransformFollow>().enabled = false;
        gun.GetComponent<VRTK_InteractObjectHighlighter>().enabled = true;
    }

    
}
