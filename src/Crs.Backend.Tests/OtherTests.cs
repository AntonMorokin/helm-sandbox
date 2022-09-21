using NUnit.Framework;
using System;

namespace Crs.Backend.Tests
{
    public class OtherTests
    {
        [Test]
        public void SomeFlackyTest()
        {
            if (new Random().NextDouble() < 0.2)
            {
                Assert.Fail("Today is not your day, sorry.");
            }
        }
    }
}
