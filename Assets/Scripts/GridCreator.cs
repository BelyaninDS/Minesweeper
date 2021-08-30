using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GridCreator : MonoBehaviour
{
    public int width, height;           //������� ����
    public float cellSize;              //������� ������ ���� � ������

    public int minesCount;              //�-�� ��� �� ����

    private Grid grid;
 
    // Start is called before the first frame update
    private void Start()
    {
        grid = new Grid(width, height, cellSize);

        //���������� ����
        SetupMines(minesCount);
        
        //���������� ����� ��� ������ ���
        SetupCellValues();

        //���������� ������ �� ����
        CenterCamera();
         
    }

    //������������� ���� �� ��������� �����������, ���� ������������ "-1"
    private void SetupMines(int count)
    {
        int x, y;
        var rand = new System.Random();     //����� ���� ������������ UnityEngine.Random, �� System.Random ������ ������ ������ (�� ������� ���� ��� ������� ������� � �����)
        for (int i = 0; i < count; i++)
        {
            x = rand.Next(grid.Width);
            y = rand.Next(grid.Height);

            grid.SetValue(x, y, -1);
        }
    }


    //������������� �������� ����� ��� ���
    private void SetupCellValues()
    {
        int width, height;              //���������� ��� �������� ����������� ����. �������� � ��� ���������, �������� �� ����� �������� GetLenght() ������ ���
        int minesAround;                //�-�� ��� ������ ������
        int[,] values = grid.Values;    //�������� �������� �������� ����� 

        width = values.GetLength(0);
        height = values.GetLength(1);

        for(int x = 0; x < values.GetLength(0); x++)
        {
            

            for(int y = 0; y < values.GetLength(1); y++)
            {
                minesAround = 0;
                //���� �� �� ������ � ����� - ���������� ��������
                if (values[x, y] == -1)
                    continue;
       
                //��������� ��� ������ ������ �������
                for(int dx = -1; dx < 2; dx++)
                {
                    for(int dy = -1; dy < 2; dy++)
                    {
                        int xdx = x + dx;
                        int ydy = y + dy;
                        if ((xdx < width && xdx >= 0) && (ydy < height && ydy >=0) && values[xdx, ydy] == -1)
                            minesAround++;
                    }
                }
                grid.SetValue(x, y, minesAround);
            }   
        }           
    }


    private void CenterCamera()
    {
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camera.position = new Vector3(width * cellSize / 2, height * cellSize/2, camera.position.z);
    }
}
