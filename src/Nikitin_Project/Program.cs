using Nikitin_Project;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        double[,] graph =
        {
            { 0, 0.94, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.88, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity },
            { 0.94, 0, 0.66, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.2, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity },
            { double.PositiveInfinity, 0.66, 0, 1.04, double.PositiveInfinity, 1.7, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity },
            { double.PositiveInfinity, double.PositiveInfinity, 1.04, 0, double.PositiveInfinity, 0.77, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity },
            { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 0, 1.92, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity },
            { double.PositiveInfinity, double.PositiveInfinity, 1.7, 0.77, 1.92, 0, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.52 },
            { 1.88, 1.2, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 0, 0.53, double.PositiveInfinity, double.PositiveInfinity },
            { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 0.53, 0, 1.54, double.PositiveInfinity },
            { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.54, 0, 0.86 },
            { double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity, 1.52, double.PositiveInfinity, double.PositiveInfinity, 0.86, 0 }
        };

        Console.WriteLine("Начальная и конечная точки могут быть только одни, в ином случае программа аварийно завершит работу.");
        Console.WriteLine("Сначала введите точки, которые нужно проигнорировать, затем введите начальную и конечную точку для рассчёта кратч. пути");

        HashSet<int> ignoredPoints = GetIgnoredPoints();
        double[,] modifiedGraph = ModifyGraph(graph, ignoredPoints);

        Floyd floyd = new Floyd(modifiedGraph);

        int start = GetVertex("начальную вершину");
        int end = GetVertex("конечную вершину");

        double distance = floyd.GetDistance(start, end);
        if (double.IsInfinity(distance))
        {
            Console.WriteLine("Пути нет");
        }
        else
        {
            Console.WriteLine($"Кратчайшее расстояние: {distance} км");
            floyd.PrintPath(start, end);
        }
    }

    static HashSet<int> GetIgnoredPoints()
    {
        HashSet<int> ignoredPoints = new HashSet<int>();
        bool validInput = false;

        while (!validInput)
        {
            Console.WriteLine("Введите номера точек без контейнеров (через пробел): ");
            string[] ignoredInput = Console.ReadLine().Split(' ');
            validInput = true;

            foreach (string point in ignoredInput)
            {
                if (int.TryParse(point, out int p) && p >= 1 && p <= 10)
                {
                    ignoredPoints.Add(p - 1);
                }
                else
                {
                    Console.WriteLine($"Некорректный ввод: {point}. Пожалуйста, введите числа от 1 до 10.");
                    validInput = false;
                    break;
                }
            }
        }
        return ignoredPoints;
    }

    static double[,] ModifyGraph(double[,] graph, HashSet<int> ignoredPoints)
    {
        double[,] modifiedGraph = (double[,])graph.Clone();
        foreach (int point in ignoredPoints)
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                modifiedGraph[i, point] = double.PositiveInfinity;
                modifiedGraph[point, i] = double.PositiveInfinity;
            }
        }
        return modifiedGraph;
    }

    static int GetVertex(string vertexType)
    {
        int vertex = -1;
        while (vertex < 0 || vertex >= 10)
        {
            Console.WriteLine($"Введите {vertexType} (1-10): ");
            try
            {
                vertex = int.Parse(Console.ReadLine()) - 1;
                if (vertex < 0 || vertex >= 10)
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число от 1 до 10.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
            }
        }
        return vertex;
    }
}
