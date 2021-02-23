using System.Collections.Generic;
using Amazon.CDK;

namespace io.jbarrameda.CrudApi.service.aws
{
    /// <summary>
    /// A CloudApp backed by Aws.
    /// </summary>
    public class AwsCloudApp : Stack, ICloudApp
    {
        private readonly App _app;
        private readonly HashSet<IApiSet> _apiSets = new();
        
        internal AwsCloudApp(App app, string id, IStackProps props = null) : base(app, id, props)
        {
            _app = app;
        }

        public void AddApiSet(IApiSet set)
        {
            _apiSets.Add(set);
        }
        
        public void Synth()
        {
            CreateResources();
            _app.Synth();
        }

        private void CreateResources()
        {
            foreach (var apiSet in _apiSets)
            {
                apiSet.CreateResources();
            }
            // create a dashboard
            new Dashboard().CreateResources(this, _apiSets);
        }

    }
}