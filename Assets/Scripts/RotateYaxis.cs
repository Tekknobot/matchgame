using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateYaxis : MonoBehaviour
{
    public bool flipTrue = false;
    public float smooth = 1f;
    private Vector3 targetAngles;

    void Update()
    {
		if (flipTrue)
		{       
            targetAngles = transform.eulerAngles + 180f * Vector3.up; // what the new angles should be
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
        }
    }

    public void flipTile()
    {
        flipTrue = true;
    }   

    public void resetflipTile()
    {
        flipTrue = false;
        transform.rotation = Quaternion.identity;
    }          
}
