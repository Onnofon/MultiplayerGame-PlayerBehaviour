using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMouseLook : MonoBehaviour
{
    // Use this for initialization
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity;
    public float smoothing = 2.0f;
    GameObject character;

    
    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            sensitivity += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            sensitivity -= 1;
        }

        //Mouse direction X en y
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //Mouse direction influenced by smoothing en sensitivity
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

        //Gives boundaries to the camera
        if (mouseLook.y < -90)
        {
            mouseLook.y = -90;
        }

        if (mouseLook.y > 90)
        {
            mouseLook.y = 90;
        }

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
