using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public GameObject mcPrefab;
	private GameObject mainCharater;

	// Use this for initialization
	void Start () {
		mainCharater = (GameObject)Instantiate(mcPrefab);
		mainCharater.transform.position = new Vector3(0.0f, -4.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		mainCharater.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
	}
}
