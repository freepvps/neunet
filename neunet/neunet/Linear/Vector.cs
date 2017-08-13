using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Neunet.Linear
{
    public class Vector : ILinearObject<Vector>, ILinearMultiplicative<Vector, double>, ILinearAdditive<double, Vector>, ILinearMultiplicative<double, Vector>
    {
        private double[] Values;
        public int Dimension { get => Values.Length; }

        public Vector(int dimension)
        {
            Values = new double[dimension];
        }
        public Vector(double[] values) : this(values.Length)
        {
            Buffer.BlockCopy(values, 0, Values, 0, Values.Length * sizeof(double));
        }
        public Vector(Vector vector) : this(vector.Values)
        {

        }


        public static implicit operator Vector(double[] input) => new Vector(input);

        public double this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }

        private void CheckDim(Vector v)
        {
            if (v.Dimension != Dimension)
            {
                throw new ArgumentOutOfRangeException("Dimension check failed");
            }
        }

        public Vector Add(Vector value)
        {
            CheckDim(value);

            var result = new Vector(this);
            for (var i = 0; i < result.Dimension; i++)
            {
                result[i] += value[i];
            }
            return result;
        }

        public Vector Product(Vector value)
        {
            CheckDim(value);

            var result = new Vector(this);
            for (var i = 0; i < Dimension; i++)
            {
                result[i] *= value[i];
            }
            return result;
        }

        public double ScalarProduct(Vector value)
        {
            CheckDim(value);

            var result = 0.0;
            for (var i = 0; i < Dimension; i++)
            {
                result += value[i] * this[i];
            }
            return result;
        }

        double ILinearMultiplicative<Vector, double>.Product(Vector input)
        {
            return ScalarProduct(input);
        }

        public Vector Add(double value)
        {
            var result = new Vector(this);
            for (var i = 0; i < result.Dimension; i++)
            {
                result[i] += value;
            }
            return result;
        }

        public Vector Product(double value)
        {
            var result = new Vector(this);
            for (var i = 0; i < result.Dimension; i++)
            {
                result[i] *= value;
            }
            return result;
        }

        public Vector Map(Func<double, double> func)
        {
            var result = new Vector(this);
            for (var i = 0; i < result.Dimension; i++)
            {
                result[i] = func(result[i]);
            }
            return result;
        }

        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)Values).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        public override string ToString()
        {
            return $"[ {string.Join(", ", Values)} ]";
        }
    }
}
