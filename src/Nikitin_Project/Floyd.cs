using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nikitin_Project
{
    class Floyd
    {
        private double[,] distances;
        private int[,] next;
        private const double INF = double.PositiveInfinity;

        public Floyd(double[,] graph)
        {
            int n = graph.GetLength(0);
            distances = new double[n, n];
            next = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    distances[i, j] = (graph[i, j] == 0 && i != j) ? INF : graph[i, j];
                    next[i, j] = (graph[i, j] != 0 && i != j) ? j : -1;
                }
            }

            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (distances[i, k] + distances[k, j] < distances[i, j])
                        {
                            distances[i, j] = distances[i, k] + distances[k, j];
                            next[i, j] = next[i, k];
                        }
                    }
                }
            }
        }

        public double GetDistance(int from, int to) => distances[from, to];

        public void PrintPath(int start, int end)
        {
            if (next[start, end] == -1)
            {
                Console.WriteLine("Путь не существует");
                return;
            }
            Console.Write("Маршрут: " + (start + 1));
            while (start != end)
            {
                start = next[start, end];
                Console.Write(" -> " + (start + 1));
            }
            Console.WriteLine();
        }
    }
}
