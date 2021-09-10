using Api.Common.Utilities;

using NUnit.Framework;

using System;

using NUnitAssert = NUnit.Framework.Assert;

namespace Test.CommonTests.Utilities
{
    public class UtilitiesStringExtensions
    {
        [SetUp]
        public void Setup()
        {
        }
        #region StringExtensions
        [Test]
        public void Utilities_StringExtensions_IsNullOrEmpty_Result_True()
        {
            //ARRENG
            var name = "name";

            //ACT
            var hasValue = name.HasValue();

            //ASSERT
            NUnitAssert.AreEqual(true, hasValue);
            NUnitAssert.IsTrue(hasValue);
        }

        [Test]
        public void Utilities_StringExtensions_IsNullOrEmpty_Result_False()
        {
            //ARRENG
            var name = "";

            //ACT
            var hasValue = name.HasValue();

            //ASSERT
            NUnitAssert.AreEqual(false, hasValue);
            NUnitAssert.IsFalse(hasValue);
        }

        [Test]
        public void Utilities_StringExtensions_NullOrWhiteSpace_Result_False()
        {
            //ARRENG
            var name = " ";

            //ACT
            var hasValue = name.HasValue();

            //ASSERT
            NUnitAssert.AreEqual(false, hasValue);
            NUnitAssert.IsFalse(hasValue);
        }
        #endregion
        #region 

        [Test] 
        public void Utilities_ToInt_Result_is_Equal_Int()
        {
            //ARRENG
            var name = "1";

            //ACT
            var hasValue = name.ToInt();

            //ASSERT
            NUnitAssert.AreEqual(1, hasValue);
            NUnitAssert.GreaterOrEqual(1, hasValue);
        }
        #endregion

    }
}
