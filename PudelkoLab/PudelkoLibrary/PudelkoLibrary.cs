

namespace PudelkoLibrary
    
{
    public class Pudelko
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        
        public UnitOfMeasure Unit{ get; set; }
        public Pudelko(double a, double b = 10, double c = 10, UnitOfMeasure unit = UnitOfMeasure.meter) 
        {
            
            if ((unit == UnitOfMeasure.meter && (a > 10 || b > 10 || c > 10)) || (unit == UnitOfMeasure.centimeter && (a>1000 || b>1000 || c>1000)) || (unit == UnitOfMeasure.milimeter && (a>10000 || b>10000 || c>10000)) || a<0 || b<0 || c<0)
            {
                throw new ArgumentOutOfRangeException();
            }
            A = a; B = b; C = c; Unit = unit;

        }
        public Pudelko() 
        {
            A = 10;
            B = 10;
            C = 10;
            Unit = UnitOfMeasure.centimeter;
        }
        public override string ToString()
        {
            return $"{A} {Unit} × {B} {Unit} × {C} {Unit}";
        }
        public static explicit operator double[](Pudelko pudelko)
        {
            return new double[] { pudelko.A, pudelko.B, pudelko.C };
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
            UnitOfMeasure unitFormat = alias(format);

            double aConverted = ConvertToUnit(A, unitFormat);
            double bConverted = ConvertToUnit(B, unitFormat);
            double cConverted = ConvertToUnit(C, unitFormat);
            switch (unitFormat)
            {
                case UnitOfMeasure.meter:
                    return $"{aConverted:0.000} {format} × {bConverted:0.000} {format} × {cConverted:0.000} {format}";
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
                    if (Unit == UnitOfMeasure.meter) { return value / 100; }
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
    }
}
