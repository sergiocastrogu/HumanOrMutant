using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.APIGatewayEvents;
using System.Collections;
using NUnit.Framework;
using System.Reflection.PortableExecutable;

namespace Mutant.Tests;

[TestFixture]
public class MutantControllerTests
{

    private readonly ILambdaContext context;

    public MutantControllerTests()
    {
        context = new TestLambdaContext();
    }

    [Test]
    public async Task TestPostNoMatchCaracters()
    {
        var lambdaFunction = new LambdaEntryPoint();
        var request = new APIGatewayProxyRequest();
        request.Body = File.ReadAllText("./SampleRequests/MutantController-POST-NoMatchCaracters.json");
        request.Path = "/api/mutant/mutant";
        request.HttpMethod = "POST";
        request.Headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        var context = new TestLambdaContext();
        var response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.AreEqual(403, response.StatusCode);
    }

    [Test]
    public async Task TestGetStats()
    {
        var lambdaFunction = new LambdaEntryPoint();
        var request = new APIGatewayProxyRequest();
        request.Path = "/api/mutant/stats";
        request.HttpMethod = "GET";
        var context = new TestLambdaContext();
        var response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.AreEqual(200, response.StatusCode);
        Assert.AreNotEqual(String.Empty, response.Body);
    }

    [Test]
    public async Task TestPostNoMutant()
    {
        var lambdaFunction = new LambdaEntryPoint();
        var request = new APIGatewayProxyRequest();
        request.Body = File.ReadAllText("./SampleRequests/MutantController-POST-NoMutant.json");
        request.Path = "/api/mutant/mutant";
        request.HttpMethod = "POST";
        request.Headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        var context = new TestLambdaContext();
        var response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.AreEqual(403, response.StatusCode);
    }

    [Test]
    public async Task TestPostMutantVertical()
    {
        var lambdaFunction = new LambdaEntryPoint();
        var request = new APIGatewayProxyRequest();
        request.Body = File.ReadAllText("./SampleRequests/MutantController-POST-VerticalMutant.json");
        request.Path = "/api/mutant/mutant";
        request.HttpMethod = "POST";
        request.Headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        var context = new TestLambdaContext();
        var response = await lambdaFunction.FunctionHandlerAsync(request, context);
        request.Headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        Assert.AreEqual(200, response.StatusCode);
    }

    [Test]
    public async Task TestPostMutantHorizontal()
    {
        var lambdaFunction = new LambdaEntryPoint();
        var request = new APIGatewayProxyRequest();
        request.Body = File.ReadAllText("./SampleRequests/MutantController-POST-HorizontalMutant.json");
        request.Path = "/api/mutant/mutant";
        request.HttpMethod = "POST";
        request.Headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        var context = new TestLambdaContext();
        var response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.AreEqual(200, response.StatusCode);
    }

    [Test]
    public async Task TestPostMutantDiagonal()
    {
        var lambdaFunction = new LambdaEntryPoint();
        var request = new APIGatewayProxyRequest();
        request.Body = File.ReadAllText("./SampleRequests/MutantController-POST-DiagonalMutant.json");
        request.Path = "/api/mutant/mutant";
        request.HttpMethod = "POST";
        request.Headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        var context = new TestLambdaContext();
        var response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.AreEqual(200, response.StatusCode);
    }

    [Test]
    public async Task TestPostMutant()
    {
        var lambdaFunction = new LambdaEntryPoint();
        var request = new APIGatewayProxyRequest();
        request.Body = File.ReadAllText("./SampleRequests/MutantController-POST-Mutant.json");
        request.Path = "/api/mutant/mutant";
        request.HttpMethod = "POST";
        request.Headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        var context = new TestLambdaContext();
        var response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.AreEqual(200, response.StatusCode);
    }

    [Test]
    public async Task TestPostRepeatMutant()
    {
        var lambdaFunction = new LambdaEntryPoint();
        var request = new APIGatewayProxyRequest();
        request.Body = File.ReadAllText("./SampleRequests/MutantController-POST-Mutant.json");
        request.Path = "/api/mutant/mutant";
        request.HttpMethod = "POST";
        request.Headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        var context = new TestLambdaContext();
        var response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.AreEqual(200, response.StatusCode);
    }

    [Test]
    public async Task TestPostNullMutant()
    {
        var lambdaFunction = new LambdaEntryPoint();
        var request = new APIGatewayProxyRequest();
        request.Body = File.ReadAllText("./SampleRequests/MutantController-POST-Null-Mutant.json");
        request.Path = "/api/mutant/mutant";
        request.HttpMethod = "POST";
        request.Headers = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        var context = new TestLambdaContext();
        var response = await lambdaFunction.FunctionHandlerAsync(request, context);

        Assert.AreEqual(403, response.StatusCode);
    }

}