using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour {

	public float health = 100f;
	public Slider Slider;
	public Image FillImage; 
	public Color FullHealthColor = Color.green;
	public Color ZeroHealthColor = Color.red;
	public int ScoreValue = 100;
    public GameObject boom;

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
			ScoreManager.score += ScoreValue;
            Instantiate(boom, transform.position, transform.rotation);
            Destroy(this.gameObject);
		}
	}

	private void SetHealthUI ()
	{
		Slider.value = health;

		FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, health / 100f);
	}

}
