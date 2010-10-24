using System.Collections.Generic;

namespace QueryablesCompared
{
    public interface IPassthroughDatabase
    {
        QueryableResult<QueryablesCompared.Foo> Passthrough(IEnumerable<QueryablesCompared.Foo> inputs);
    }
}