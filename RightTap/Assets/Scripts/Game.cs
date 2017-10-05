using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public GameObject mcPrefab;
    private GameObject mMainCharacter;
	private System.Random mRandomer;
    
    void Start()
    {
		mMainCharacter = (GameObject)Instantiate(mcPrefab);
		mRandomer = new System.Random();
    }

    void Update()
    {
		if(Input.GetMouseButton(0))
		{
			int number = mRandomer.Next(0, 100);
			mMainCharacter.GetComponent<MainCharacter>().Number = number;
		}
    }
}
