using System;
using System.Collections.Generic;

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
        string Name { get; set; }

        /// <summary>
        /// Create infrastructure resources needed by the set of APIs.
        /// </summary>
        void CreateResources();

        List<ApiMonitor> GetApiMonitors();
    }

    /// <summary>
    /// An API in the application.
    ///
    /// E.g.: a CRUD's get operation. 
    /// </summary>
    public class ApiMonitor
    {
        /// <summary>
        /// Name of the API
        /// </summary>
        public String ApiName { get; init; }

        /// <summary>
        /// Monitors of the metrics of the API
        /// </summary>
        public List<MetricMonitor> MetricMonitors { get; set; }
    }

    /// <summary>
    /// Monitors of a metric.
    /// </summary>
    public class MetricMonitor
    {
        /// <summary>
        /// Name of the metric.
        ///
        /// E.g. Availability, Throughput, Latency
        /// </summary>
        public String MetricName { get; set; }

        /// <summary>
        /// Description of the metric.
        ///
        /// E.g. percentage of successful requests 
        /// </summary>
        public String MetricDescription { get; set; }

        /// <summary>
        /// Signals if the metric should show in the dashboard
        /// </summary>
        public bool DashboardVisible { get; set; }

        /// <summary>
        /// The thresholds of the metric.
        ///
        /// E.g. SLA threshold, SLO, SLI
        /// </summary>
        public List<Monitor> Monitors { get; set; }
    }

    /// <summary>
    /// A threshold of a metric
    /// </summary>
    public class Monitor
    {
        /// <summary>
        /// If the violation of the threshold should trigger an alarm
        /// </summary>
        private bool IsAlarmEnabled { get; set; }

        /// <summary>
        /// The level of the alarm of the threshold if IsAlarmEnabled 
        /// </summary>
        AlarmLevel AlarmLevel { get; set; }

        double ThresholdValue { get; set; }

        ThresholdType ThresholdType { get; set; }
    }

    public enum ThresholdType
    {
        MAX,
        MIN
    }

    public enum AlarmLevel
    {
        /// <summary>
        /// High severity, e.g. violation of SLA (service level agreement).
        /// Economic impact, user experience degraded
        /// </summary>
        SEV1,

        /// <summary>
        /// Medium severity, e.g. violation of SLO (service level objective):
        /// User experience degraded, no economic impact
        /// </summary>
        SEV2,

        /// <summary>
        /// Low severity, e.g. violation of SLI (service level indicator).
        /// User experience is not degraded and there is no economic impact
        /// There is performance degradation.
        /// </summary>
        SEV3
    }
}