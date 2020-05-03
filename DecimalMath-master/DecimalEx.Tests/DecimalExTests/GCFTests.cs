using System.Collections;
using DecimalMath;
using NUnit.Framework;

namespace DecimalExTests.DecimalExTests
{

    public class GCFTests
    {
        // For verification, can use: https://www.omnicalculator.com/math/gcf
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(1.2m, 0.42m).Returns(.06m);
                yield return new TestCaseData(1071m, 462m).Returns(21m);
                yield return new TestCaseData(decimal.MaxValue / 1000m, .28m).Returns(.035m);
                yield return new TestCaseData(0.0000000000000000000000823543m, 0.0000000000019626617431640625m).Returns(0.0000000000000000000000000343m);
            }
        }

        [TestCaseSource(nameof(TestCases))]
        public decimal Test(decimal a, decimal b)
        {
            return DecimalEx.GCF(a, b);
        }

        public static IEnumerable TestMultipleCases
        {
            get
            {
                yield return new TestCaseData(20m, 50m, new[]{120m}).Returns(10m);
                yield return new TestCaseData(20m, 50m, new[]{120m, 35m}).Returns(5m);
                yield return new TestCaseData(20m, 50m, new[]{120m, 35m, 49m}).Returns(1m);
            }
        }

        [TestCaseSource("TestMultipleCases")]
        public decimal TestMultiple(decimal a, decimal b, params decimal[] values)
        {
            return DecimalEx.GCF(a, b, values);
        }
    }

}