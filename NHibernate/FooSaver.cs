using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;

namespace QueryablesCompared.NHibernate
{
    public class FooSaver
    {
        public static QueryableResult<Foo> GetInDatabase(IEnumerable<Foo> inputs)
        {
            using (var sessionFactory = Setup.GetSessionFactory(true))
            {
                using(var session = sessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        foreach (var input in inputs)
                        {
                            session.Save(input);
                        }

                        transaction.Commit();
                    }
                }
            }

            var sessionFactoryResult = Setup.GetSessionFactory(false);
            var sessionResult = sessionFactoryResult.OpenSession();

            return new QueryableResult<Foo>(sessionFactoryResult, sessionResult)
                {
                    Queryable = sessionResult.Query<Foo>()
                };
        }
    }
}
