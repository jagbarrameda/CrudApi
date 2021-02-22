namespace io.jbarrameda.CrudApi.service
{
    /**
     * A set of api.
     * 
     * These apis are usually related but not necessarily.
     * This set create will define and create cloud resources such as storage, server-less services, and api endpoints.
     */
    public interface IApiSet
    {
        /// <summary>
        /// Create infrastructure resources needed by the set of APIs.
        /// </summary>
        void CreateResources();
    }
}