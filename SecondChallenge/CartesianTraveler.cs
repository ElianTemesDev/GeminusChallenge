namespace SecondChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[4, 4];
            int[] values = new int[] { 1, 2, -1, 1, 0, 1, 2, -1, -1, -2 };

            int x = 0, y = 0;
            matrix = InitializeMatrix(matrix, x, y);

            Console.WriteLine("Si quiere usar los valores de prueba oprima la letra Y, caso contrario N. ");
            Console.Write("Si no ingresa ningun valor, se asume que la respuesta es N, si el valor es incorrecto, se asume Y: ");
            string response = Console.ReadLine() == "" ? "n" : "y";

            //Desconozco si es asi, pero intuyo que si la letra ingresada es minuscula, el compilador mismo sacaria el .ToLower por ser innecesario.
            if(response.ToLower() == "n")
            {
                Console.WriteLine("Ingrese de a un numero a la vez hasta completar 10 ingresos totales un valor numerico entero, " +
                    "puede ser negativo o positivio.");

                InputCordinates(ref values);
            }

            PrintMatrixMovements(matrix, values, ref x, ref y);
        }

        private static void PrintMatrixMovements(int[,] matrix, int[] values, ref int x, ref int y)
        {
            // actualizo en base a las cordenadas el valor de X y muestro por pantalla cada movimiento
            for (int i = 0; i < values.Length; i += 2)
            {
                int newX = values[i + 1];
                int newY = values[i];

                int lastX = x, lastY = y; // guardo la ultima posicion de X
                x += newX;
                y += newY;
                // me aseguro que X no salga de la matriz
                if (x < 0) x = 0;
                if (x > 3) x = 3;
                if (y < 0) y = 0;
                if (y > 3) y = 3;
                matrix[lastX, lastY] = 'O'; // remplazo por O la posicion anterior
                matrix[x, y] = 'X';

                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        Console.Write((char)matrix[j, k] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        private static void InputCordinates(ref int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                int value;
                while (!Int32.TryParse(Console.ReadLine(), out value))
                {
                    Console.WriteLine("Escriba un solo valor numerico entero porfavor.");
                }
                values[i] = value;
            }
        }

        private static int[,] InitializeMatrix(int[,] matrix, int x, int y)
        {
            // Inicializo la matriz con caracteres O
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matrix[i, j] = 'O';
                }
            }

            matrix[x, y] = 'X';

            return matrix;
        }
    }
}