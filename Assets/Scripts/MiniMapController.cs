using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour {

    public Camera minimap;
    public Transform mapBounds;

	// Use this for initialization
	void Start ()
    {
        Debug.Log(mapBounds.position.y);
        Debug.Log(minimap.ScreenToWorldPoint(new Vector3(0, minimap.pixelHeight)));
        Debug.Log(minimap.ScreenToWorldPoint(new Vector3(minimap.pixelWidth, 0)));

        float top = minimap.ScreenToWorldPoint(new Vector3(0, minimap.pixelHeight)).y;
        float right = minimap.ScreenToWorldPoint(new Vector3(minimap.pixelWidth, 0)).x;

        Debug.Log(top + " " + right);

        Instantiate(mapBounds, new Vector3(right / 2, top), Quaternion.identity, transform);
        Instantiate(mapBounds, new Vector3(right / 2, 0), Quaternion.identity, transform);
        Instantiate(mapBounds, new Vector3(0, top/2), Quaternion.identity, transform);
        Instantiate(mapBounds, new Vector3(right, top/2), Quaternion.identity, transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
