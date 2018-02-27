using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SetPosition : MonoBehaviour {

    private ImageTargetBehaviour itb_stones;

    // Use this for initialization
    void Start()
    {
        itb_stones = GameObject.Find("ImageTargetStones").GetComponent<ImageTargetBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (itb_stones != null)
        {
            Vector3 v3 = new Vector3(itb_stones.transform.position.x, itb_stones.transform.position.y, itb_stones.transform.position.z);
            transform.position = v3;
            transform.rotation = Quaternion.Euler(itb_stones.transform.rotation.eulerAngles * -1);
        }
    }
}
