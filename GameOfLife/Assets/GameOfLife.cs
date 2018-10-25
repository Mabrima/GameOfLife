using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour {

    public float tick;
    float nextTick;
    int amountOfAliveNeighbors = 0;
    int numberOfRows = 100;
    int numberOfColumns = 100;
    int spawnChance = 10;

    public GameObject cellPrefab;
    Cell[,] cells;

	// Use this for initialization
	void Start () {
        initializeGameOfLife();
    }

    void initializeGameOfLife()
    {
        cells = new Cell[numberOfColumns, numberOfRows];

        Vector3 spawnOffset = new Vector3(0, 0, 0);
        Vector3 spawnXOffset = new Vector3(1, 0, 0);
        Vector3 spawnYOffset = new Vector3(0, 1, 0);

        for (int x = 0; x < numberOfColumns; x++)
        {
            for (int y = 0; y < numberOfRows; y++)
            {
                spawnOffset = spawnXOffset * x + spawnYOffset * y;

                GameObject newCell = Instantiate(cellPrefab, transform.position + spawnOffset, Quaternion.identity, transform);
                cells[x, y] = newCell.GetComponent<Cell>();
                if (Random.Range(0, 100) > spawnChance)
                {
                    cells[x, y].SetAlive(false);
                    cells[x, y].SetNextAlive(false);
                }
            }
        }

        nextTick = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Tick!"); 

        if (nextTick > tick) 
        {

            for (int x = 0; x < numberOfColumns; x++)
            {
                for (int y = 0; y < numberOfRows; y++)
                {
                    CheckCellNeighbors(x, y);
                    HandleCell(cells[x, y]);
                }
            }

            for (int x = 0; x < numberOfColumns; x++)
            {
                for (int y = 0; y < numberOfRows; y++)
                {
                    cells[x, y].UpdateState();
                }
            }

            nextTick -= tick;
        }
        nextTick += Time.deltaTime;
	}

    void CheckCellNeighbors(int x, int y)
    {
        amountOfAliveNeighbors = 0;

        int minX = -1;
        int maxX = 1;
        int minY = -1;
        int maxY = 1;

        if (x == 0)
            minX = 0;
        if (x == numberOfColumns - 1)
            maxX = 0;
        if (y == 0)
            minY = 0;
        if (y == numberOfRows - 1)
            maxY = 0;

        for (int xi = minX; xi <= maxX; xi++)
        {
            for (int yi = minY; yi <= maxY; yi++)
            {
                if (!(xi == 0 && yi == 0))
                    amountOfAliveNeighbors += cells[x + xi, y + yi].CheckAlive();
            }
        }
    }

    void HandleCell(Cell cell)
    {
        if (cell.CheckAlive() == 1  && (amountOfAliveNeighbors < 2 || amountOfAliveNeighbors > 3))
        {
            cell.SetNextAlive(false);
        }
        else if (cell.CheckAlive() == 0 && amountOfAliveNeighbors == 3)
        {
            cell.SetNextAlive(true);
        }
    }
}
