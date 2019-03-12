using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockConstruct : MonoBehaviour {
	public GameObject[] buildings;
	public GameObject Xstreet;
	public GameObject Zstreet;
	public GameObject crossroad;
	int buildingFootPrint = 30;
	public int xInicial = 0;
	public int yInicial = 0;
	public int[,] mapgrid;
	public float seed=42; 
	int result;
	public bool activated;


	// Use this for initialization
	void Start () {
		for (int h = 0; h < 5; h++) {
			for (int w = 0; w < 5; w++) {
				if (mapgrid [(w + Mathf.Abs(xInicial))%45, (h + Mathf.Abs(yInicial))%45] < 0) {
					result = mapgrid [(w + Mathf.Abs(xInicial))%45, (h + Mathf.Abs(yInicial))%45];

				} else {
					result = (int)(Mathf.PerlinNoise ((w + xInicial) / 10.0f + seed, (h + yInicial) / 10.0f + seed) * 10);

				}
				Vector3 pos = new Vector3 ((w + xInicial) * buildingFootPrint, 0, (h + yInicial) * buildingFootPrint);
				if (result == -3) {
					var build = Instantiate (crossroad, pos, crossroad.transform.rotation);
					build.transform.parent = gameObject.transform;
				} else if (result == -2) {
					var build = Instantiate (Zstreet, pos, Zstreet.transform.rotation);
					build.transform.parent = gameObject.transform;

				} else if (result == -1) {
					var build = Instantiate (Xstreet, pos, Xstreet.transform.rotation);
					build.transform.parent = gameObject.transform;

				} else if (result < 2) {
					pos.y = 0.001f;
					int n = Random.Range (0, 5);
					var build = Instantiate (buildings [0], pos, Quaternion.identity);
					build.transform.parent = gameObject.transform;
				} else {
					int n = Random.Range (5, buildings.Length);
					var build = Instantiate (buildings [n], pos,buildings[n].transform.rotation);
					build.transform.parent = gameObject.transform;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
