using System.Collections.Generic;

namespace io.jbarrameda.CrudApi.service
{
    /// <summary>
    /// A Dashboard
    /// </summary>
    public interface IDashboard
    {
        void CreateResources(HashSet<IApiSet> apis);
    }
}