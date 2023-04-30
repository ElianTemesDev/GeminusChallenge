namespace FirstChallenge
{
    internal class Checkerboard
    {
        static void Main(string[] args)
        {
            Console.Write("Ingrese del 1 al 10 la dimension del tablero de damas a imprimir: ");
            int size = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            size = size < 1 ? 1 : size;
            size = size > 10 ? 10 : size;
            int[,] matrix = new int[size, size];

            if (size == 1)
            {
                Console.WriteLine("X");
            }
            else
            {
                CreateCheckerboard(size, matrix);
            }                
        }

        private static void CreateCheckerboard(int size, int[,] matrix)
        {

            for (int x = 1; x < matrix.GetLength(0) + 1; x++)
            {
                for (int y = 1; y < matrix.GetLength(0) + 1; y++)
                {
                    if (x % 2 != 0)
                    {
                        if (y % 2 != 0)
                        {
                            matrix[x - 1, y - 1] = 'X';
                        }
                        else
                        {
                            matrix[x - 1, y - 1] = '_';
                        }
                    }
                    else
                    {
                        if (y % 2 != 0)
                        {
                            matrix[x - 1, y - 1] = '_';
                        }
                        else
                        {
                            matrix[x - 1, y - 1] = 'X';
                        }
                    }
                }
            }
            PrintCheckerboard(matrix);
        }

        private static void PrintCheckerboard(int[,] matrix)
        {
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(0); y++)
                {
                    Console.Write((char)matrix[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}