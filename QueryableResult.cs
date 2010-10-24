using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryablesCompared
{
    public class QueryableResult<T> : IDisposable
    {
        public IQueryable<T> Queryable;

        readonly IEnumerable<IDisposable> _disposables;

        public QueryableResult(params IDisposable[] disposables)
        {
            _disposables = disposables ?? new IDisposable[0];
        }

        public virtual void Dispose()
        {
            foreach(var disposeable in _disposables)
                disposeable.Dispose();
        }
    }
}
