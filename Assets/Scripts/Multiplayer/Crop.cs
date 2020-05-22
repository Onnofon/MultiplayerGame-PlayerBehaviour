using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public bool isUnplanted = true;
    public bool harvestable;
    public GameObject crop;

    public float grow;

    public bool dirUp;
    public float speed;
    public Material harvestableMat;
    public Material growingMat;
    public Transform startTransform;
    public Transform maxGrowth;

    private void Start()
    {
        startTransform = crop.transform;
    }

    private void Update()
    {
        if (!isUnplanted && !harvestable)
        {
            crop.SetActive(true);
            if (dirUp)
                crop.transform.Translate(Vector2.up * speed * Time.deltaTime);

            if (crop.transform.position.y <= maxGrowth.position.y)
            {
                dirUp = true;
            }
            else
            {
                dirUp = false;
                isUnplanted = true;
                harvestable = true;
                crop.GetComponent<MeshRenderer>().material = harvestableMat;
                
            }
        }
    }
}
