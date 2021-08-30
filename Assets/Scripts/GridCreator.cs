using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GridCreator : MonoBehaviour
{
    public int width, height;           //Размеры поля
    public float cellSize;              //Размеры ячейки поля в юнитах

    public int minesCount;              //К-во мин на поле

    private Grid grid;
 
    // Start is called before the first frame update
    private void Start()
    {
        grid = new Grid(width, height, cellSize);

        //Генерируем мины
        SetupMines(minesCount);
        
        //Генерируем цифры для поиска мин
        SetupCellValues();

        //центрируем камеру на поле
        CenterCamera();
         
    }

    //Устанавливаем мины по случайным координатам, мина обозначается "-1"
    private void SetupMines(int count)
    {
        int x, y;
        var rand = new System.Random();     //Можно было использовать UnityEngine.Random, но System.Random кушает меньше памяти (по крайней мере так говорят мудрецы с Хабра)
        for (int i = 0; i < count; i++)
        {
            x = rand.Next(grid.Width);
            y = rand.Next(grid.Height);

            grid.SetValue(x, y, -1);
        }
    }


    //Устанавливаем значения ячеек без мин
    private void SetupCellValues()
    {
        int width, height;              //Переменные для хранения размерности поля. Алгоритм и так медленный, наверное не стоит вызывать GetLenght() каждый раз
        int minesAround;                //К-во мин вокруг ячейки
        int[,] values = grid.Values;    //Получаем значения исходной сетки 

        width = values.GetLength(0);
        height = values.GetLength(1);

        for(int x = 0; x < values.GetLength(0); x++)
        {
            

            for(int y = 0; y < values.GetLength(1); y++)
            {
                minesAround = 0;
                //Если мы на клетке с миной - пропускаем итерацию
                if (values[x, y] == -1)
                    continue;
       
                //Проверяем все ячейки вокруг текущей
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
