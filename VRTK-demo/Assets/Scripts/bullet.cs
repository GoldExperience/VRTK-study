using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float bullet_force = 1000f;
    [SerializeField] Info_Manager info;
    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("[INFO]").GetComponent<Info_Manager>();
        this.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * bullet_force);
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var tag = collision.collider.tag;
        

        Debug.Log("Bullet hit:" + tag);
        if (tag == "bullet")
        {
            Destroy(this.gameObject);
            info.update_when_hit();
        }
    }
}
