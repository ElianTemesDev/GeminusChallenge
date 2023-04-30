namespace FirstChallenge
{
    internal class Checkerboard
    {
        static void Main(string[] args)
        {
            Console.Write("Ingrese del 1 al 10 la dimension del tablero de damas a imprimir: ");
            int size = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            size = Math.Max(1, Math.Min(10, size));
            char[,] matrix = new char[size, size];

            if (size == 1)
            {
                Console.WriteLine("X");
            }
            else
            {
                CreateCheckerboard(size, matrix);
                PrintCheckerboard(matrix);
            }
        }

        private static void CreateCheckerboard(int size, char[,] matrix)
        {

            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    matrix[x, y] = (x + y) % 2 == 0 ? 'X' : '_';
                }
            }
        }

        private static void PrintCheckerboard(char[,] matrix)
        {
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    Console.Write(matrix[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}
