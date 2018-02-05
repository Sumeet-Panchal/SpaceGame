using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

    public static ArrayList planetPositions;
    public static ArrayList planetNames;

    public Transform star;
    public float orbitSpeed;

    private void Awake()
    {
        if (planetNames == null)
            planetNames = new ArrayList();
        if (planetPositions == null)
            planetPositions = new ArrayList();

    }

    // Use this for initialization
    void Start () {
        if (!planetNames.Contains(gameObject.name))
        {
            planetNames.Add(gameObject.name);
            planetPositions.Add(transform.position);
        }
        else
        {
            transform.position = (Vector3) planetPositions[planetNames.IndexOf(gameObject.name)];
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(star.position, transform.forward, orbitSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        planetPositions[planetNames.IndexOf(gameObject.name)] = transform.position;
    }
}
