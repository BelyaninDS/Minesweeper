using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Utilities;

public class Grid
{
    private int width, height;              //������� �����         
    private float cellSize;                 //������ ������ ����� � ���� (� ������)
    private int[,] gridArray;               //������ �������� �����
    private TextMesh[,] debugTextArray;     //������ ��� ������ �������� �� �����

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        
        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        //��������� ������ �����
        for (int x = 0; x < gridArray.GetLength(0); x++)
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {                
                //��������� ����� ����������
                debugTextArray[x, y] = UtilClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y), 20, Color.white, TextAnchor.MiddleCenter);

                //������ ���������� ����� �����
                //������������
                Debug.DrawLine(GetWorldPosition(x, y) - new Vector3(cellSize, cellSize) * 0.5f, GetWorldPosition(x, y+1) - new Vector3(cellSize, cellSize) * 0.5f, Color.white, 100f);
                //��������������
                Debug.DrawLine(GetWorldPosition(x, y) - new Vector3(cellSize, cellSize) * 0.5f, GetWorldPosition(x+1, y) - new Vector3(cellSize, cellSize) * 0.5f, Color.white, 100f);          
            }

        //������� �������������� �������
        Debug.DrawLine(GetWorldPosition(0, height) - new Vector3(cellSize, cellSize) * 0.5f, GetWorldPosition(width, height) - new Vector3(cellSize, cellSize) * 0.5f, Color.white, 100f);
        //������� ������������ �������
        Debug.DrawLine(GetWorldPosition(width, 0) - new Vector3(cellSize, cellSize) * 0.5f, GetWorldPosition(width, height) - new Vector3(cellSize, cellSize) * 0.5f, Color.white, 100f);

    }


    //���������� ������� ������ � ������������ �� �� �����������
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }


    //������ �������� ������ ��� ������ �� �����
    public void SetValue(int x, int y, int value)
    {
        gridArray[x, y] = value;

        if (value == -1)
        {
            debugTextArray[x, y].text = "*";
            debugTextArray[x, y].color = Color.red;
        }
        else if (value == 0)
            debugTextArray[x, y].text = "";
        else
        {
            debugTextArray[x, y].text = gridArray[x, y].ToString();
            //debugTextArray[x, y].color = new Color(0, 255 / gridArray[x,y], gridArray[x,y] * (255 / 9));      //���� �� ��������, ��������� �����
        }
    }


    //������� ������������
    //�-�� �����
    public int Height       
    {
        get { return height; }
    }

    //�-�� ��������
    public int Width        
    {
        get { return width; }
    }

    //������ �������� �����
    public int[,] Values
    {
        get { return gridArray; }
    }


}
