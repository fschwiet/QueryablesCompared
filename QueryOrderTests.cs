using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using QueryablesCompared.NHibernate;
using Should.Fluent;
using Should.Fluent.Model;


using QueryableFactory = System.Func<System.Collections.Generic.IEnumerable<QueryablesCompared.Foo>, System.Linq.IQueryable<QueryablesCompared.Foo>>;

namespace QueryablesCompared
{
    [TestFixture]
    class QueryOrderTests
    {
        [Test]
        [TestCaseSource("QueryableImplementations")]
        public void take_after_sort(Func<IEnumerable<Foo>, QueryableResult<Foo>> getFooQueryable)
        {
            var inputs = Enumerable.Range(0, 20).Select(i => new Foo() {Value = i});

            using (var queryableResult = getFooQueryable(inputs))
            {
                var result = queryableResult.Queryable.OrderByDescending(f => f.Value).Take(5).Where(f => f.Value < 5).ToArray();

                result.Length.Should().Equal(0);
            }
        }

        public IEnumerable<Func<IEnumerable<Foo>, QueryableResult<Foo>>> QueryableImplementations()
        {
            return new Func<IEnumerable<Foo>, QueryableResult<Foo>>[]
                {
                    s => new QueryableResult<Foo>() { Queryable = s.AsQueryable()} , 
                    FooSaver.GetInDatabase
                };
        }
    }
}
