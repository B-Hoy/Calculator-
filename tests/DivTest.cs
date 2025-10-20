using NUnit.Framework;
using Calculator_.src;
using System.Windows.Controls;

namespace Calculator_.Tests
{
    [TestFixture]
    public class DivideOp_DivByZero
    {
        private Divide d;

        [SetUp]
        public void SetUp()
        {
            d = new (new Button());
        }

        [Test]
        public void IsZeroDivWorking()
        {
            var result = d.Evaluate([1, 0]);

            Assert.That(result == 0, "This is going to break if we manage to get here!");
        }
    }
}