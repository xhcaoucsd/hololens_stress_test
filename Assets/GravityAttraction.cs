using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttraction : MonoBehaviour {
    public float g;

    private GameObject gravity;
    private Rigidbody rb;
    
    public void Initialize(GameObject gravity)
    {
        this.gravity = gravity;
        this.rb = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = (gravity.GetComponent<Transform>().position - transform.position).normalized;
        rb.AddForce(direction * g * Time.deltaTime);
	}
}
