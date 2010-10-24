using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Dialect;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;

namespace QueryablesCompared.NHibernate
{
    public class Setup
    {
        public static Antlr.Runtime.ANTLRFileStream _ignored;

        public static ISessionFactory GetSessionFactory(bool resetData)
        {
            var nhConfig = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connstr => connstr.FromConnectionStringWithKey("db"))
                    .ProxyFactoryFactory<ProxyFactoryFactory>()
                    .AdoNetBatchSize(100)
                )
                .Mappings(mapping => mapping.FluentMappings.AddFromAssemblyOf<Foo>())
                .BuildConfiguration();

            if (resetData)
            {
                var schemaExport = new SchemaExport(nhConfig);

                schemaExport.Create(false, true);
            }

            return nhConfig.BuildSessionFactory();
        }
    }
}
