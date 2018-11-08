using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/* 		Author : Saad Khawaja
	 *  http://www.saadkhawaja.com
	 * 	http://www.twitter.com/saadskhawaja

	 *     This file is part of Grid Based A* - Tower Defense.

		    Grid Based A* - Tower Defense is free software: you can redistribute it and/or modify
		    it under the terms of the GNU General Public License as published by
		    the Free Software Foundation, either version 3 of the License, or
		    (at your option) any later version.

		    Grid Based A* - Tower Defense is distributed in the hope that it will be useful,
		    but WITHOUT ANY WARRANTY; without even the implied warranty of
		    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
		    GNU General Public License for more details.


	 * 
*/ 

public class EnemyAStar : MonoBehaviour {
	
	public GameManager_ASTAR Game;
	public MyPathNode nextNode;
	bool gray = false;
	public MyPathNode[,] grid;
    
	public gridPosition currentGridPosition = new gridPosition();
	public gridPosition startGridPosition = new gridPosition();
	public gridPosition endGridPosition = new gridPosition();
	
	private Orientation gridOrientation = Orientation.Vertical;
	private bool allowDiagonals = true;
	private bool correctDiagonalSpeed = true;
	private Vector2 input;
	private bool isMoving = true;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float t;
	private float factor;
	private Color myColor;
    

	// Use this for initialization
	void Start () {
	
		myColor = getRandomColor();

		startGridPosition = new gridPosition(0,UnityEngine.Random.Range(0,Game.gridHeight-1));
		endGridPosition = new gridPosition(Game.gridWidth-1,UnityEngine.Random.Range(0,Game.gridHeight-1));
		initializePosition ();


		//PathSolver<MyPathNode, System.Object> aStar = new PathSolver<MyPathNode, System.Object>(Game.grid);
		//IEnumerable<MyPathNode> path = aStar.Search(new Vector2(startGridPosition.x, startGridPosition.y), new Vector2(endGridPosition.x, endGridPosition.y), null);



		foreach(GameObject g in GameObject.FindGameObjectsWithTag("GridBox"))
		{
			g.GetComponent<Renderer>().material.color = Color.white;
		}


		updatePath();

		this.GetComponent<Renderer>().material.color = myColor;



	}


    /// <summary>
    /// 更新paths，并取下一个node位置
    /// </summary>
    /// <param name="currentX"></param>
    /// <param name="currentY"></param>
	public void findUpdatedPath(int currentX,int currentY)
	{
		PathSolver<MyPathNode, System.Object> aStar = new PathSolver<MyPathNode, System.Object>(Game.grid);
		IEnumerable<MyPathNode> path = aStar.Search(new Vector2(currentX, currentY), new Vector2(endGridPosition.x, endGridPosition.y), null);

        int x = 0;

		if (path != null) {
		
			foreach (MyPathNode node in path)
			{
				if(x==1)
				{
					nextNode = node;
					break;
				}

				x++;

			}


			foreach(GameObject g in GameObject.FindGameObjectsWithTag("GridBox"))
			{
				if(g.GetComponent<Renderer>().material.color != Color.red && g.GetComponent<Renderer>().material.color == myColor)
					g.GetComponent<Renderer>().material.color = Color.white;
			}


			foreach (MyPathNode node in path)
			{
				GameObject.Find(node.X + "," + node.Y).GetComponent<Renderer>().material.color = myColor;
			}
		}
		
		
		


		
	}


	Color getRandomColor()
	{
		Color tmpCol = new Color(UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f),UnityEngine.Random.Range(0f,1f));
		return tmpCol;

	}
	// Update is called once per frame
	void Update () {
		
		if (!isMoving) {
			StartCoroutine(move());
		}
	}


	
	public float moveSpeed;
	
	public class gridPosition{
		public int x =0;
		public int y=0;

		public gridPosition()
		{
		}

		public gridPosition (int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	};
	
	
	private enum Orientation {
		Horizontal,
		Vertical
	};

	
	public IEnumerator move() {
		isMoving = true;
		startPosition = transform.position;
		t = 0;
		
		if(gridOrientation == Orientation.Horizontal) {
			endPosition = new Vector2(startPosition.x + System.Math.Sign(input.x) * Game.gridSize,
			                          startPosition.y);
			currentGridPosition.x += System.Math.Sign(input.x);
		} else {
			endPosition = new Vector2(startPosition.x + System.Math.Sign(input.x) * Game.gridSize,
			                          startPosition.y + System.Math.Sign(input.y) * Game.gridSize);
			
			currentGridPosition.x += System.Math.Sign(input.x);
			currentGridPosition.y += System.Math.Sign(input.y);
		}
		
		if(allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
			factor = 0.9071f;
		} else {
			factor = 1f;
		}

	
		while (t < 1f) {
			t += Time.deltaTime * (moveSpeed/Game.gridSize) * factor;
			transform.position = Vector2.Lerp(startPosition, endPosition, t);
			yield return null;
		}
		
		
		
		isMoving = false;
		getNextMovement ();
		
		yield return 0;
		
		
		
		
		
	}
	
	void updatePath()
	{
		findUpdatedPath (currentGridPosition.x, currentGridPosition.y);
	}
	
	void getNextMovement()
	{
		updatePath();

        Color color = this.GetComponent<SpriteRenderer>().color;
        if (Game.grid[currentGridPosition.x, currentGridPosition.y].IsShadow)
            this.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0.5f);
        else
            this.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1f);


        input.x = 0;
		input.y = 0;
		if (nextNode.X > currentGridPosition.x) {
			input.x = 1;
			this.GetComponent<SpriteRenderer>().sprite = Game.carFront;
		}
		if (nextNode.Y > currentGridPosition.y) {
			input.y = 1;
			this.GetComponent<SpriteRenderer>().sprite = Game.carUp;
		}
		if (nextNode.Y < currentGridPosition.y) {
			input.y = -1;
			this.GetComponent<SpriteRenderer>().sprite = Game.carDown;
		}
		if (nextNode.X < currentGridPosition.x) {
			input.x = -1;
			this.GetComponent<SpriteRenderer>().sprite = Game.carBack;
		}
		
		StartCoroutine (move ());
	}
	
	public Vector2 getGridPosition(int x, int y)
	{
		float contingencyMargin = Game.gridSize*.10f;
		float posX = Game.gridBox.transform.position.x + (Game.gridSize*x) - contingencyMargin;
		float posY = Game.gridBox.transform.position.y + (Game.gridSize*y) + contingencyMargin;
		return new Vector2(posX,posY);	
		
	}
	
	
	public void initializePosition()
	{
		this.gameObject.transform.position = getGridPosition (startGridPosition.x, startGridPosition.y);
		currentGridPosition.x = startGridPosition.x;
		currentGridPosition.y = startGridPosition.y;
		isMoving = false;
		GameObject.Find(startGridPosition.x + "," + startGridPosition.y).GetComponent<Renderer>().material.color = Color.black; 

	}
	


}
