using Amazon.CDK;
using io.jbarrameda.CrudApi.service.aws;

namespace io.jbarrameda.CrudApi.examples.CompeteApp
{
    /**
     * An AWS stack for the backend of the Compete App.
     *
     * It contains a API service for logging the weights of the players
     */
    public class CompeteApp : AwsCloudApp
    {
        internal CompeteApp(App app, string id, IStackProps props = null) : base(app, id, props)
        {
            // The code that defines your stack goes here
            CreateResources();
        }

        private void CreateResources()
        {
            AddApiSet(new DdbCrudApiSet(this, "Weights"));
            // var l = new WidgetService(this, "TheWidgetsService");
        }
    }
}
