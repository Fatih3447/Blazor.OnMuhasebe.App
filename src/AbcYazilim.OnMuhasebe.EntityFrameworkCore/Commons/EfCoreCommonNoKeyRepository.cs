using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbcYazilim.OnMuhasebe.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace AbcYazilim.OnMuhasebe.Commons;

public class EfCoreCommonNoKeyRepository<TEntity> : EfCoreRepository<OnMuhasebeDbContext, TEntity>,
    ICommonNoKeyRepository<TEntity> where TEntity : class, IEntity
{
    public EfCoreCommonNoKeyRepository(IDbContextProvider<OnMuhasebeDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }

    public async Task<TEntity> FromSqlRawSingleAsync(string sql, params object[] parameters)
    {
        var dbSet = await GetDbSetAsync();
        return (await dbSet.FromSqlRaw(sql, parameters).ToListAsync()).FirstOrDefault();
    }

    public async Task<IList<TEntity>> FromSqlRawAsync(string sql, params object[] parameters)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FromSqlRaw(sql, parameters).ToListAsync();
    }
}