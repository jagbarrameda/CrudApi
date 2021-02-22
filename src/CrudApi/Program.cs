using Amazon.CDK;
using io.jbarrameda.CrudApi.examples.CompeteApp;

namespace io.jbarrameda.CrudApi
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var competeApp = new CompeteApp(new App(), "CompeteApp");
            competeApp.Synth();
        }
    }
}
