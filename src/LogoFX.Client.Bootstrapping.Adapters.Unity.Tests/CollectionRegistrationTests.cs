using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LogoFX.Client.Bootstrapping.Adapters.Unity.Tests
{
    [TestFixture]
    class CollectionRegistrationTests
    {
        [Test]
        public void MultipleImplementationAreRegistered_ResolvedCollectionContainsAllImplementations()
        {        
            var adapter = new UnityContainerAdapter();
            adapter.RegisterCollection<ITestDependency>(new[] {typeof (TestDependencyA), typeof (TestDependencyB)});

            var collection = adapter.Resolve<IEnumerable<ITestDependency>>().ToArray();

            var firstItem = collection.First();
            var secondItem = collection.Last();

            Assert.IsInstanceOf(typeof(TestDependencyA), firstItem);
            Assert.IsInstanceOf(typeof(TestDependencyB), secondItem);
        }
    }

    interface ITestDependency
    {
        
    }

    class TestDependencyA : ITestDependency
    {
        
    }

    class TestDependencyB : ITestDependency
    {

    }
}
