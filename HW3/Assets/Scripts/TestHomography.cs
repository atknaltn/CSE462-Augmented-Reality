using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;

public class TestHomography : MonoBehaviour
{
	private void Start()
    {
		/*PART 1.1*/
		Vector3[] source = new Vector3[4];
        source[0] = new Vector3(256,390,0);
		source[1] = new Vector3(418,842,0);
		source[2] = new Vector3(347,642,0);
		source[3] = new Vector3(741,596,0);
		Vector3[] destination = new Vector3[4];
		destination[0] = new Vector3(256, 390, 0);
		destination[1] = new Vector3(418, 842, 0);
		destination[2] = new Vector3(347, 642, 0);
		destination[3] = new Vector3(741, 596, 0);

		double[,] homographyMatrix = MatrixConverter(FindHomography(source, destination));
		printMatrix(homographyMatrix);

		/*PART 1.3-4*/
		double[,] xy = new double[3, 1] { { source[1].x }, { source[1].y }, { 1 } };
		double[,] uv = new double[3, 1] { { destination[1].x }, { destination[1].y }, { 1 } };

		CalculateTarget(homographyMatrix, xy);
		CalculateSource(homographyMatrix, uv);

		/*PART 1.5*/
		Vector3[] s2 = new Vector3[5];
		s2[0] = new Vector3(600, 200, 0);
		s2[1] = new Vector3(100, 500, 0);
		s2[2] = new Vector3(500, 400, 0);
		s2[3] = new Vector3(800, 600, 0);
		s2[4] = new Vector3(300, 700, 0);

		Vector3[] d1 = new Vector3[5];
		d1[0] = new Vector3(2033, 1613, 0);
		d1[1] = new Vector3(885, 917, 0);
		d1[2] = new Vector3(1809, 1161, 0);
		d1[3] = new Vector3(2501, 709, 0);
		d1[4] = new Vector3(1345, 465, 0);

		Vector3[] d2 = new Vector3[5];
		d2[0] = new Vector3(2057, 1437, 0);
		d2[1] = new Vector3(873, 713, 0);
		d2[2] = new Vector3(1817, 961, 0);
		d2[3] = new Vector3(2521, 509, 0);
		d2[4] = new Vector3(1358, 266, 0);

		Vector3[] d3 = new Vector3[5];
		d3[0] = new Vector3(1861, 1649, 0);
		d3[1] = new Vector3(805, 937, 0);
		d3[2] = new Vector3(1657, 1193, 0);
		d3[3] = new Vector3(2389, 745, 0);
		d3[3] = new Vector3(1213, 493, 0);

		double[,] hm1 = MatrixConverter(FindHomography(s2, d1));
		double[,] hm2 = MatrixConverter(FindHomography(s2, d2));
		double[,] hm3 = MatrixConverter(FindHomography(s2, d3));

		List<double[,]> Points = new List<double[,]>();  
		List<double[,]> actualPoints = new List<double[,]>(); 

		Points.Add(new double[3, 1] { { 400 }, { 600 }, { 1 } });
		Points.Add(new double[3, 1] { { 600 }, { 700 }, { 1 } });
		Points.Add(new double[3, 1] { { 900 }, { 300 }, { 1 } });

		/// Image 1 
		Debug.Log("Image 1");
		actualPoints.Add(new double[3, 1] { { 1585 }, { 701 }, { 1 } });
		actualPoints.Add(new double[3, 1] { { 2041 }, { 465 }, { 1 } });
		actualPoints.Add(new double[3, 1] { { 2729 }, { 1397 }, { 1 } });

		CalculateError(hm1, Points, actualPoints);

		/// Image 2
		Debug.Log("Image 2");
		actualPoints = new List<double[,]>(); 

		actualPoints.Add(new double[3, 1] { { 1585 }, { 497 }, { 1 } });
		actualPoints.Add(new double[3, 1] { { 2053 }, { 277 }, { 1 } });
		actualPoints.Add(new double[3, 1] { { 2785 }, { 1189 }, { 1 } });

		CalculateError(hm2, Points, actualPoints);


		/// Image 3 
		Debug.Log("Image 3");
		actualPoints = new List<double[,]>();

		actualPoints.Add(new double[3, 1] { { 1437 }, { 729 }, { 1 } });
		actualPoints.Add(new double[3, 1] { { 1901 }, { 493 }, { 1 } });
		actualPoints.Add(new double[3, 1] { { 2589 }, { 1469 }, { 1 } });

		CalculateError(hm3, Points, actualPoints);

		/*PART 1.6*/
		double[,] p1 = new double[3, 1] { { 7.5f }, { 5.5f }, { 1 } };
		double[,] p2 = new double[3, 1] { { 6.3f }, { 3.3f }, { 1 } };
		double[,] p3 = new double[3, 1] { { 0.1f }, { 0.1f }, { 1 } };

		Debug.Log("Image 1");
		CalculateTarget(hm1, p1);
		CalculateTarget(hm1, p2);
		CalculateTarget(hm1, p3);
		Debug.Log("Image 2");
		CalculateTarget(hm2, p1);
		CalculateTarget(hm2, p2);
		CalculateTarget(hm2, p3);
		Debug.Log("Image 3");
		CalculateTarget(hm3, p1);
		CalculateTarget(hm3, p2);
		CalculateTarget(hm3, p3);

		/*PART 1.7*/
		double[,] i1 = new double[3, 1] { { 500 }, { 400 }, { 1 } };
		double[,] i2 = new double[3, 1] { { 86 }, { 167 }, { 1 } };
		double[,] i3 = new double[3, 1] { { 10 }, { 10 }, { 1 } };

		Debug.Log("Image 1");
		CalculateSource(hm1, i1);
		CalculateSource(hm1, i2);
		CalculateSource(hm1, i3);

		Debug.Log("Image 2");
		CalculateSource(hm2, i1);
		CalculateSource(hm2, i2);
		CalculateSource(hm2, i3);

		Debug.Log("Image 3");
		CalculateSource(hm3, i1);
		CalculateSource(hm3, i2);
		CalculateSource(hm3, i3);

	}

	public void printMatrix(double[,] homographyMatrix)
    {
		string log = "";

		for (int i = 0; i < homographyMatrix.GetLength(0); i++)
		{
			for (int j = 0; j < homographyMatrix.GetLength(1); j++)
				log += homographyMatrix[i, j] + " ";
			log += "\n";
		}

		Debug.Log("Calculated Homography Matrix : \n\n" + log);
	}

	double[,] MatrixConverter(Matrix4x4 matrix)
    {
		double[,] temp =
		{
			{matrix.m00,matrix.m01,matrix.m02},
			{matrix.m10,matrix.m11,matrix.m12},
			{matrix.m20,matrix.m21,matrix.m22},
		};

		return temp;
	}

	public static Matrix4x4 FindHomography(Vector3[] fromCorners, Vector3[] toCorners)
	{
		double[][] arr = new double[2*fromCorners.Length][];

		for (int i = 0; i < fromCorners.Length;i++)
		{
			var p1 = fromCorners[i];
			var p2 = toCorners[i];
			arr[i * 2] = new double[] { -p1.x, -p1.y, -1, 0, 0, 0, p2.x * p1.x, p2.x * p1.y, p2.x };
			arr[i * 2 + 1] = new double[] { 0, 0, 0, -p1.x, -p1.y, -1, p2.y * p1.x, p2.y * p1.y, p2.y };
		}

		var svd = DenseMatrix.OfRowArrays(arr).Svd();
		var v = svd.VT.Transpose();

		// right singular vector
		var rsv = v.Column(v.ColumnCount - 1);

		// reshape to 3x3 matrix
		Matrix4x4 h = Matrix4x4.zero;
		h.SetRow(0, new Vector4((float)rsv[0], (float)rsv[1], (float)rsv[2], 0));
		h.SetRow(1, new Vector4((float)rsv[3], (float)rsv[4], (float)rsv[5], 0));
		h.SetRow(2, new Vector4((float)rsv[6], (float)rsv[7], (float)rsv[8], 0));

		return h;
	}
	public static double[,] CalculateTarget(double[,] matrixA, double[,] matrixB)
	{
		int rowsA = matrixA.GetLength(0);
		int columnsA = matrixA.GetLength(1);
		int columnsB = matrixB.GetLength(1);

		double[,] target = new double[rowsA,columnsA];
		for (int i = 0; i < rowsA; i++)
		{
			for (int j = 0; j < columnsB; j++)
			{
				for (int k = 0; k < columnsA; k++)
				{
					target[i,j] += matrixA[i, k] * matrixB[k, j];
				}
			}
		}
		target[0,0] = target[0,0] / target[2,0];
		target[1,0] = target[1,0] / target[2,0];

		Debug.Log("u = " + target[0,0] + "," + "v = " + target[1, 0]);
		double[,] res = new double[,] { { target[0, 0] }, { target[1, 0] },{1} };
		return res;
	}
	public static double[,] CalculateSource(double[,] matrixA, double[,] matrixB)
	{
		var temp = DenseMatrix.OfArray(matrixA).Inverse();
		matrixA = temp.ToArray();

		int rowsA = matrixA.GetLength(0);
		int columnsA = matrixA.GetLength(1);
		int columnsB = matrixB.GetLength(1);

		double[,] target = new double[rowsA, columnsA];
		for (int i = 0; i < rowsA; ++i)
		{
			for (int j = 0; j < columnsB; ++j)
			{
				for (int k = 0; k < columnsA; ++k)
				{
					target[i,j] += matrixA[i, k] * matrixB[k, j];
				}
			}

		}
		target[0,0] = target[0,0] / target[2,0];
		target[1,0] = target[1,0] / target[2,0];

		Debug.Log("x = " + target[0,0] + "," + "y = " + target[1, 0]);
		double[,] res = new double[,] { { target[0, 0] }, { target[1, 0] }, { 1 } };
		return res;
	}
	private void CalculateError(double[,] hMatix, List<double[,]> Points, List<double[,]> actualPoints)
	{
		printMatrix(hMatix);
		int k = 0;
		foreach (double[,] temp in Points)
		{
			double[,] uv = actualPoints[k++];
			var res = CalculateTarget(hMatix, temp);
			Debug.Log("Error : %" + CalcErrorPercentage(res, uv));
		}
	}

	// Percentage
	private float CalcErrorPercentage(double[,] targetResult, double[,] actualPoint)
	{
		var x1 = targetResult[0, 0];
		var x2 = actualPoint[0, 0];

		var y1 = targetResult[1, 0];
		var y2 = actualPoint[1, 0];

		float resDif = Mathf.Sqrt(Mathf.Pow((float)(x1), 2) + Mathf.Pow((float)(y1), 2));
		float actualDif = Mathf.Sqrt(Mathf.Pow((float)(x2), 2) + Mathf.Pow((float)(y2), 2));

		return Mathf.Abs((actualDif - resDif) / actualDif * 100);
	}
}