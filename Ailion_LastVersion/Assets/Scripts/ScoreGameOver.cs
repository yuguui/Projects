using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreGameOver : MonoBehaviour
{

	Text text;

	void Awake ()
	{
		text = GetComponent <Text> ();
        text.text = "Score: \n" + ScoreManager.score;

    }

    void Update ()
	{

	}
		
}
