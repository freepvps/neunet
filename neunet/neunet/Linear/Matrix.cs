using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Neunet.Linear
{
    public class Matrix :
        ILinearObject<Matrix>,
        ILinearAdditive<double, Matrix>, 
        ILinearMultiplicative<double, Matrix>, 
        ILinearMultiplicative<Vector, Vector>, 
        IFlatten<Vector>,
        IMap<Matrix, Vector>,
        IMap<Matrix, Matrix>
    {
        private Vector[] Vectors { get; }
        public int Height { get => Vectors.Length; }
        public int Width { get => Vectors.Length == 0 ? 0 : Vectors[0].Dimension; }

        public Matrix(int height, int width)
        {
            Vectors = new Vector[height];
            for (var i = 0; i < height; i++)
            {
                Vectors[i] = new Vector(width);
            }
        }
        public Matrix(Vector vector) : this(vector.Dimension, 1)
        {
            for (var i = 0; i < vector.Dimension; i++)
            {
                Vectors[i][0] = vector[i];
            }
        }
        public Matrix(double[][] matrix) : this(matrix.Length, matrix.Length == 0 ? 0 : matrix[0].Length)
        {
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    this[i, j] = matrix[i][j];
                }
            }
        }
        public Matrix(Vector[] vectors) : this(vectors.Length, vectors.Length == 0 ? 0 : vectors[0].Dimension)
        {
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    this[i, j] = vectors[i][j];
                }
            }
        }
        public Matrix(double[,] matrix) : this(matrix.GetLength(0), matrix.GetLength(1))
        {
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    this[i, j] = matrix[i, j];
                }
            }
        }
        public Matrix(Matrix matrix) : this(matrix.Vectors)
        {
        }

        public static implicit operator Matrix(Vector[] input) => new Matrix(input);
        public static implicit operator Matrix(Vector input) => new Matrix(input);
        public static implicit operator Matrix(double[][] input) => new Matrix(input);
        public static implicit operator Matrix(double[,] input) => new Matrix(input);

        public Vector this[int i]
        {
            get => Vectors[i];
            set
            {
                if (value.Dimension != Vectors[i].Dimension)
                {
                    throw new ArgumentOutOfRangeException("Dimension check failed");
                }
                for (var j = 0; j < value.Dimension; j++)
                {
                    Vectors[i][j] = value[j];
                }
            }
        }
        public double this[int i, int j]
        {
            get => Vectors[i][j];
            set => Vectors[i][j] = value;
        }

        public Matrix Add(Matrix value)
        {
            if (Height != value.Height || Width != value.Width)
            {
                throw new ArgumentOutOfRangeException("Check height or width failed");
            }
            var res = new Matrix(this);
            for (var i = 0; i < res.Height; i++)
            {
                for (var j = 0; j < res.Width; j++)
                {
                    res[i, j] += value[i, j];
                }
            }
            return res;
        }

        public Matrix Product(Matrix value)
        {
            if (Width != value.Height)
            {
                throw new ArgumentOutOfRangeException("Dimension check failed");
            }
            var result = new Matrix(Height, value.Width);
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < value.Width; j++)
                {
                    for (var t = 0; t < Width; t++)
                    {
                        result[i, j] += this[i, t] * value[t, j];
                    }
                }
            }
            return result;
        }

        public Matrix Add(double value)
        {
            var result = new Matrix(this);
            for (var i = 0; i < result.Height; i++)
            {
                for (var j = 0; j < result.Width; j++)
                {
                    result[i, j] += value;
                }
            }
            return result;
        }

        public Matrix Product(double value)
        {
            var result = new Matrix(this);
            for (var i = 0; i < result.Height; i++)
            {
                for (var j = 0; j < result.Width; j++)
                {
                    result[i, j] *= value;
                }
            }
            return result;
        }

        public Vector Product(Vector value)
        {
            if (value.Dimension != Width)
            {
                throw new ArgumentOutOfRangeException("Matrix * Vector dimension check failed");
            }
            var res = new Vector(Height);
            for (var i = 0; i < Height; i++)
            {
                res[i] = value.ScalarProduct(this[i]);
            }
            return res;
        }

        public Matrix Map(Func<double, double> func)
        {
            var result = new Matrix(this);
            for (var i = 0; i < result.Height; i++)
            {
                for (var j = 0; j < result.Width; j++)
                {
                    result[i, j] = func(result[i, j]);
                }
            }
            return result;
        }

        public IEnumerator<double> GetEnumerator()
        {
            return Vectors.SelectMany(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join<Vector>(Environment.NewLine, Vectors);
        }

        public Vector Flatten()
        {
            var result = new Vector(Height * Width);
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    result[i * Width + j] = this[i, j];
                }
            }
            return result;
        }

        public Matrix Map(Func<Vector, Vector> func)
        {
            return new Matrix(Vectors.Select(func).ToArray());
        }

        public Matrix Map(Func<Matrix, Matrix> func)
        {
            return func(this);
        }
    }
}
