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
            foreach (var api in apis.SelectMany(apiSet => apiSet.GetApis()))
            {
                AddApiWidgets(awsDashboard, api);
            }
        }
        
        /// <summary>
        /// Adds the widgets of an API.
        /// The widgets get added in the same row in the dashboard
        /// </summary>
        /// <param name="awsDashboard"></param>
        /// <param name="api"></param>
        private void AddApiWidgets(AwsDashboard awsDashboard, Api api)
        {
            awsDashboard.AddWidgets(GetWidgets(api));
        }

        /// <summary>
        /// Gets the widgets of an API
        /// </summary>
        /// <param name="api"></param>
        /// <returns></returns>
        private IWidget[] GetWidgets(Api api)
        {
            return (from metric in api.DashboardMetrics where metric.Visible select GetWidget(metric)).ToArray();
        }

        /// <summary>
        /// Gets the GraphWidget of a metric
        /// </summary>
        /// <param name="metric"></param>
        /// <returns></returns>
        private IWidget GetWidget(DashboardMetric metric)
        {
            return new TextWidget(new TextWidgetProps()
            {
                Markdown = metric.Name
            });
        }
    }
}