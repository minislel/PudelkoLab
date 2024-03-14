

namespace PudelkoLibrary
    
{
    public class Pudelko
    {
        private double[] dimensions = new double[3]; 

        public double A { get { return dimensions[0]; } set { dimensions[0] = value; } }
        public double B { get { return dimensions[1]; } set { dimensions[1] = value; } }
        public double C { get { return dimensions[2]; } set { dimensions[2] = value; } }
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
        public Pudelko(double a=0.1, double b = 0.1, double c = 0.1, UnitOfMeasure unit = UnitOfMeasure.meter) 
        {
            
            if ((unit == UnitOfMeasure.meter && (a > 10 || b > 10 || c > 10)) || (unit == UnitOfMeasure.centimeter && (a>1000 || b>1000 || c>1000) || (a<0.1 || b<0.1 || c<0.1)) || (unit == UnitOfMeasure.milimeter && (a>10000 || b>10000 || c>10000)) || a<=0 || b<=0 || c<=0 || (unit == UnitOfMeasure.milimeter && (a<1 || b<1 || c<1)))
            {
                throw new ArgumentOutOfRangeException();
            }
            if (unit == UnitOfMeasure.meter)
            {
                A = a; B = b; C = c; Unit = unit;
            }
            else if (unit == UnitOfMeasure.centimeter)
            {
                A = a / 100; B = b / 100; C = c / 100; Unit = UnitOfMeasure.meter;
            }
            else if (unit == UnitOfMeasure.milimeter)
            { 
                A= a / 1000; B = b / 1000; C = c / 1000; Unit = UnitOfMeasure.meter;
            }

        }
        public Pudelko((int a, int b, int c) dimensions)
        { 
        A= dimensions.a; B= dimensions.b; C= dimensions.c;
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
                    if (Unit == UnitOfMeasure.milimeter)
                        { return value / 1000; }
                        else if (Unit == UnitOfMeasure.centimeter)

                        { return value / 100; }
                    else return value;
                case UnitOfMeasure.centimeter:
                    if (Unit == UnitOfMeasure.meter) { return value * 100; }
                    else if (Unit == UnitOfMeasure.milimeter) { return value * 10; }
                    else return value ;
                case UnitOfMeasure.milimeter:
                    if (Unit == UnitOfMeasure.meter) { return value * 1000; }
                    else if (Unit == UnitOfMeasure.centimeter) { return value * 10; }
                    else return value;
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
