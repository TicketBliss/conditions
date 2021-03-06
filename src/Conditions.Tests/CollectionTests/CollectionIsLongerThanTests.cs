using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conditions.Tests.CollectionTests
{
    /// <summary>
    /// Tests the ValidatorExtensions.IsLongerThan method.
    /// </summary>
    [TestClass]
    public class CollectionIsLongerThanTests
    {
        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        [Description("Calling IsLongerThan(1) with a collection containing no elements should fail.")]
        public void CollectionIsLongerThanTest01()
        {
            // HashSet only implements generic ICollection<T>, no ICollection.
            HashSet<int> set = new HashSet<int>();

            Condition.Requires(set).IsLongerThan(1);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        [Description("Calling IsLongerThan(0) with a collection containing no elements should fail.")]
        public void CollectionIsLongerThanTest02()
        {
            // HashSet only implements generic ICollection<T>, no ICollection.
            HashSet<int> set = new HashSet<int>();

            Condition.Requires(set).IsLongerThan(0);
        }

        [TestMethod]
        [Description("Calling IsLongerThan(-1) with a collection containing no elements should pass.")]
        public void CollectionIsLongerThanTest03()
        {
            // HashSet only implements generic ICollection<T>, no ICollection.
            HashSet<int> set = new HashSet<int>();

            Condition.Requires(set).IsLongerThan(-1);
        }

        [TestMethod]
        [Description("Calling IsLongerThan(1) with a collection containing two element should pass.")]
        public void CollectionIsLongerThanTest04()
        {
            // HashSet only implements generic ICollection<T>, no ICollection.
            HashSet<int> set = new HashSet<int> {1, 2};

            Condition.Requires(set).IsLongerThan(1);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        [Description("Calling IsLongerThan(2) with a collection containing two elements should fail.")]
        public void CollectionIsLongerThanTest05()
        {
            // HashSet only implements generic ICollection<T>, no ICollection.
            HashSet<int> set = new HashSet<int> {1, 2};

            Condition.Requires(set).IsLongerThan(2);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        [Description("Calling IsLongerThan(1) with an ArrayList containing one element should fail.")]
        public void CollectionIsLongerThanTest06()
        {
            // ArrayList implements ICollection.
            ArrayList list = new ArrayList {1};

            Condition.Requires(list).IsLongerThan(1);
        }

        [TestMethod]
        [Description("Calling IsLongerThan(0) with an ArrayList containing one element should pass.")]
        public void CollectionIsLongerThanTest07()
        {
            // ArrayList implements ICollection.
            ArrayList list = new ArrayList {1};

            Condition.Requires(list).IsLongerThan(0);
        }

        [TestMethod]
        [Description("Calling IsLongerThan(-1) on a null reference should pass.")]
        public void CollectionIsLongerThanTest08()
        {
            IEnumerable list = null;

            Condition.Requires(list).IsLongerThan(-1);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        [Description("Calling IsLongerThan(0) on a null reference should fail.")]
        public void CollectionIsLongerThanTest09()
        {
            IEnumerable list = null;

            Condition.Requires(list).IsLongerThan(0);
        }

        [TestMethod]
        [Description("Calling IsLongerThan with the condtionDescription parameter should pass.")]
        public void CollectionIsLongerThanTest10()
        {
            IEnumerable list = null;

            Condition.Requires(list).IsLongerThan(-1, string.Empty);
        }

        [TestMethod]
        [Description(
            "Calling a failing IsLongerThan should throw an Exception with an exception message that contains the given parameterized condition description argument."
            )]
        public void CollectionIsLongerThanTest11()
        {
            IEnumerable list = null;
            try
            {
                Condition.Requires(list, "list").IsLongerThan(1, "abc {0} def");
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("abc list def"));
            }
        }

        [TestMethod]
        [Description(
            "Calling IsLongerThan(1) with a collection containing no elements should succeed when exceptions are suppressed."
            )]
        public void CollectionIsLongerThanTest12()
        {
            // HashSet only implements generic ICollection<T>, no ICollection.
            HashSet<int> set = new HashSet<int>();

            Condition.Requires(set).SuppressExceptionsForTest().IsLongerThan(1);
        }
    }
}