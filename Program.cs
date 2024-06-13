using CsharpGraphs;

namespace Laboratorio
{
    class Program
    {
        static void Main()
        {
            AdjacencyMatrixGraph graph = new(6, true);
            graph.AddEdge(0, 1, 5);
            graph.AddEdge(1, 2, 6);
            graph.AddEdge(2, 3, 7);
            graph.AddEdge(1, 3, 7);
            graph.AddEdge(2, 4, 1);
            graph.AddEdge(3, 5, 1);
            graph.AddEdge(4, 5, 3);
            graph.AddEdge(0, 4, 5);
            graph.AddEdge(5, 2, 1);
            graph.AddEdge(0, 2, 1);

            graph.dijkstra(0);
        }
    }
}
