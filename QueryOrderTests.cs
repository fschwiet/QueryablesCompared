using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Should.Fluent;
using Should.Fluent.Model;


using QueryableFactory = System.Func<System.Collections.Generic.IEnumerable<QueryablesCompared.Foo>, System.Linq.IQueryable<QueryablesCompared.Foo>>;

namespace QueryablesCompared
{
    public class Foo
    {
        public int Value;
    }


    [TestFixture]
    class QueryOrderTests
    {
        [Test]
        [TestCaseSource("QueryableImplementations")]
        public void take_after_sort(QueryableFactory getFooQueryable)
        {
            var inputs = Enumerable.Range(0, 20).Select(i => new Foo() {Value = i});
            var expected = Enumerable.Range(0, 5).Select(i => new Foo() {Value = i}).ToArray();

            var queryable = getFooQueryable(inputs);

            var result = queryable.OrderBy(f => f.Value).Take(5).ToArray();

            result.Length.Should().Equal(5);
            result[0].Value.Should().Equal(0);
            result[1].Value.Should().Equal(1);
            result[2].Value.Should().Equal(2);
            result[3].Value.Should().Equal(3);
            result[4].Value.Should().Equal(4);
        }

        public IEnumerable<QueryableFactory> QueryableImplementations()
        {
            return new QueryableFactory[]
                {
                    s => s.AsQueryable()
                };
        }
    }
}
