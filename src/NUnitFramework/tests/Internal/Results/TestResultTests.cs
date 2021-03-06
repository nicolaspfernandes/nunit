// ***********************************************************************
// Copyright (c) 2010-2016 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Interfaces;
using NUnit.TestUtilities;

namespace NUnit.Framework.Internal.Results
{
    /// <summary>
    /// Abstract base for tests of TestResult
    /// </summary>
    [TestFixture]
    public abstract class TestResultTests
    {
        protected const string NonWhitespaceIgnoreReason = "because";
        protected TestMethod _test;
        protected TestResult _testResult;

        protected TestSuite _suite;
        protected TestSuiteResult _suiteResult;

        [SetUp]
        public void SetUp()
        {
            _test = new TestMethod(new MethodWrapper(typeof(DummySuite), "DummyMethod"));
            _testResult = _test.MakeTestResult();

            _suite = new TestSuite(typeof(DummySuite));
            _suiteResult = (TestSuiteResult)_suite.MakeTestResult();
        }

        protected static void ReasonNodeExpectedValidation(TNode testNode, string reasonMessage)
        {
            TNode reason = testNode.SelectSingleNode("reason");
            Assert.NotNull(reason);
            Assert.NotNull(reason.SelectSingleNode("message"));
            Assert.AreEqual(reasonMessage, reason.SelectSingleNode("message").Value);
            Assert.Null(reason.SelectSingleNode("stack-trace"));
        }

        protected static void NoReasonNodeExpectedValidation(TNode testNode)
        {
            TNode reason = testNode.SelectSingleNode("reason");
            Assert.IsNull(reason, "This test expects no reason element to be present in the xml representation.");
        }

        #region Nested DummySuite

        public class DummySuite
        {
            public void DummyMethod() { }
        }

        #endregion
    }
 }
