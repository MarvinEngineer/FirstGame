using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using IEnumerator = System.Collections.IEnumerator;

public class NewBehaviourScript : MonoBehaviour {

    public Camera cam;
    

    public void TakeScreen()
    {
        StartCoroutine(CaptureScreen());
    }


    public IEnumerator CaptureScreen()
    {

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();



        RenderTexture rt = new RenderTexture(1000, 700, 24);
        cam.targetTexture = rt;
        Texture2D screenShot = new Texture2D(1000, 700, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, 1000, 700), 0, 0);
        screenShot.Apply();
        gameObject.GetComponent<UITexture>().mainTexture = screenShot;

        cam.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] pngData = screenShot.EncodeToPNG();

        Debug.Log("ОК");

    }
}

