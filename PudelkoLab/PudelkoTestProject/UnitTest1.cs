using Microsoft.VisualStudio.TestTools.UnitTesting;
using PudelkoLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading;

namespace PudelkoUnitTests
{

    [TestClass]
    public static class InitializeCulture
    {
        [AssemblyInitialize]
        public static void SetEnglishCultureOnAllUnitTest(TestContext context)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }

    // ========================================

    [TestClass]
    public class UnitTestsPudelkoConstructors
    {
        private static double defaultSize = 0.1; // w metrach
        private static double accuracy = 0.001; //dokładność 3 miejsca po przecinku

        private void AssertPudelko(Pudelko p, double expectedA, double expectedB, double expectedC)
        {
            Assert.AreEqual(expectedA, p.A, delta: accuracy);
            Assert.AreEqual(expectedB, p.B, delta: accuracy);
            Assert.AreEqual(expectedC, p.C, delta: accuracy);
        }

        #region Constructor tests ================================

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Pudelko p = new Pudelko();

            Assert.AreEqual(defaultSize, p.A, delta: accuracy);
            Assert.AreEqual(defaultSize, p.B, delta: accuracy);
            Assert.AreEqual(defaultSize, p.C, delta: accuracy);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
        public void Constructor_3params_DefaultMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
        public void Constructor_3params_InMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100.0, 25.5, 3.1,
                 1.0, 0.255, 0.031)]
        [DataRow(100.0, 25.58, 3.13,
                 1.0, 0.255, 0.031)] // dla centymertów liczy się tylko 1 miejsce po przecinku
        public void Constructor_3params_InCentimeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a: a, b: b, c: c, unit: UnitOfMeasure.centimeter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100, 255, 3,
                 0.1, 0.255, 0.003)]
        [DataRow(100.0, 25.58, 3.13,
                 0.1, 0.025, 0.003)] // dla milimetrów nie liczą się miejsca po przecinku
        public void Constructor_3params_InMilimeters(double a, double b, double c,
                                                     double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a, b: b, c: c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }


        // ----

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_DefaultMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a, b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_InMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a: a, b: b, unit: UnitOfMeasure.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 2.5, 0.11, 0.025)]
        [DataRow(100.1, 2.599, 1.001, 0.025)]
        [DataRow(2.0019, 0.25999, 0.02, 0.002)]
        public void Constructor_2params_InCentimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.centimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 2.0, 0.011, 0.002)]
        [DataRow(100.1, 2599, 0.1, 2.599)]
        [DataRow(200.19, 2.5999, 0.2, 0.002)]
        public void Constructor_2params_InMilimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        // -------

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_DefaultMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_InMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 0.11)]
        [DataRow(100.1, 1.001)]
        [DataRow(2.0019, 0.02)]
        public void Constructor_1param_InCentimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.centimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 0.011)]
        [DataRow(100.1, 0.1)]
        [DataRow(200.19, 0.2)]
        public void Constructor_1param_InMilimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        // ---

        public static IEnumerable<object[]> DataSet1Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5, 3.1},
            new object[] {1.0, -2.5, 3.1},
            new object[] {1.0, 2.5, -3.1},
            new object[] {-1.0, -2.5, 3.1},
            new object[] {-1.0, 2.5, -3.1},
            new object[] {1.0, -2.5, -3.1},
            new object[] {-1.0, -2.5, -3.1},
            new object[] {0, 2.5, 3.1},
            new object[] {1.0, 0, 3.1},
            new object[] {1.0, 2.5, 0},
            new object[] {1.0, 0, 0},
            new object[] {0, 2.5, 0},
            new object[] {0, 0, 3.1},
            new object[] {0, 0, 0},
            new object[] {10.1, 2.5, 3.1},
            new object[] {10, 10.1, 3.1},
            new object[] {10, 10, 10.1},
            new object[] {10.1, 10.1, 3.1},
            new object[] {10.1, 10, 10.1},
            new object[] {10, 10.1, 10.1},
            new object[] {10.1, 10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_DefaultMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.01, 0.1, 1)]
        [DataRow(0.1, 0.01, 1)]
        [DataRow(0.1, 0.1, 0.01)]
        [DataRow(1001, 1, 1)]
        [DataRow(1, 1001, 1)]
        [DataRow(1, 1, 1001)]
        [DataRow(1001, 1, 1001)]
        [DataRow(1, 1001, 1001)]
        [DataRow(1001, 1001, 1)]
        [DataRow(1001, 1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InCentimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.centimeter);
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.1, 1, 1)]
        [DataRow(1, 0.1, 1)]
        [DataRow(1, 1, 0.1)]
        [DataRow(10001, 1, 1)]
        [DataRow(1, 10001, 1)]
        [DataRow(1, 1, 10001)]
        [DataRow(10001, 10001, 1)]
        [DataRow(10001, 1, 10001)]
        [DataRow(1, 10001, 10001)]
        [DataRow(10001, 10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMiliimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.milimeter);
        }


        public static IEnumerable<object[]> DataSet2Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5},
            new object[] {1.0, -2.5},
            new object[] {-1.0, -2.5},
            new object[] {0, 2.5},
            new object[] {1.0, 0},
            new object[] {0, 0},
            new object[] {10.1, 10},
            new object[] {10, 10.1},
            new object[] {10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_DefaultMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.01, 1)]
        [DataRow(1, 0.01)]
        [DataRow(0.01, 0.01)]
        [DataRow(1001, 1)]
        [DataRow(1, 1001)]
        [DataRow(1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InCentimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.1, 1)]
        [DataRow(1, 0.1)]
        [DataRow(0.1, 0.1)]
        [DataRow(10001, 1)]
        [DataRow(1, 10001)]
        [DataRow(10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMilimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.milimeter);
        }




        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_DefaultMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(0.01)]
        [DataRow(1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InCentimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(0.1)]
        [DataRow(10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMilimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.milimeter);
        }

        #endregion


        #region ToString tests ===================================

        [TestMethod, TestCategory("String representation")]
        public void ToString_Default_Culture_EN()
        {
            var p = new Pudelko(2.5, 9.321);
            string expectedStringEN = "2.500 m × 9.321 m × 0.100 m";

            Assert.AreEqual(expectedStringEN, p.ToString());
        }

        [DataTestMethod, TestCategory("String representation")]
        [DataRow(null, 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("m", 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("cm", 2.5, 9.321, 0.1, "250.0 cm × 932.1 cm × 10.0 cm")]
        [DataRow("mm", 2.5, 9.321, 0.1, "2500 mm × 9321 mm × 100 mm")]
        public void ToString_Formattable_Culture_EN(string format, double a, double b, double c, string expectedStringRepresentation)
        {
            var p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Assert.AreEqual(expectedStringRepresentation, p.ToString(format));
        }

        [TestMethod, TestCategory("String representation")]
        [ExpectedException(typeof(FormatException))]
        public void ToString_Formattable_WrongFormat_FormatException()
        {
            var p = new Pudelko(1);
            var stringformatedrepreentation = p.ToString("wrong code");
        }

        #endregion


        #region Pole, Objętość ===================================
        [TestMethod, TestCategory("Pole, objetosc")]
        [DataRow(null, 1, 5, 4, 20)]
        [DataRow(UnitOfMeasure.milimeter, 2000, 2000, 2000, 8)]
        [DataRow(UnitOfMeasure.milimeter, 2130, 7053, 9999, 150.21387711)]
        [DataRow(UnitOfMeasure.centimeter, 12.5, 54.3, 426.4, 0.289419)]
        [DataRow(UnitOfMeasure.meter, 4, 5.32, 9.4, 200.032)]
        public void ObjetoscTest(UnitOfMeasure format, double a, double b, double c, double expectedObjetosc)
        {
            var p = new Pudelko(a, b, c, format);
            Assert.AreEqual(expectedObjetosc, p.Objetosc);
        }
        [TestMethod, TestCategory("Pole, objetosc")]
        [DataRow(null, 1, 5, 4, 58)]
        [DataRow(UnitOfMeasure.milimeter, 2000, 2000, 2000, 24)]
        [DataRow(UnitOfMeasure.centimeter, 12.5, 54.3, 426.4, 5.832454)]
        [DataRow(UnitOfMeasure.meter, 4, 5.32, 9.4, 217.77600)]

        public void PoleTest(UnitOfMeasure format, double a, double b, double c, double expectedPole)
        {
            var p = new Pudelko(a, b, c, format);
            Assert.AreEqual(expectedPole, p.Pole);
        }
        [TestMethod, TestCategory("LaczeniePudelek")]
        [DataRow(null, 1, 5, 4, null, 1,1,1, null, 2,5,4)]
        [DataRow(UnitOfMeasure.milimeter, 2000, 2000, 2000, UnitOfMeasure.centimeter, 20, 100, 52, null, 2.2, 2, 2)]
        [DataRow(UnitOfMeasure.centimeter, 12, 54.3, 426, UnitOfMeasure.meter, 4, 4, 0.4, UnitOfMeasure.meter, 4.12, 4, 4.26)]
        [DataRow(UnitOfMeasure.meter, 4, 5.32, 9.4, UnitOfMeasure.centimeter, 300, 200, 59, null, 7, 5.32, 9.4)]
        public void LaczenieTest(UnitOfMeasure format, double a, double b, double c, UnitOfMeasure format2, double a2, double b2, double c2, UnitOfMeasure format3, double a3, double b3, double c3)
        {
            var p1 = new Pudelko(a, b, c, format);
            var p2 = new Pudelko(a2, b2, c2, format2);
            var expected = new Pudelko(a3, b3, c3, format3 );
            p1 = p1 + p2;
            Assert.AreEqual<Pudelko>(p1, expected);
        }


        #endregion

        #region Equals ===========================================
        [TestMethod, TestCategory("EqualsTest")]
        [DataRow(null, 1, 1, 1, null, 1, 1, 1)]
        [DataRow(UnitOfMeasure.centimeter, 1, 1, 1, UnitOfMeasure.centimeter, 1, 1, 1)]
        [DataRow(UnitOfMeasure.milimeter, 2000, 2000, 2000, UnitOfMeasure.meter, 2, 2, 2)]
        [DataRow(UnitOfMeasure.milimeter, 1234, 4567, 7890, UnitOfMeasure.meter, 1.234, 4.567, 7.890)]
        public void EqualsTestTrue(UnitOfMeasure format, double a, double b, double c, UnitOfMeasure format2, double a2, double b2, double c2)
        {
            var p1 = new Pudelko(a, b, c, format);
            var p2 = new Pudelko(a2, b2, c2, format2);
            Assert.IsTrue(p1.Equals(p2));
        }
        [TestMethod, TestCategory("EqualsTest")]
        [DataRow(null, 1, 1, 1, null, 1, 4, 1)]
        [DataRow(UnitOfMeasure.centimeter, 2, 1, 1, UnitOfMeasure.centimeter, 1, 1, 1)]
        [DataRow(UnitOfMeasure.milimeter, 2600, 2000, 2000, UnitOfMeasure.meter, 2, 2, 2)]
        [DataRow(UnitOfMeasure.milimeter, 1254, 4567, 7890, UnitOfMeasure.meter, 1.234, 4.567, 7.890)]
        public void EqualsTestFalse(UnitOfMeasure format, double a, double b, double c, UnitOfMeasure format2, double a2, double b2, double c2)
        {
            var p1 = new Pudelko(a, b, c, format);
            var p2 = new Pudelko(a2, b2, c2, format2);
            Assert.IsFalse(p1.Equals(p2));
        }



        #endregion

        #region Operators overloading ===========================
        // ToDo
        #endregion

        #region Conversions =====================================
        [TestMethod]
        public void ExplicitConversion_ToDoubleArray_AsMeters()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            double[] tab = (double[])p;
            Assert.AreEqual(3, tab.Length);
            Assert.AreEqual(p.A, tab[0]);
            Assert.AreEqual(p.B, tab[1]);
            Assert.AreEqual(p.C, tab[2]);
        }
         
        [TestMethod]
        public void ImplicitConversion_FromAalueTuple_As_Pudelko_InMilimeters()
        {
            var (a, b, c) = (2500, 9321, 100); // in milimeters, ValueTuple
            Pudelko p = (a, b, c);
            Assert.AreEqual((int)(p.A * 1000), a);
            Assert.AreEqual((int)(p.B * 1000), b);
            Assert.AreEqual((int)(p.C * 1000), c);
        }

        #endregion

        #region Indexer, enumeration ============================
        [TestMethod]
        public void Indexer_ReadFrom()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            Assert.AreEqual(p.A, p[0]);
            Assert.AreEqual(p.B, p[1]);
            Assert.AreEqual(p.C, p[2]);
        }

        [TestMethod]
        public void ForEach_Test()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            var tab = new[] { p.A, p.B, p.C };
            int i = 0;
            foreach (double x in p)
            {
                Assert.AreEqual(x, tab[i]);
                i++;
            }
        }

        #endregion

        #region Parsing =========================================

        #endregion

    }
}