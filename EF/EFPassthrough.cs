using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryablesCompared.EF
{
    public class EFPassthrough : IPassthroughDatabase
    {
        public QueryableResult<QueryablesCompared.Foo> Passthrough(IEnumerable<QueryablesCompared.Foo> inputs)
        {
            using(var context = new FooContext(true))
            {
                foreach(var input in inputs)
                    context.Foos.Add(input);

                context.SaveChanges();
            }

            var resultContext = new FooContext(false);

            return new QueryableResult<QueryablesCompared.Foo>(resultContext)
                {
                    Queryable = resultContext.Foos.AsQueryable()
                };
        }
    }
}
