﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject cubePrefab;
	Vector3 gridCubePosition, nextCubePosition;
	public GameObject[,] gridCubes;
	public GameObject nextCube;
	float eachTurnTime, turnTimer, eachGameTime, gameTimer;
	int gridCubeMaxX, gridCubeMaxY;
	Color[] nextCubeColor;
	Color currentNextCubeColor;
	int coloredX;
	public GameObject blackCubes;
	public int blackX, blackY;
	bool detectKeyboardInput;
	//public Text nextCubeText, gameOverText;


	void Start() 
	{
		gridCubeMaxX = 8;
		gridCubeMaxY = 5;
		gridCubes = new GameObject[gridCubeMaxX, gridCubeMaxY];

		blackX = 0;
		blackY = 0;

		detectKeyboardInput = false;

		eachTurnTime = 2.0f;
		turnTimer = eachTurnTime;
		eachGameTime = 60.0f;
		gameTimer = eachGameTime;

		nextCubeColor = new Color[5];
		nextCubeColor [0] = Color.blue;
		nextCubeColor [1] = Color.green;
		nextCubeColor [2] = Color.red;
		nextCubeColor [3] = Color.yellow;
		nextCubeColor [4] = Color.magenta;

		for (int x = 0; x < gridCubeMaxX; x++) 
		{
			for (int y = 0; y < gridCubeMaxY; y++)
			{
				gridCubePosition = new Vector3 (x*2, y*2, 0);
				gridCubes [x,y] = Instantiate (cubePrefab, gridCubePosition, Quaternion.identity);			
				gridCubes [x,y].GetComponent<Renderer>().material.color = Color.white;
			}
		}
		nextCubePosition = new Vector3 (20, 10, 0);
		nextCube = Instantiate (cubePrefab, nextCubePosition, Quaternion.identity);
		//test this line
		NewNextCube ();
	}

	//set for X position, check if it is occupied, change the color there
	public void GetInThereNextCube(int y)
	{

		List<GameObject> whiteCubesInLine = new List<GameObject> ();

		for (int x = 0; x < gridCubeMaxX; x++) {
				if (gridCubes[coloredX, y].GetComponent<Renderer>().material.color == Color.white) {
					whiteCubesInLine.Add(gridCubes[x,y]);
			}
		}

		if (whiteCubesInLine.Count == 0) {
				EndGame (false);
		}

		else {
				coloredX = Random.Range (0,whiteCubesInLine.Count);
				gridCubes[coloredX, y].GetComponent<Renderer>().material.color = currentNextCubeColor;
				gridCubes[coloredX, y].GetComponent<CubeBehavior>().isColored = true;
				Destroy(nextCube);
		}
	}

	//fail to press keyboard on time, blacken cubes
	public void BlackenCubes()
	{
		List<GameObject> whiteCubesAll = new List<GameObject> ();

		for (int x = 0; x < gridCubeMaxX; x++) {
			for (int y = 0; y < gridCubeMaxY; y++) {
				if (gridCubes[x, y].GetComponent<Renderer>().material.color == Color.white) {
					whiteCubesAll.Add(gridCubes[x,y]);
				}
			}
		}

		blackCubes = whiteCubesAll [Random.Range (0, whiteCubesAll.Count)];
		blackCubes.GetComponent<Renderer>().material.color = Color.black;
		gridCubes[blackX, blackY].GetComponent<CubeBehavior>().isBlacked = true;
		//cannot be destroyed
		Destroy(nextCube);
	}


	// 	coloredX = Random.Range (0,gridCubeMaxX);
	// 	if (Cubes[coloredX, y].GetComponent<Renderer>().material.color == Color.white)
	// 	{
	// 		gridCubes[coloredX, y].GetComponent<Renderer>.material.color = currentNextCubeColor;
	// 		Cubes[coloredX, y].GetComponent<CubeBehavior>().isColored = true;
	// 		nextCube = null;
	// 	}
	// 	else 
	// }


	public void KeyboardInput ()
	{
		if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
		{
			GetInThereNextCube(4);
			detectKeyboardInput = true;
		}
		if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
		{
			GetInThereNextCube(3);
			detectKeyboardInput = true;
		}

		if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
		{
			GetInThereNextCube(2);
			detectKeyboardInput = true;
		}

		if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4))
		{
			GetInThereNextCube(1);
			detectKeyboardInput = true;
		}
		if (Input.GetKey (KeyCode.Alpha5) || Input.GetKey (KeyCode.Keypad5)) {
			GetInThereNextCube (0);
			detectKeyboardInput = true;
		} 
		else {
			detectKeyboardInput = false;
		}

	}

	public void NewNextCube()
	{
		currentNextCubeColor = 	nextCubeColor [Random.Range (0,5) ];
		nextCube.GetComponent<Renderer>().material.color = currentNextCubeColor;

	}

	//activate the cube when click
	public void Actication(GameObject selectedCube, int x, int y)
	{
		if (selectedCube.GetComponent<CubeBehavior>().isSelected) {
		}
	}


	//deactivate the cube when click on an active cube
	public void Deactivation(GameObject selectedCube, int x, int y)
	{

	}


	public void ProcessClick(GameObject selectedCube, int x, int y)
	{

	}


	//add a UI here
	void EndGame(bool win)
	{
		if (win){
//			Text.win = "YOU WIN!";
			print ("win");
		}
		else {
//			Text.gameOverText = "GG!";
			print ("lose");
		}
	}

	void Update()
	{
		if (Time.time < gameTimer) {
			//press key
			KeyboardInput ();

			if (Time.time > turnTimer)
			{
				nextCubePosition = new Vector3 (20, 10, 0);
				nextCube = Instantiate (cubePrefab, nextCubePosition, Quaternion.identity);
				//test this line
				NewNextCube ();

				if (detectKeyboardInput == false) {
					BlackenCubes ();
					detectKeyboardInput = true;				
				}
					

				turnTimer += eachTurnTime;
			}

		}
	}
}
