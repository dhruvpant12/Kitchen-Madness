using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstFrame = true;

    // Update is called once per frame
    void Update()
    {
        if(isFirstFrame)
        {
            isFirstFrame = false;
            Loader.LoadGameScene();
        }
    }
}
