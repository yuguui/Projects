using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour {

	public float health = 100f;
	public Slider Slider;
	public Image FillImage; 
	public Color FullHealthColor = Color.green;
	public Color ZeroHealthColor = Color.red;
    //public GameObject GameOver;
    public string escena;

	private void OnEnable()
	{
		SetHealthUI ();
    }

    public void recibirDaño(int daño)
    {
        health -= daño;
		SetHealthUI();
        if (health <= 0)
        {
            //GameOver.SetActive (true);
            gameObject.GetComponent<CharacterController2>().enabled = false;
            //WaitForSeconds(5);
            SceneManager.LoadScene(escena);
        }
    }

	private void SetHealthUI ()
	{
		Slider.value = health;

		FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, health / 100f);
	}

}
