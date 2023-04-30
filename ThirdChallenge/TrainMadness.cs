using System.Runtime.InteropServices;
using static ThirdChallenge.TrainMadness;

namespace ThirdChallenge
{
    internal class TrainMadness
    {
        static void Main(string[] args)
        {
            const int cubicMetersCapacity = 141, totalWagonsCubicCapacity = cubicMetersCapacity * 3;
            double[] wagonsSize = { cubicMetersCapacity, cubicMetersCapacity, cubicMetersCapacity};
            List<double> listOfBoxSizes = new();
            double[] boxValues = new double[3];
            bool impossibleBox = false, tooBigBoxFlag = false;
            List<Box> boxes = new();

            //Aca asumo que una caja jamas podria tener alguno de sus tres datos como cero por lo tanto esa caja no se toma como valida y se utilizan el resto
            AskForBoxes(boxValues, ref impossibleBox, ref boxes);

            /*Mientras ninguna caja individual supere las constantes de largo, ancho y alto, se asume factibilidad, despues se analizan 
            todas las cajas para ver que no superen la capacidad cubica total de los 3 vagones*/
            //Sumo los metros cubicos de las cajas y checkeo si alguna caja sobre pasa las dimensiones del tren
            double sumOfCubicMeters = SumCubicMetersOfBoxes(boxes, ref tooBigBoxFlag, ref listOfBoxSizes);

            /* Se fija que ninguna caja supere las medidas del vagon o que la suma de todas las cajas sobrepase la capacidad total de los 3 vagones
             despues se utiliza una heuristica de ubicar la caja en el vagon que tenga el maximo espacio disponible de los 3 */
            if (CheckFactibility(totalWagonsCubicCapacity, sumOfCubicMeters, tooBigBoxFlag))
            {
                int[] quantityOfBoxesInWagon = new int [3];
                int wagonIndex;
                foreach (double cubicMeters in listOfBoxSizes) 
                {
                    double maxValue;
                    maxValue = wagonsSize.Max();

                    wagonIndex = Array.IndexOf(wagonsSize, maxValue);
                    if (wagonsSize[wagonIndex] - cubicMeters > 0)
                    {
                        wagonsSize[wagonIndex] = wagonsSize[wagonIndex] - cubicMeters;
                        quantityOfBoxesInWagon[wagonIndex]++;
                    }
                    else
                    {
                        Console.WriteLine($"La caja de tamaño {cubicMeters} m3 no entra en ninguno de los tres vagones, se deja la caja y se sigue con las siguientes para ver si pueden entrar.");
                    }
                }
                for(int i = 0; i < wagonsSize.Length; i++)
                {
                    Console.WriteLine($"Sobra de espacio en el vagon {i+1} {wagonsSize[i]} m3.");
                    Console.WriteLine($"Cantidad de cajas en el vagon {quantityOfBoxesInWagon[i]}U.");
                }
            }
        }

        private static bool CheckFactibility(int totalWagonsCubicCapacity, double sumOfCubicMeters, bool tooBigBoxFlag)
        {
            if (sumOfCubicMeters <= totalWagonsCubicCapacity && tooBigBoxFlag != true)
            {
                Console.WriteLine($"Es factible guardar las cajas en los 3 vagones, estas ocuparian un total de {sumOfCubicMeters} m3");
                return true;
            }
            else
            {
                Console.WriteLine("No es factible guardar las cajas en los 3 vagones, estas superan la capacidad total de los 3 vagones.");
                Console.WriteLine($"La capacidad total de los 3 vagones es de {totalWagonsCubicCapacity} m3");
                Console.WriteLine($"Mientras que las cajas estan ocupando: {sumOfCubicMeters} m3");
                return false;
            }
        }

        private static double SumCubicMetersOfBoxes(List<Box> boxes, ref bool tooBigBoxFlag, ref List<double> cubicMeterOfEachBox)
        {
            double accumulator = 0;

            for (int i = 0; i <= boxes.Count() - 1; i++)
            {
                if (CheckIfBoxOutOfBounds(boxes[i], ref tooBigBoxFlag))
                {
                    break;
                }

                cubicMeterOfEachBox.Add(boxes[i].ToCubicMeters());
                accumulator += boxes[i].ToCubicMeters();
            }

            return accumulator;
        }

        private static bool CheckIfBoxOutOfBounds(Box box, ref bool tooBigBoxFlag)
        {
            if (!CanFitBox(box))
            {
                Console.WriteLine("Es imposible realizar la carga de todas las cajas, la ");
                Console.WriteLine($"caja de medida {box} excede en alguna dimension a la capacidad de los vagones");
                tooBigBoxFlag = true;
            }
            return tooBigBoxFlag;
        }

        private static void AskForBoxes(double[] boxValues, ref bool impossibleBox, ref List<Box> boxes)
        {
            Console.WriteLine("Ingrese en el orden expuesto el largo, ancho y altura de la caja: ");
            do
            {
                for (int i = 0; i < 3; i++)
                {
                    boxValues[i] = Convert.ToDouble(Console.ReadLine());
                }

                for (int i = 0; i < boxValues.Length; i++)
                {
                    if (boxValues[i] == 0)
                    {
                        impossibleBox = true;
                    }
                }
                if (!impossibleBox) boxes.Add(new Box(boxValues[0], boxValues[1], boxValues[2]));

                Console.Write("Si quiere seguir ingresando cajas presione Y: ");
            }
            while (Console.ReadLine()?.ToLower() == "y");
            Console.WriteLine();
        }

        private static bool CanFitBox(Box box)
        {
            //Declaro constantes para evitar los numeros magicos que no dan significado semantico
            const double length = 15.4, width = 3.051948052, height = 3;

            /*Si cualquiera de los valores de la caja supera las dimensiones del tren, el programa se interrumpe ya que es infactible la carga
            aunque se podria modificar para que ignore las cajas imposibles de cargar*/
            if (box.Length > length || box.Width > width || box.Height > height)
            {
                return false;
            }
            return true;
        }

        public struct Box
        {
            //Utilizo una struct principalmente porque es mucho mas similar a un tipo de dato que a un objeto con comportamiento en si
            public Box(double length, double width, double height)
            {
                Length = length;
                Width = width;
                Height = height;
            }

            public double Length { get; }
            public double Width { get; }
            public double Height { get; }

            public override string ToString() => $"Largo: {Length}, ancho: {Width}, altura: {Height}";
            public double ToCubicMeters() => Length * Width * Height;
        }
    }
}