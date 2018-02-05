using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour {

    public static Vector3 playerSpawnInfo;

    public Transform rearThruster;
    public Transform frontThruster;
    public Rigidbody2D rb;
    public float rearThrusterForce;
    public float sideThrusterForce;
    public Transform playerParent;
    public Transform pointer;
    public Text pointerDistText;
    public string pointerDistStarter;

    public Queue planetParents;
    public Transform stars;
    public Transform planets;
    public bool inSolarSys;

    public string curPlanetLevelName;
    public bool onPlanet;

    public PauseController pause;

    public Vector2 playAreaX;
    public Vector2 playAreaY;

    public Text TEMP;

    // Use this for initialization
    void Start () {
        planetParents = new Queue();
        inSolarSys = false;
        transform.position = (Vector2)playerSpawnInfo;
        transform.rotation = Quaternion.Euler(0,0,playerSpawnInfo.z);
        TEMP.text = "";

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause.pause();
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForceAtPosition(rearThruster.transform.up * rearThrusterForce, rearThruster.position);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForceAtPosition(frontThruster.transform.up * rearThrusterForce, frontThruster.position);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddTorque(sideThrusterForce);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddTorque(-sideThrusterForce);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rb.drag = 1;
            rb.angularDrag = 1;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.drag = 0;
            rb.angularDrag = 0;
        }
        if (Input.GetKeyDown(KeyCode.Tab) && onPlanet)
        {
            startPlanetLevel();
        }

        if (Mathf.Abs(rb.angularVelocity) > 400) rb.angularVelocity = 400 * Mathf.Sign(rb.angularVelocity);

        updatePointer();
        
    }

    public void updatePointer()
    {
        float closest = Mathf.Infinity;
        Transform closeStar = null;
        Transform pointTo = stars;
        if (inSolarSys) pointTo = planets;

        for (int i = 0; i < pointTo.childCount; i++)
        {
            Transform cur = pointTo.GetChild(i);
            float dist = Vector2.Distance(cur.position, transform.position);
            if (dist < closest)
            {
                closest = dist;
                closeStar = cur;
            }
        }

        if (closest < 30)
        {
            pointer.gameObject.SetActive(false);
            pointerDistText.text = pointerDistStarter + (inSolarSys ? "Planet" : "Star") + ":\nHere";
        }
        else
        {
            pointer.gameObject.SetActive(true);
            Vector3 dir = closeStar.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            pointer.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
            pointerDistText.text = pointerDistStarter + (inSolarSys ? "Planet" : "Star") + ":\n" + (int)closest;
        }
    }

    public void startPlanetLevel()
    {
        if (SceneManager.GetSceneByName(curPlanetLevelName) == null)
        {
            TEMP.text = "Would Load " + curPlanetLevelName + " Level if it existed";
            Debug.Log("Would Load " + curPlanetLevelName + " Level if it existed");
        }
        else
        {
            SceneManager.LoadScene(curPlanetLevelName);
            playerSpawnInfo = new Vector3(transform.position.x, transform.position.y, transform.rotation.eulerAngles.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Planet":
                onPlanet = true;
                curPlanetLevelName = collision.gameObject.name;
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Planet":
                onPlanet = false;
                curPlanetLevelName = null;
                TEMP.text = "";
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.usedByEffector)
        {
            switch (collision.gameObject.tag)
            {
                case "Planet":
                    transform.SetParent(collision.transform);
                    planetParents.Enqueue(collision.gameObject);
                    rb.AddForceAtPosition(frontThruster.transform.up * rearThrusterForce, frontThruster.position);
                    rb.velocity /= 2;
                    break;
                case "Star":
                    inSolarSys = true;
                    planets = collision.transform.parent.GetChild(0).transform;
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.usedByEffector)
        {
            switch (collision.gameObject.tag)
            {
                case "Planet":
                    transform.SetParent(playerParent);
                    if (planetParents.Peek().Equals(collision.gameObject))
                        planetParents.Dequeue();
                    break;
                case "Star":
                    inSolarSys = false;
                    planets = null;
                    break;
            }
        }
        switch (collision.gameObject.tag)
        {
            case "PlayArea":
                if (transform.position.x > playAreaX.y)
                    transform.position = new Vector2(playAreaX.x, transform.position.y);
                else if (transform.position.x < playAreaX.x)
                    transform.position = new Vector2(playAreaX.y, transform.position.y);
                else if (transform.position.y < playAreaY.x)
                    transform.position = new Vector2(transform.position.x, playAreaY.y);
                else if (transform.position.y > playAreaY.y)
                    transform.position = new Vector2(transform.position.x, playAreaY.x);
                else
                    transform.position = new Vector2(0, 0);
                break;
        }
    }
}
