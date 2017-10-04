using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	public GameObject mcPrefab;
	private GameObject mainCharater;
    //private GameObject[] mMainCharacters;

	// Use this for initialization
	void Start () {
		mainCharater = (GameObject)Instantiate(mcPrefab);
		mainCharater.transform.position = new Vector3(0.0f, -4.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		mainCharater.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
	}

        //mMainCharacters = new GameObject[5];
        //for(int i=0; i<5; i++)
        //{
        //    mMainCharacters[i] = (GameObject)Instantiate(mcPrefab);
        //}
}
