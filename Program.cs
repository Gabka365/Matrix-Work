using System;
using System.Drawing;
using static MatrixWork.ArraySupport;


namespace MatrixWork
{
    internal static class Program
    {
        private static void Main()
        {
            while (true)
            { 
                SizeInput(out int[] Size);
                MatrixFilling(in Size, out int[,] Matrix);
                Print(in Size, in Matrix);
                ChooseOperation(out int NumberOfOperation);
                Implement(in Size, in NumberOfOperation, ref Matrix);
            }

            
        }
    }
}


