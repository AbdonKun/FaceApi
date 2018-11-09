using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace BF.POC.FaceAPI.Persistence
{
    public class FaceAPIContext : DbContext
    {
        public FaceAPIContext() : base("FaceAPI")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            MapearAssembly(modelBuilder);
        }

        private void MapearAssembly(DbModelBuilder modelBuilder)
        {
            var entitiesTypes = Assembly.Load("BF.POC.FaceAPI.Domain").GetTypes().Where(type => type.Namespace == "BF.POC.FaceAPI.Domain.Entities");
            var metodo = modelBuilder.GetType().GetMethod("Entity");

            foreach (var entityType in entitiesTypes)
            {
                metodo.MakeGenericMethod(entityType).Invoke(modelBuilder, null);
            }
        }
    }
}
