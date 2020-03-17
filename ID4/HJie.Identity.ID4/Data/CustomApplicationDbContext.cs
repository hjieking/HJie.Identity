using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJie.Identity.ID4.Data
{
    /// <summary>
    /// 应用用户数据
    /// </summary>
    public class CustomApplicationDbContext: IdentityDbContext<IdentityUser, IdentityRole,string>
    {
        public CustomApplicationDbContext(DbContextOptions<CustomApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ///表名设置小写
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
