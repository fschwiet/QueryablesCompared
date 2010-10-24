using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using QueryablesCompared.EF;
using QueryablesCompared.NHibernate;
using QueryablesCompared.Object;
using QueryablesCompared.RavenDB;
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
        public void paging_before_where(IPassthroughDatabase database)
        {
            var inputs = Enumerable.Range(0, 20).Select(i => new Foo() { Value = i });

            using (var queryableResult = database.Passthrough(inputs))
            {
                var result = queryableResult.Queryable.OrderByDescending(f => f.Value).Take(5).Where(f => f.Value < 5).ToArray();

                result.Length.Should().Equal(0);
            }
        }

        [Test]
        [TestCaseSource("QueryableImplementations")]
        public void where_before_paging(IPassthroughDatabase database)
        {
            var inputs = Enumerable.Range(0, 20).Select(i => new Foo() { Value = i });

            using (var queryableResult = database.Passthrough(inputs))
            {
                var result = queryableResult.Queryable.Where(f => f.Value < 5).OrderByDescending(f => f.Value).Take(5).ToArray();

                result.Length.Should().Equal(5);
            }
        }

        public IPassthroughDatabase[] QueryableImplementations()
        {
            return new IPassthroughDatabase[]
                {
                    new ObjectPassthrough(),
                    new NHPassthrough(),
                    new EFPassthrough(),
                    new RavenDBPassthrough(),
                    new RavenDBWithoutIndexPassthrough()
                };
        }
    }
}
