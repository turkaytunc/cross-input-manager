using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");

        transform.position += horizontalInput * Time.deltaTime * Vector3.right;
    }
}
