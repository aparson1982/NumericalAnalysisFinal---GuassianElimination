using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NumericalAnalysisFinal
{
     class ProblemNumber1
    {
        private static void GaussianElimination()
        {
            //declares and instantiates a jaggedArray with double precision. 
            double[][] matrixA = {
                new double[] {4, 3, -2, 5},
                new double[] {8, 1, 0, 6},
                new double[] {2, 4, 5, 6},
                new double[] {3, 1, 7, 2}
            };
           
            double[] matrixB = { 26.6654, 37.7765, 54.4432, 37.7779 };  //declares and instantiates the augmented portion of the matrix with double precision

            Console.WriteLine("Original Problem: " );
            DisplayArray(matrixA);
            
            for (int k = 0; k < matrixB.Length; k++)
            {

                // Checks This finds the row to pivot and swaps rows
                int maxValue = k;
                for (int i = k + 1; i < matrixB.Length; i++)
                {
                    if (Math.Abs(matrixA[i][k]) > Math.Abs(matrixA[maxValue][k]))
                    {
                        maxValue = i;
                    }

                    var MatrixA_ik_Value = matrixA[i][k];  //used only for debugging
                    var MatrixA_maxValuek_value = matrixA[maxValue][k];  //used only for debugging
                }
                double[] modifiedRow = matrixA[k];
                matrixA[k] = matrixA[maxValue];
                matrixA[maxValue] = modifiedRow;
                var MatrixA_k_value = matrixA[maxValue];  //used only for debugging

                double t = matrixB[k];
                matrixB[k] = matrixB[maxValue];
                matrixB[maxValue] = t;
                var MatrixB_maxValue = matrixB[maxValue];  //used only for debugging
                
                // swap within A and B and perform forward elimination
                for (int i = k + 1; i < matrixB.Length; i++)
                {
                    double alpha = matrixA[i][k] / matrixA[k][k];
                    matrixB[i] -= alpha * matrixB[k];
                    for (int j = k; j < matrixB.Length; j++)
                    {
                        matrixA[i][j] -= alpha * matrixA[k][j];
                        var matrixA_ij_value = matrixA[i][j];
                    }

                    var matrixB_i_value = matrixB[i];
                }
            }
            Console.WriteLine("");
            Console.WriteLine("After Forward Elimination:");
            DisplayArray(matrixA);
            
            // performs back substitution and calculates the results for x1, x2, x3, x4
            double[] x = new double[matrixB.Length];
            double sum;
            for (int i = matrixB.Length - 1; i >= 0; i--)
            {
                sum = 0.0;
                for (int j = i + 1; j < matrixB.Length; j++)
                {
                   sum += matrixA[i][j] * x[j];
                }
                x[i] = (matrixB[i] - sum) / matrixA[i][i];
            }
            
            Console.WriteLine("");
            double[][] matrxA = matrixA;
            for (int i = 0; i < 4; i++)  //used for calculating the results of backward substitution in order to display them
            {
                double z = matrxA[i][i];
                for (int j = 0; j < 4; j++)
                {
                    matrxA[i][j] = matrxA[i][j] / z;
                }
            }
            Console.WriteLine("After Backward Substitution:");
            DisplayArray(matrxA);
            Console.WriteLine("");

            Console.WriteLine("Results:  ");
            for (int i = 0; i < matrixB.Length; i++)
            {
                Console.WriteLine("x" + i + "=" + "{0:N5}", x[i]);
            }
            Console.ReadLine();
        }

        private static void DisplayArray(dynamic array)  
        {
            if (array.Length == 0)
                return;

            if (array[0].GetType().IsArray)
            {
                foreach (var element in array)
                {
                    DisplayArray(element);
                }

            }
            else
            {
                int size = array.Length;
              
                string str = "[ ";

                for (int i = 0; i < size; i++)
                {
                    str += array[i].ToString();

                    if (i < size - 1)
                        str += ", ";
                }

                str += " ]";
                 Console.WriteLine(str);
            }

        }

        private static void Main(string[] args)
        {
            GaussianElimination();
        }
    }
}
