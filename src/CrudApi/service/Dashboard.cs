using System.Collections.Generic;
using System.Linq;
using Amazon.CDK;
using Amazon.CDK.AWS.CloudWatch;
using AwsDashboard = Amazon.CDK.AWS.CloudWatch.Dashboard;

namespace io.jbarrameda.CrudApi.service
{
    /**
     * Creates a dashboard for a IApiService service
     */
    public class Dashboard
    {
        /// <summary>
        /// Creates the resources for the dashboard based on the APIs.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="apis"></param>
        public void CreateResources(Construct scope, HashSet<IApiSet> apis)
        {
            AwsDashboard awsDashboard = new AwsDashboard(scope, "The super dashboard");
            AddApiSetWidgets(awsDashboard, apis);
        }

        /// <summary>
        /// Adds the widgets of a set of APIs.
        /// The widgets get added one row for each api.
        /// If an API has multiple widgets, they all get added in the same row.
        /// </summary>
        /// <param name="awsDashboard"></param>
        /// <param name="apis"></param>
        private void AddApiSetWidgets(AwsDashboard awsDashboard, HashSet<IApiSet> apis)
        {
            foreach (var apiSet in apis)
            {
                AddApiWidgets(awsDashboard, apiSet);
            }
        }

        private void AddApiWidgets(AwsDashboard awsDashboard, IApiSet apiSet)
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
        private void AddApiWidgets(AwsDashboard awsDashboard, ApiMonitor apiMonitor)
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
            apiWidgets.AddRange(from metric in apiMonitor.MetricMonitors where metric.DashboardVisible select GetWidget(metric));
            return apiWidgets.ToArray();
        }

        /// <summary>
        /// Gets the GraphWidget of a metric
        /// </summary>
        /// <param name="metricMonitor"></param>
        /// <returns></returns>
        private IWidget GetWidget(MetricMonitor metricMonitor)
        {
            return new TextWidget(new TextWidgetProps
            {
                Markdown = "### " + metricMonitor.MetricName,
                Height = 3
            });
        }
    }
}