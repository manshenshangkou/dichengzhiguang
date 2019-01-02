using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomButton : MonoBehaviour {

    private Camera camera;
    public float zoomLevel;
    private float zoomCurrent;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        zoomCurrent = zoomLevel;
	}
	
	// Update is called once per frame
	void Update () {
        ZoomManage();

        camera.orthographicSize = zoomCurrent;
	}

    void ZoomManage()
    {
        if (zoomCurrent>8)
        {
            zoomCurrent = 8;
        }
        if (zoomCurrent<5)
        {
            zoomCurrent = 5;
        }
    }

    public void OnZoomAddClick()
    {
        zoomCurrent += 3;

    }
    public void OnZoomMinClick()
    {
        zoomCurrent -= 3;
    }
}
