using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace QueryablesCompared.NHibernate.Mappings
{
    public class FooMapping : ClassMap<Foo>
    {
        public FooMapping()
        {
            Id(p => p.Identifier).GeneratedBy.Native();
            Map(p => p.Value);
        }
    }
}
