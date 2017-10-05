using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
	private int mNumber;
	public int Number
	{
		set
		{
			if(mNumber!=value)
			{
				mNumber = value;
				this.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = mNumber.ToString();
			}
		}
	}

    void Start()
    {
        this.transform.position = new Vector3(0.0f, -3.0f, 0.0f);
    }
}
