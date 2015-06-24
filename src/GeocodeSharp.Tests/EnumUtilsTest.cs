using System;
using GeocodeSharp.Google;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeocodeSharp.Tests
{
    [TestClass]
    public class EnumUtilsTest
    {
        [TestMethod]
        public void TestEnumUtils_GetValue()
        {
            GeocodeStatus gs =  GeocodeStatus.OverQueryLimit;
            var value = EnumUtils.GetValue(gs);
            Assert.AreEqual("OVER_QUERY_LIMIT",value);
        }

        [TestMethod]
        public void TestEnumUtils_GetValue_NoEnumMember()
        {
            GeocodeStatus gs = GeocodeStatus.Unexpected;
            var value = EnumUtils.GetValue(gs);
            Assert.AreEqual("Unexpected", value);
        }
    }
}
