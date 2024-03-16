

namespace PudelkoLibrary
    
{
    public class Pudelko : IEquatable<Pudelko>
    {
        
        private readonly double[] dimensions = new double[3];

        public double A => dimensions[0];
        public double B => dimensions[1];
        public double C => dimensions[2];
        public double Objetosc => Math.Round(A*B*C, 9);
        public double Pole => Math.Round((A * B*2)+(A*C*2)+(B*C*2),6);
        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= dimensions.Length)
                    throw new IndexOutOfRangeException("Index is out of range.");
                return dimensions[index];
            }
            set
            {
                if (index < 0 || index >= dimensions.Length)
                    throw new IndexOutOfRangeException("Index is out of range.");
                dimensions[index] = value;
            }
        }

        public UnitOfMeasure Unit{ get; set; }
        public Pudelko(double? a=null , double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter) 
        {
            if ((unit == UnitOfMeasure.meter && (a > 10 || b > 10 || c > 10)) || (unit == UnitOfMeasure.centimeter && (a>1000 || b>1000 || c>1000) || (a<0.1 || b<0.1 || c<0.1)) || (unit == UnitOfMeasure.milimeter && (a>10000 || b>10000 || c>10000)) || a<=0 || b<=0 || c<=0 || (unit == UnitOfMeasure.milimeter && (a<1 || b<1 || c<1)))
                        {
                            throw new ArgumentOutOfRangeException();
                        }

            Unit = unit;
                        if (unit == UnitOfMeasure.meter)
                        {
                            if (!a.HasValue)
                            { dimensions[0] = 0.1; }
                            else { dimensions[0] = a.Value; }
                            if (!b.HasValue)
                            { dimensions[1] = 0.1; }
                            else { dimensions[1] = b.Value; }
                            if (!c.HasValue)
                            { dimensions[2] = 0.1; }
                            else { dimensions[2] = c.Value; }

                        }
                        else if (unit == UnitOfMeasure.centimeter)
                        {
                            if (!a.HasValue)
                            { dimensions[0] = 0.1; }
                            else { dimensions[0] = a.Value/100; }
                            if (!b.HasValue)
                            { dimensions[1] = 0.1; }
                            else { dimensions[1] = b.Value / 100; }
                            if (!c.HasValue)
                            { dimensions[2] = 0.1; }
                            else { dimensions[2] = c.Value/100; }
                            Unit = UnitOfMeasure.meter;
                        }
                        else if (unit == UnitOfMeasure.milimeter)
                        {
                            if (!a.HasValue)
                            { dimensions[0] = 0.1; }
                            else { dimensions[0] = a.Value / 1000; }
                            if (!b.HasValue)
                            { dimensions[1] = 0.1; }
                            else { dimensions[1] = b.Value / 1000; }
                            if (!c.HasValue)
                            { dimensions[2] = 0.1; }
                            else { dimensions[2] = c.Value / 1000; }
                            Unit = UnitOfMeasure.meter;
                        }

        }


        public Pudelko((int a, int b, int c) dimensionss)
        {

            dimensions[0]= dimensionss.a; dimensions[1]= dimensionss.b; dimensions[2]= dimensionss.c;
        Unit = UnitOfMeasure.meter;
        }
        public override string ToString()
        {
            return ToString("m");
        }
        public static explicit operator double[](Pudelko pudelko)
        {
            return new double[] { pudelko.A, pudelko.B, pudelko.C };
        }
        public static implicit operator Pudelko((int a, int b, int c) dimensions)
        {
            return new Pudelko(dimensions.a, dimensions.b, dimensions.c, UnitOfMeasure.milimeter);
        }
        /*        public bool Equals(Pudelko b, Pudelko d)
                {
                    Pudelko pudelko1 = new Pudelko(b.A, b.B, b.C, b.Unit);
                    Pudelko pudelko2 = new Pudelko(d.A, d.B, d.C, d.Unit);
                    if (pudelko1.A==pudelko2.A && pudelko1.B==pudelko2.B && pudelko1.C==pudelko2.C)
                        { return true; }
                    else { return false; }
                }*/
        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, C, Unit);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Pudelko);
        }
        public bool Equals(Pudelko? other)
        {
            if (other == null)
                return false;

            if (this.A == other.A && this.B == other.B && this.C == other.C)
                return true;
            else
                return false;
        }



        public static UnitOfMeasure alias(string value)
        {
            switch (value)
            {
                case "m": return UnitOfMeasure.meter;
                case "cm": return UnitOfMeasure.centimeter;
                case "mm": return UnitOfMeasure.milimeter;
                default: throw new FormatException();
            }
        }
        
        public string ToString(string format)
        {
            if (format == null) {format = "m";}
            UnitOfMeasure unitFormat = alias(format);

            double aConverted = ConvertToUnit(A, unitFormat);
            double bConverted = ConvertToUnit(B, unitFormat);
            double cConverted = ConvertToUnit(C, unitFormat);
            switch (unitFormat)
            {
                case UnitOfMeasure.meter:
                    return $"{aConverted:0.000} m × {bConverted:0.000} m × {cConverted:0.000} m";
                case UnitOfMeasure.centimeter:
                    return $"{aConverted:0.0} {format} × {bConverted:0.0} {format} × {cConverted:0.0} {format}";
                case UnitOfMeasure.milimeter:
                    return $"{aConverted} {format} × {bConverted} {format} × {cConverted} {format}";
                default:
                    throw new ArgumentException("Invalid unit format.");
            }
        }

            private double ConvertToUnit(double value, UnitOfMeasure unitFormat)
        {
            switch (unitFormat)
            {
                case UnitOfMeasure.meter:
                    return value;
                case UnitOfMeasure.centimeter:
                    return value*100 ;
                case UnitOfMeasure.milimeter:
                    return value*1000;
                default:
                    throw new ArgumentException("Invalid unit format.");
            }
        }
       
        
        public IEnumerator<double> GetEnumerator()
        {
            foreach (var dimension in dimensions)
            {
                yield return dimension;
            }
        }
    }
}
