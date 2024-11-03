using System;
using System.ComponentModel.DataAnnotations;

namespace DotNetHtmxTemplate.Repository.Data
{

    public abstract class AppEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        [Key]
        public virtual TKey Id { get; set; } = default!;
    }

}