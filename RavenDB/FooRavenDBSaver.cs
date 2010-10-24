using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Raven.Client.Document;
using Raven.Database;
using Raven.Client;
using Raven.Database.Indexing;

namespace QueryablesCompared.RavenDB
{
    public class FooRavenDBWithIndexSaver
    {
        public static QueryableResult<Foo> GetInDatabase(IEnumerable<Foo> inputs)
        {
            var currentIndex = 0;

            var store = new DocumentStore()
                {
                    RunInMemory = true,
                    Conventions = new DocumentConvention()
                        {
                            DocumentKeyGenerator = f => (currentIndex++).ToString()
                        }
                    
                };

            store.Initialize();

            store.DatabaseCommands.PutIndex("foo", new IndexDefinition()
                {
                    Map = "from doc in docs select new { doc.Value }"
                });

            using(var session = store.OpenSession())
            {
                foreach(var input in inputs)
                {
                    session.Store(input);
                }
                session.SaveChanges();
            }

            while (store.DocumentDatabase.Statistics.StaleIndexes.Count() > 0)
            {
                Thread.Sleep(100);
            }            

            var sessionResult = store.OpenSession();

            return new QueryableResult<Foo>(store, sessionResult)
                {
                    Queryable = sessionResult.Query<Foo>("foo").AsQueryable()
                };
        }
    }
}
