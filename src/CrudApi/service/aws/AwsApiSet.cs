using System.Collections.Generic;
using Amazon.CDK;

namespace io.jbarrameda.CrudApi.service.aws
{
    /// <summary>
    /// A IApiSet backed by AWS
    /// </summary>
    public abstract class AwsApiSet : IApiSet
    {
        protected readonly Stack Stack;

        protected AwsApiSet(Stack stack)
        {
            Stack = stack;
        }

        /// <summary>
        /// Name of the set of APIs
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Creates the resources needed by this set of apis
        /// </summary>
        public abstract void CreateResources();

        /// <summary>
        /// Gets the monitors of the APIs
        /// </summary>
        /// <returns></returns>
        public abstract List<ApiMonitor> GetApiMonitors();
    }
}