using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rrs.Expandos;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Tests.Rrs.Expandos
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestConverter()
        {
            var o = new
            {
                a = 1,
                b = "two"
            };
            dynamic e = ExpandoConverter.Convert(o);

            Assert.AreEqual(1, e.a);
            Assert.AreEqual("two", e.b);
        }

        [TestMethod]
        public void TestToExpando()
        {
            var o = new
            {
                a = 1,
                b = "two"
            };
            dynamic e = o.ToExpando();

            Assert.AreEqual(1, e.a);
            Assert.AreEqual("two", e.b);
        }

        [TestMethod]
        public void TestMergeExpandos()
        {
            var a = new ExpandoObject();
            ((dynamic)a).a = 1;

            var b = new ExpandoObject();
            ((dynamic)b).b = "two";

            dynamic e = a.Merge(b);

            Assert.AreEqual(1, e.a);
            Assert.AreEqual("two", e.b);
        }

        [TestMethod]
        public void TestMergeObjects()
        {
            var a = new
            {
                a = 1,
            };
            var b = new
            {
                b = "two"
            };
            dynamic e = a.Merge(b);

            Assert.AreEqual(1, e.a);
            Assert.AreEqual("two", e.b);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMergeExpandosThrows()
        {
            var a = new
            {
                a = 1,
            };
            var b = new
            {
                a = 2,
                b = "two"
            };
            a.Merge(b);
        }

        [TestMethod]
        public void TestExcept()
        {
            var a = new ExpandoObject();
            dynamic da = a;
            da.a = 1;
            da.b = "two";

            IDictionary<string, object> e = a.Except("b");

            Assert.AreEqual(1, e["a"]);
            Assert.IsFalse(e.ContainsKey("b"));
        }
    }
}
