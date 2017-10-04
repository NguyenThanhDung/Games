using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public GameObject mcPrefab;

	// Use this for initialization
	void Start () {
		GameObject mc = (GameObject)Instantiate(mcPrefab);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
