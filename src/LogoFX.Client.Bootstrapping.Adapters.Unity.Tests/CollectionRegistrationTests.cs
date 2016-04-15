using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LogoFX.Client.Bootstrapping.Adapters.Unity.Tests
{
    [TestFixture]
    class CollectionRegistrationTests
    {
        [Test]
        public void MultipleImplementationAreRegisteredByType_ResolvedCollectionContainsAllImplementations()
        {
            var adapter = new UnityContainerAdapter();
            adapter.RegisterCollection<ITestDependency>(new[] { typeof(TestDependencyA), typeof(TestDependencyB) });

            var collection = adapter.Resolve<IEnumerable<ITestDependency>>().ToArray();

            var firstItem = collection.First();
            var secondItem = collection.Last();

            Assert.IsInstanceOf(typeof(TestDependencyA), firstItem);
            Assert.IsInstanceOf(typeof(TestDependencyB), secondItem);
        }

        [Test]
        public void MultipleImplementationAreRegisteredByTypeAsParameter_ResolvedCollectionContainsAllImplementations()
        {
            var adapter = new UnityContainerAdapter();
            adapter.RegisterCollection(typeof(ITestDependency), new[] { typeof(TestDependencyA), typeof(TestDependencyB) });

            var collection = adapter.Resolve<IEnumerable<ITestDependency>>().ToArray();

            var firstItem = collection.First();
            var secondItem = collection.Last();

            Assert.IsInstanceOf(typeof(TestDependencyA), firstItem);
            Assert.IsInstanceOf(typeof(TestDependencyB), secondItem);
        }

        [Test]
        public void MultipleImplementationAreRegisteredByInstance_ResolvedCollectionContainsAllImplementations()
        {
            var adapter = new UnityContainerAdapter();
            var instanceA = new TestDependencyA();
            var instanceB = new TestDependencyB();
            adapter.RegisterCollection(new ITestDependency[] { instanceA, instanceB });

            var collection = adapter.Resolve<IEnumerable<ITestDependency>>().ToArray();

            var firstItem = collection.First();
            var secondItem = collection.Last();

            Assert.AreSame(instanceA, firstItem);
            Assert.AreSame(instanceB, secondItem);
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
