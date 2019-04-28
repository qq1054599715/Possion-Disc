using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PossionDisc
{
    private float minDistance;
    private float width, height;
    private int k;
    private int dimension = 2;
    private float cellSize;
    private int rows, cols;
    private List<Vector3> cells;
    private List<Vector3> spwanPoints;

    public PossionDisc(float width, float height, float minDistance, int k = 30)
    {
        this.k = k;
        this.width = width;
        this.height = height;
        this.dimension = 2;
        this.minDistance = minDistance;
        this.cellSize = this.minDistance / Mathf.Sqrt(this.dimension);
        this.rows = Mathf.FloorToInt(height / this.cellSize);
        this.cols = Mathf.FloorToInt(width / this.cellSize);
        this.cells = new List<Vector3>();
        for (int i = 0; i < this.rows; i++)
        {
            for (int j = 0; j < this.cols; j++)
            {
                this.cells.Add(new Vector3(float.NaN, 0, 0));
            }
        }
    }

    private bool IsFit(int idx, int idy, Vector3 newPosition)
    {
        bool fit = true;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int neighborX = idx + i;
                int neighborY = idy + j;
                bool isExist = neighborX > -1 && neighborX < this.cols && neighborY > -1 && neighborY < this.rows;
                if (isExist == false)
                {
                    continue;
                }

                int neighborIndex = neighborX + neighborY * this.cols;
                Vector3 neighbor = this.cells[neighborIndex];
                if (!float.IsNaN(neighbor.x))
                {
                    float distance = Vector3.SqrMagnitude(newPosition - neighbor);
                    if (distance < this.minDistance*this.minDistance)
                    {
                        fit = false;
                    }
                }
            }
        }

        return fit;
    }

    public void  Begin(float startX,float startY)
    {
        this.spwanPoints = new List<Vector3>();
        Vector3 startPosition = new Vector3(startX,startY);
        this.spwanPoints.Add(startPosition);
        int x = Mathf.FloorToInt(startPosition.x / this.cellSize);
        int y = Mathf.FloorToInt(startPosition.y / this.cellSize);
        this.cells[x + y * this.cols] = startPosition;
       
    }

    public List<Vector3> Next(ref bool isOver,ref bool hasSpwan,ref Vector3 spwanPoint)
    {
            int spwanIndex = UnityEngine.Random.Range(0, this.spwanPoints.Count);
            Vector3 centre = this.spwanPoints[spwanIndex];

            bool accept = false;
            
            for (int i = 0; i < k; i++)
            {
                float angle = UnityEngine.Random.Range(0, 2 * Mathf.PI);
                float mag = UnityEngine.Random.Range(this.minDistance, 2 * this.minDistance);
                Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * mag;
                Vector3 newPoint = centre + dir;

                int idx = Mathf.FloorToInt(newPoint.x / cellSize);
                int idy = Mathf.FloorToInt(newPoint.y / cellSize);

                if (idx > -1 && idx < rows && idy > -1 && idy < cols)
                {
                    bool fit = IsFit(idx, idy, newPoint);
                    if (fit == true)
                    {
                        accept = true;
                        hasSpwan = true;
                        spwanPoint = newPoint;
                        spwanPoints.Add(newPoint);
                        cells[idx + idy * cols] = newPoint;
                        break;
                    }
                }
            }

            if (accept == false)
            {
                spwanPoints.RemoveAt(spwanIndex);
            }

            if (this.spwanPoints.Count > 0)
            {
                isOver = false;
            }
            else
            {
                isOver = true;
            }

            return this.cells;
            
    }
    
}
