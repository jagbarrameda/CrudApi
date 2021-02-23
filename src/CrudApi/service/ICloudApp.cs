namespace io.jbarrameda.CrudApi.service
{
    /// <summary>
    /// Represents a complete cloud application.
    /// 
    /// A cloud application consists of a set of APIs and cloud resources. 
    /// An API is an endpoint that the application or users of the application interact with.
    /// A API may be public or internal to the application.
    /// 
    /// APIs are grouped in set via ApiSets usually when the are related by the resources they use.
    /// For example, all the CRUD apis for the same data type are grouped together in a APISet.
    /// Such APISet creates all resources needed by these apis, such as a db table.
    ///
    /// Ideally, APIs from other APISets must interact between them.
    /// An APISet must not access the resources of another APISet, such approach is known as back-door
    /// and is discouraged as a bad practice.
    ///
    /// An application has a set of cloud application dependencies.
    /// An dependency is a set of external APIs that the application depends on.
    /// Any degradation on these dependencies may bring part of all the application down.
    /// Each of these APIs have health metrics.
    /// The application tracks these health metrics and
    /// the application may and should define alarms for their expected SLOs and SLAs. 
    ///
    /// An application has dashboards.
    /// The dashboards reflect the running state of the application, including its dependencies.
    /// For each API, the dashboard shows 3 metrics:
    /// 1. availability
    /// 1. latency
    /// 1. transactions per second
    /// Each of these 2 graph may also shows three thresholds:
    /// 1. SLA - the service level agreement.
    /// This is captured in contract, therefore violating it may have legal and economic consequences.  
    /// 1. SLO - the service level objective.
    /// This is the intended level of service, more strict than SLA.
    /// It is internal and it is used during operation to alert of potential degradation of the service
    /// and to avoid incurring in SLA violations.
    ///
    /// There is a dashboard for public API.
    /// A dashboard for internal APIs and for dependencies.
    ///
    /// The ICloudApp is independent of the underlying cloud or clouds.
    /// This will help to address the cloud lock risks of cloud applications. 
    /// </summary>
    public interface ICloudApp
    {
        /// <summary>
        /// Adds a APISet to the cloud app.
        /// </summary>
        /// <param name="apiSet">The api set</param>
        public void AddApiSet(IApiSet apiSet);

        /// <summary>
        /// Synthesizes the app, creating and deploying all required infrastructure.
        /// </summary>
        public void Synth();
    }
    
}