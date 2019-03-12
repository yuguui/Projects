using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildCity : MonoBehaviour {

	public int mapWidth = 9;
	public int mapHeight = 9;
	int[,] mapgrid;
	public int xInicial = 0;
	public int yInicial = 0;
	public float seed;
	GameObject[,] blocks = new GameObject[9, 9];
	public GameObject cityBlock;
	public GameObject player;


	// Use this for initialization
	void Start () {
		mapgrid = new int[mapWidth*5,mapHeight*5];

		//generate map data
		seed = Random.Range(0,100);


			
		//build streets
		int x= 0;
		for(int w = 0; w < mapWidth*5-1; w++)
		{
			for(int h = 0; h < mapHeight*5; h++)
			{
				if (w != mapWidth*5-1) 
				{
					mapgrid [x, h] = -1;
				}
			}
			x += Random.Range(3,5);
			if(x >= mapWidth*5-1) break;
		}	

		int z = 0;
		for(int h = 0; h < mapHeight*5; h++)
		{
			for(int w = 0; w < mapWidth*5; w++)
			{
				if (h != mapHeight*5-1) {
					if (mapgrid [w, z] == -1)
						mapgrid [w, z] = -3;
					else
						mapgrid [w, z] = -2;
				}
			}
			z += Random.Range(3,5);
			if(z >= mapWidth*5-1) break;
		}	 

		//generate city

		for (int h = 0; h < 9; h++) {
			for (int w = 0; w < 9; w++) {


				blocks [h, w] = Instantiate (cityBlock, transform.position, transform.rotation) as GameObject;
				blockConstruct myBlock = blocks[h, w].GetComponent<blockConstruct> ();
				myBlock.seed = seed;
				myBlock.xInicial = h*5;
				myBlock.yInicial = w*5;
				myBlock.mapgrid = mapgrid;

			}
		}
	}
	void Update () {
		blockConstruct myBlock = blocks[0, 0].GetComponent<blockConstruct> ();
		int minX = myBlock.xInicial*30;
		int minZ = myBlock.yInicial*30;

		myBlock = blocks[8, 8].GetComponent<blockConstruct> ();
		int maxX = myBlock.xInicial*30+150;
		int maxZ = myBlock.yInicial*30+150;
		//Debug.Log (player.transform.position.y);

		if (minX < player.transform.position.x - 825) 
		{
			newColum(0);

		}else if(maxX > player.transform.position.x + 825) 
		{
			newColum(8);

		}
		if (minZ < player.transform.position.z - 824) 
		{
			newLine(0);

		}else if(maxZ > player.transform.position.z + 825) 
		{
			newLine(8);

		}
			
	}
	void newColum(int c){
		for (int n = 0; n < 9; n++) {
			Destroy (blocks [c, n]);
		}
		if (c==0){
			for (int h = 1; h < 9; h++) {
				for (int w = 0; w < 9; w++) {
					blocks [h-1, w] = blocks[h,w];
				}
			}
		}else if(c==8){
			for (int h = 7; h >= 0; h--) {
				for (int w = 0; w < 9; w++) {
					blocks [h+1, w] = blocks[h,w];
				}
			}
		}

		for (int n = 0; n < 9; n++) {
			
			if(c==0){
				blockConstruct myBlock2 = blocks[7, n].GetComponent<blockConstruct> ();
				blocks [8, n] = Instantiate (cityBlock, transform.position, transform.rotation) as GameObject;
				blockConstruct myBlock = blocks[8, n].GetComponent<blockConstruct> ();
				myBlock.seed = seed;
				myBlock.xInicial = myBlock2.xInicial+5;
				myBlock.yInicial = myBlock2.yInicial;
				myBlock.mapgrid = mapgrid;

			}else if (c==8){
				blockConstruct myBlock2 = blocks[1, n].GetComponent<blockConstruct> ();
				blocks [0, n] = Instantiate (cityBlock, transform.position, transform.rotation) as GameObject;
				blockConstruct myBlock = blocks[0, n].GetComponent<blockConstruct> ();
				myBlock.seed = seed;
				myBlock.xInicial = myBlock2.xInicial-5;
				myBlock.yInicial = myBlock2.yInicial;
				myBlock.mapgrid = mapgrid;
			}
		}

	}
	void newLine(int c){
		for (int n = 0; n < 9; n++) {
			Destroy (blocks [n, c]);
		}
		if (c==0){
			for (int h = 1; h < 9; h++) {
				for (int w = 0; w < 9; w++) {
					blocks [w, h-1] = blocks[w,h];
				}
			}
		}else if(c==8){
			for (int h = 7; h >= 0; h--) {
				for (int w = 0; w < 9; w++) {
					blocks [w, h+1] = blocks[w,h];
				}
			}
		}

		for (int n = 0; n < 9; n++) {

			if(c==0){
				blockConstruct myBlock2 = blocks[n, 7].GetComponent<blockConstruct> ();
				blocks [n, 8] = Instantiate (cityBlock, transform.position, transform.rotation) as GameObject;
				blockConstruct myBlock = blocks[n, 8].GetComponent<blockConstruct> ();
				myBlock.seed = seed;
				myBlock.xInicial = myBlock2.xInicial;
				myBlock.yInicial = myBlock2.yInicial+5;
				myBlock.mapgrid = mapgrid;

			}else if (c==8){
				blockConstruct myBlock2 = blocks[n, 1].GetComponent<blockConstruct> ();
				blocks [n, 0] = Instantiate (cityBlock, transform.position, transform.rotation) as GameObject;
				blockConstruct myBlock = blocks[n, 0].GetComponent<blockConstruct> ();
				myBlock.seed = seed;
				myBlock.xInicial = myBlock2.xInicial;
				myBlock.yInicial = myBlock2.yInicial-5;
				myBlock.mapgrid = mapgrid;

			}
		}

	}
}
