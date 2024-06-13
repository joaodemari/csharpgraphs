namespace CsharpGraphs
{
    public class AdjacencyMatrixGraph : GraphBase
    {
        int[,] Matrix;

        public AdjacencyMatrixGraph(int numVertices, bool directed = false)
            : base(numVertices, directed)
        {
            this.Matrix = new int[numVertices, numVertices];
            GenerateEmptyMatrix(numVertices);
        }

        private void GenerateEmptyMatrix(int numVertices)
        {
            for (int row = 0; row < numVertices; row++)
            {
                for (int col = 0; col < numVertices; col++)
                {
                    Matrix[row, col] = 0;
                }
            }
        }

        public override void AddEdge(int v1, int v2, int weight = 1)
        {
            if (v1 >= this.numVertices || v2 >= this.numVertices || v1 < 0 || v2 < 0)
                throw new ArgumentOutOfRangeException("Vertices are out of bounds");

            if (weight < 1)
                throw new ArgumentException("Weight cannot be less than 1");

            this.Matrix[v1, v2] = weight;

            //In an undirected graph all edges are bi-directional
            if (!this.directed)
                this.Matrix[v2, v1] = weight;
        }

        public override IEnumerable<int> GetAdjacentVertices(int v)
        {
            if (v < 0 || v >= this.numVertices)
                throw new ArgumentOutOfRangeException("Cannot access vertex");

            List<int> adjacentVertices = new List<int>();
            for (int i = 0; i < this.numVertices; i++)
            {
                if (this.Matrix[v, i] > 0)
                    adjacentVertices.Add(i);
            }
            return adjacentVertices;
        }

        public override int GetEdgeWeight(int v1, int v2)
        {
            return this.Matrix[v1, v2];
        }

        public override void Display()
        {
            for (int i = 0; i < this.numVertices; i++)
            {
                Console.Write("{0} : ", i);
                for (int j = 0; j < this.numVertices; j++)
                {
                    Console.Write(this.Matrix[i, j]);
                }
            }
        }

        void printSolution(int[] dist, int n)
        {
            Console.Write("Vertex     Distance " + "from Source\n");
            for (int i = 0; i < numVertices; i++)
                Console.Write(i + " \t\t " + dist[i] + "\n");
        }

        public void dijkstra(int src)
        {
            int[] dist = new int[numVertices]; // The output array. dist[i]
            // will hold the shortest
            // distance from src to i

            // sptSet[i] will true if vertex
            // i is included in shortest path
            // tree or shortest distance from
            // src to i is finalized
            bool[] sptSet = new bool[numVertices];

            // Initialize all distances as
            // INFINITE and stpSet[] as false
            for (int i = 0; i < numVertices; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            // Distance of source vertex
            // from itself is always 0
            dist[src] = 0;

            // Find shortest path for all vertices
            for (int count = 0; count < numVertices - 1; count++)
            {
                // Pick the minimum distance vertex
                // from the set of vertices not yet
                // processed. u is always equal to
                // src in first iteration.
                int u = minDistance(dist, sptSet);

                // Mark the picked vertex as processed
                sptSet[u] = true;

                // Update dist value of the adjacent
                // vertices of the picked vertex.
                for (int v = 0; v < numVertices; v++)
                    // Update dist[v] only if is not in
                    // sptSet, there is an edge from u
                    // to v, and total weight of path
                    // from src to v through u is smaller
                    // than current value of dist[v]
                    if (
                        !sptSet[v]
                        && Matrix[u, v] != 0
                        && dist[u] != int.MaxValue
                        && dist[u] + Matrix[u, v] < dist[v]
                    )
                        dist[v] = dist[u] + Matrix[u, v];
            }

            // print the constructed distance array
            printSolution(dist, numVertices);
        }

        int minDistance(int[] dist, bool[] sptSet)
        {
            // Initialize min value
            int min = int.MaxValue,
                min_index = -1;

            for (int v = 0; v < numVertices; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }
    }
}
