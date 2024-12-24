using GDATemplate.Data.Context;
using GDATemplate.Data.Interfaces;
using GDATemplate.Domain.Entities;

namespace GDATemplate.Data.Entities
{
    public class DemoRepository : BaseRepository<Demo, SqlContext>, IDemoRepository
    {
        public DemoRepository(SqlContext sqlContext) : base(sqlContext)
        {
        }
    }
}