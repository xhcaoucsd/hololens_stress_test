using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;

public class Spawner : MonoBehaviour, IInputClickHandler, ISpeechHandler {
    public GameObject pf_poly;
    public GameObject gravity;
    public GameObject infoDisplay;
    public float launchSpeed;
    private int objCount;
    private int triCount;
    private int vertCount;
    private int fps;

    private readonly float FpsUpdateRate = 2.0f;
    private float fpsNextUpdate;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GameObject poly = (GameObject)Instantiate(pf_poly, Camera.main.transform.position, Camera.main.transform.rotation);
        poly.GetComponent<GravityAttraction>().Initialize(gravity);

        Mesh polyMesh = poly.GetComponent<MeshFilter>().mesh;
        if (polyMesh == null) return;
        objCount++;
        triCount += polyMesh.triangles.Length / 3;
        vertCount += polyMesh.vertices.Length;

        poly.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * launchSpeed);
        Debug.Log("hey");
        Debug.Log("objCount: " + objCount + " triCount: " + triCount + " vertCount: " + vertCount);
    }

    public void OnSpeechKeywordRecognized(SpeechKeywordRecognizedEventData eventData)
    {
        string command = eventData.RecognizedText.ToLower();
        switch (command)
        {
            case "reset":
                objCount = 0;
                triCount = 0;
                vertCount = 0;
                GameObject[] gameObjects;
                gameObjects = GameObject.FindGameObjectsWithTag("poly");
                foreach (GameObject o in gameObjects)
                {
                    Destroy(o.gameObject);
                }
                break;
            default:
                break;
        }
    }


    // Use this for initialization
    void Start () {
        InputManager.Instance.PushModalInputHandler(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        
        string newText = "objects: " + objCount + "\ntriangles: " + triCount + "\nvertices: " + vertCount + "\nFPS: " + fps;
        if (Time.time > fpsNextUpdate)
        {
            fps = (int) (1.0f / Time.deltaTime);
            fpsNextUpdate = Time.time + 1.0f / FpsUpdateRate;
        }
        infoDisplay.GetComponent<Text>().text = newText;
	}
}
