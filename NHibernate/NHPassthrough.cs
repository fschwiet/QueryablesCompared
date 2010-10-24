using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;

namespace QueryablesCompared.NHibernate
{
    public class NHPassthrough : IPassthroughDatabase
    {
        public QueryableResult<Foo> Passthrough(IEnumerable<Foo> inputs)
        {
            using (var sessionFactory = Setup.GetSessionFactory(true))
            {
                using(var session = sessionFactory.OpenSession())
                {
                    foreach (var input in inputs)
                    {
                        session.Save(input);
                    }

                    session.Flush();
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
