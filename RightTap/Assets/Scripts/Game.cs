using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject mcPrefab;
    private GameObject mMainCharacter;
    
    void Start()
    {
		mMainCharacter = (GameObject)Instantiate(mcPrefab);
		mMainCharacter.GetComponent<MainCharacter>().Number = 25;
    }

    void Update()
    {

    }
}
