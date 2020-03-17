using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJie.Identity.ID4.Data
{
    /// <summary>
    /// 登录时数据交互
    /// </summary>
    public class CustomPersistedGrantDbContext : PersistedGrantDbContext<CustomPersistedGrantDbContext>
    {
        public CustomPersistedGrantDbContext(DbContextOptions options,
            OperationalStoreOptions storeOptions) : base(options, storeOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            /////表名设置小写
            //foreach (var _entityType in builder.Model.GetEntityTypes())
            //{
            //    _entityType.SetTableName(_entityType.GetTableName().ToLower());

            //    ///字段设置为小写
            //    foreach (var _property in _entityType.GetProperties())
            //    {
            //        _property.SetColumnName(_property.GetColumnName().ToLower());
            //        foreach (var containingkey in _property.GetContainingKeys())
            //        {
            //            containingkey.SetName(containingkey.GetName().ToLower());
            //        }
            //        foreach (var _index in _property.GetContainingIndexes())
            //        {
            //            _index.SetName(_index.GetName().ToLower());
            //        }
            //        foreach (var _foreignkey in _property.GetContainingForeignKeys())
            //        {
            //            _foreignkey.SetConstraintName(_foreignkey.GetConstraintName().ToLower());
            //        }
            //    }
            //}
        }
    }
}
