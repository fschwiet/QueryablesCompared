using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Raven.Client.Document;
using Raven.Database;

namespace QueryablesCompared.RavenDB
{
    public class RavenDBWithoutIndexPassthrough : IPassthroughDatabase
    {
        public QueryableResult<Foo> Passthrough(IEnumerable<Foo> inputs)
        {
            var store = new DocumentStore() { RunInMemory = true };

            store.Initialize();

            using(var session = store.OpenSession())
            {
                foreach(var input in inputs)
                {
                    session.Store(input);
                }
                session.SaveChanges();
            }

            var sessionResult = store.OpenSession();

            return new QueryableResult<Foo>(store, sessionResult)
                {
                    Queryable = sessionResult.Query<Foo>().AsQueryable()
                };
        }
    }
}
