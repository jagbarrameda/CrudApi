using Amazon.CDK;
using io.jbarrameda.CrudApi.example;

namespace io.jbarrameda.CrudApi
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            var schoolStack = new SchoolStack(app, "SchoolAppStack");
            app.Synth();
        }
    }
}
