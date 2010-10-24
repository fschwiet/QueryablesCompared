using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryablesCompared.Object
{
    class ObjectPassthrough : IPassthroughDatabase
    {
        public QueryableResult<Foo> Passthrough(IEnumerable<Foo> inputs)
        {
            return new QueryableResult<Foo>() {Queryable = inputs.AsQueryable()};
        }
    }
}
