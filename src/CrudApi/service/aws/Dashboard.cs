using System.Collections.Generic;
using System.Linq;
using Amazon.CDK;
using Amazon.CDK.AWS.CloudWatch;

namespace io.jbarrameda.CrudApi.service.aws
{
    /**
     * Dashboard
     */
    public class Dashboard : IDashboard
    {
        private Construct Scope { get; set; }

        public Dashboard(Construct scope)
        {
            Scope = scope;
        }

        /// <summary>
        /// Creates the resources for the dashboard based on the APIs.
        /// </summary>
        /// <param name="apis"></param>
        public void CreateResources(HashSet<IApiSet> apis)
        {
            Amazon.CDK.AWS.CloudWatch.Dashboard awsDashboard =
                new Amazon.CDK.AWS.CloudWatch.Dashboard(Scope, "The super dashboard");
            AddApiSetWidgets(awsDashboard, apis);
        }

        /// <summary>
        /// Adds the widgets of a set of APIs.
        /// The widgets get added one row for each api.
        /// If an API has multiple widgets, they all get added in the same row.
        /// </summary>
        /// <param name="awsDashboard"></param>
        /// <param name="apis"></param>
        private void AddApiSetWidgets(Amazon.CDK.AWS.CloudWatch.Dashboard awsDashboard, HashSet<IApiSet> apis)
        {
            foreach (var apiSet in apis)
            {
                AddApiWidgets(awsDashboard, apiSet);
            }
        }

        private void AddApiWidgets(Amazon.CDK.AWS.CloudWatch.Dashboard awsDashboard, IApiSet apiSet)
        {
            // Add apiset header widget
            var apiSetWidgets = new IWidget[]
            {
                // api header widget
                new TextWidget(new TextWidgetProps {Markdown = "# " + apiSet.Name + " API set", Height = 1, Width = 24})
            };
            awsDashboard.AddWidgets(apiSetWidgets);

            foreach (var monitor in apiSet.GetApiMonitors())
            {
                AddApiWidgets(awsDashboard, monitor);
            }
        }

        /// <summary>
        /// Adds the widgets of an API.
        /// The widgets get added in the same row in the dashboard
        /// </summary>
        /// <param name="awsDashboard"></param>
        /// <param name="apiMonitor"></param>
        private void AddApiWidgets(Amazon.CDK.AWS.CloudWatch.Dashboard awsDashboard, ApiMonitor apiMonitor)
        {
            awsDashboard.AddWidgets(GetWidgets(apiMonitor));
        }

        /// <summary>
        /// Gets the widgets of an API
        /// </summary>
        /// <param name="apiMonitor"></param>
        /// <returns></returns>
        private IWidget[] GetWidgets(ApiMonitor apiMonitor)
        {
            var apiWidgets = new List<IWidget>
            {
                // api header widget
                new Spacer(new SpacerProps {Height = 2, Width = 24}),
                new Row(new TextWidget(new TextWidgetProps
                    {Markdown = "## " + apiMonitor.ApiName + " API", Height = 1, Width = 24}))
            };
            // api's metrics' widgets
            apiWidgets.AddRange(from metric in apiMonitor.MetricMonitors
                where metric.DashboardVisible
                select GetWidget(metric));
            return apiWidgets.ToArray();
        }

        /// <summary>
        /// Gets the GraphWidget of a metric
        /// </summary>
        /// <param name="metricMonitor"></param>
        /// <returns></returns>
        private IWidget GetWidget(MetricMonitor metricMonitor)
        {
            if (metricMonitor.MetricName.Contains("Availability"))
            {
                return new GraphWidget(new GraphWidgetProps
                {
                    Height = 6, Width = 6,
                    Left = new IMetric[]
                    {
                        new Metric(new MetricProps
                        {
                            MetricName = "BucketSizeBytes",
                            Namespace = "AWS/S3",
                            Dimensions = new Dictionary<string, object>
                            {
                                {"StorageType", "StandardStorage"},
                                {"BucketName", "aws-codestar-ca-central-1-649438689429-compete-pipe"}
                            }
                        })
                    }
                });
            }
            
            if (metricMonitor.MetricName.Contains("tency"))
            {
                return new GraphWidget(new GraphWidgetProps
                {
                    Height = 6, Width = 6,
                    Left = new IMetric[]
                    {
                        new Metric(new MetricProps
                        {
                            MetricName = "SuccessfulRequestLatency",
                            Namespace = "AWS/DynamoDB",
                            Dimensions = new Dictionary<string, object>
                            {
                                {"TableName", "CompeteApp-WeightsTableA0241212-NW7UC1M5YGQG"},
                                {"Operation", "Scan"}
                            }
                        })
                    }
                });
            }
            
            if (metricMonitor.MetricName.Contains("put"))
            {
                return new GraphWidget(new GraphWidgetProps
                {
                    Height = 6, Width = 6,
                    Left = new IMetric[]
                    {
                        new Metric(new MetricProps
                        {
                            MetricName = "ConsumedReadCapacityUnits",
                            Namespace = "AWS/DynamoDB",
                            Dimensions = new Dictionary<string, object>
                            {
                                {"TableName", "CompeteApp-WeightsTableA0241212-16YD2VW0754RW"}
                            }
                        })
                    }
                });
            }
            
            
            if (metricMonitor.MetricName.Contains("rrors"))
            {
                return new GraphWidget(new GraphWidgetProps
                {
                    Height = 6, Width = 6,
                    Left = new IMetric[]
                    {
                        new Metric(new MetricProps
                        {
                            MetricName = "UserErrors",
                            Namespace = "AWS/DynamoDB"
                        })
                    }
                });
            }

            return new TextWidget(new TextWidgetProps
            {
                Markdown = "### " + metricMonitor.MetricName,
                Height = 3
            });
        }
    }
}