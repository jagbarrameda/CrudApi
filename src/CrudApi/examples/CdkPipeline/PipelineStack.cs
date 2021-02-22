using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.CodeBuild;
using Amazon.CDK.AWS.CodeCommit;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.CodePipeline.Actions;
using Amazon.CDK.AWS.Lambda;
using StageProps = Amazon.CDK.AWS.CodePipeline.StageProps;

namespace io.jbarrameda.CrudApi.examples.CdkPipeline
{
    public class PipelineStackProps : StackProps
    {
        public CfnParametersCode LambdaCode { get; set; }
    }

    public class PipelineStack : Stack
    {
        public PipelineStack(App app, string id, PipelineStackProps props = null) : base(app, id, props)
        {
            var code = Repository.FromRepositoryName(this, "ImportedRepo",
                "NameOfYourCodeCommitRepository");

            var cdkBuild = new PipelineProject(this, "CDKBuild", new PipelineProjectProps
            {
                BuildSpec = BuildSpec.FromObject(new Dictionary<string, object>
                {
                    ["version"] = "0.2",
                    ["phases"] = new Dictionary<string, object>
                    {
                        ["install"] = new Dictionary<string, object>
                        {
                            ["commands"] = "npm install"
                        },
                        ["build"] = new Dictionary<string, object>
                        {
                            ["commands"] = new string[]
                            {
                                "npm run build",
                                "npm run cdk synth -- o dist"
                            }
                        }
                    },
                    ["artifacts"] = new Dictionary<string, object>
                    {
                        ["base-directory"] = "dist"
                    },
                    ["files"] = new string[]
                    {
                        "LambdaStack.template.json"
                    }
                }),
                Environment = new BuildEnvironment
                {
                    BuildImage = LinuxBuildImage.STANDARD_2_0
                }
            });

            var lambdaBuild = new PipelineProject(this, "LambdaBuild", new PipelineProjectProps
            {
                BuildSpec = BuildSpec.FromObject(new Dictionary<string, object>
                {
                    ["version"] = "0.2",
                    ["phases"] = new Dictionary<string, object>
                    {
                        ["install"] = new Dictionary<string, object>
                        {
                            ["commands"] = new string[]
                            {
                                "cd lambda",
                                "npm install"
                            }
                        },
                        ["build"] = new Dictionary<string, string>
                        {
                            ["commands"] = "npm run build"
                        }
                    },
                    ["artifacts"] = new Dictionary<string, object>
                    {
                        ["base-directory"] = "lambda",
                        ["files"] = new string[]
                        {
                            "index.js",
                            "node_modules/**/*"
                        }
                    }
                }),
                Environment = new BuildEnvironment
                {
                    BuildImage = LinuxBuildImage.STANDARD_2_0
                }
            });

            var sourceOutput = new Artifact_();
            var cdkBuildOutput = new Artifact_("CdkBuildOutput");
            var lambdaBuildOutput = new Artifact_("LambdaBuildOutput");

            new Pipeline(this, "Pipeline", new PipelineProps
            {
                Stages = new[]
                {
                    new StageProps
                    {
                        StageName = "Source",
                        Actions = new[]
                        {
                            new CodeCommitSourceAction(new CodeCommitSourceActionProps
                            {
                                ActionName = "Source",
                                Repository = code,
                                Output = sourceOutput
                            })
                        }
                    },
                    new StageProps
                    {
                        StageName = "Build",
                        Actions = new[]
                        {
                            new CodeBuildAction(new CodeBuildActionProps
                            {
                                ActionName = "Lambda_Build",
                                Project = lambdaBuild,
                                Input = sourceOutput,
                                Outputs = new[] {lambdaBuildOutput}
                            }),
                            new CodeBuildAction(new CodeBuildActionProps
                            {
                                ActionName = "CDK_Build",
                                Project = cdkBuild,
                                Input = sourceOutput,
                                Outputs = new[] {cdkBuildOutput}
                            })
                        }
                    },
                    new StageProps
                    {
                        StageName = "Deploy",
                        Actions = new[]
                        {
                            new CloudFormationCreateUpdateStackAction(new CloudFormationCreateUpdateStackActionProps
                            {
                                ActionName = "Lambda_CFN_Deploy",
                                TemplatePath = cdkBuildOutput.AtPath("LambdaStack.template.json"),
                                StackName = "LambdaDeploymentStack",
                                AdminPermissions = true,
                                ParameterOverrides = props.LambdaCode.Assign(lambdaBuildOutput.S3Location),
                                ExtraInputs = new[] {lambdaBuildOutput}
                            })
                        }
                    }
                }
            });
        }
    }
}