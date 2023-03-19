# HumanOrMutant

# ASP.NET Core Web API Serverless Application

This project shows how to run an ASP.NET Core Web API project as an AWS Lambda exposed through Amazon API Gateway. The NuGet package [Amazon.Lambda.AspNetCoreServer](https://www.nuget.org/packages/Amazon.Lambda.AspNetCoreServer) contains a Lambda function that is used to translate requests from API Gateway into the ASP.NET Core framework and then the responses from ASP.NET Core back to API Gateway.


For more information about how the Amazon.Lambda.AspNetCoreServer package works and how to extend its behavior view its [README](https://github.com/aws/aws-lambda-dotnet/blob/master/Libraries/src/Amazon.Lambda.AspNetCoreServer/README.md) file in GitHub.


### Configuring for API Gateway HTTP API ###

API Gateway supports the original REST API and the new HTTP API. In addition HTTP API supports 2 different
payload formats. When using the 2.0 format the base class of `LambdaEntryPoint` must be `Amazon.Lambda.AspNetCoreServer.APIGatewayHttpApiV2ProxyFunction`.
For the 1.0 payload format the base class is the same as REST API which is `Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction`.
**Note:** when using the `AWS::Serverless::Function` CloudFormation resource with an event type of `HttpApi` the default payload
format is 2.0 so the base class of `LambdaEntryPoint` must be `Amazon.Lambda.AspNetCoreServer.APIGatewayHttpApiV2ProxyFunction`.


### Configuring for Application Load Balancer ###

To configure this project to handle requests from an Application Load Balancer instead of API Gateway change
the base class of `LambdaEntryPoint` from `Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction` to 
`Amazon.Lambda.AspNetCoreServer.ApplicationLoadBalancerFunction`.

### Project Files ###

* serverless.template - an AWS CloudFormation Serverless Application Model template file for declaring your Serverless functions and other AWS resources
* aws-lambda-tools-defaults.json - default argument settings for use with Visual Studio and command line deployment tools for AWS
* LambdaEntryPoint.cs - class that derives from **Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction**. The code in 
this file bootstraps the ASP.NET Core hosting framework. The Lambda function is defined in the base class.
Change the base class to **Amazon.Lambda.AspNetCoreServer.ApplicationLoadBalancerFunction** when using an 
Application Load Balancer.
* LocalEntryPoint.cs - for local development this contains the executable Main function which bootstraps the ASP.NET Core hosting framework with Kestrel, as for typical ASP.NET Core applications.
* Startup.cs - usual ASP.NET Core Startup class used to configure the services ASP.NET Core will use.
* web.config - used for local development.
* Controllers\ValuesController - example Web API controller

You may also have a test project depending on the options selected.

## Here are some steps to follow from Visual Studio:

To deploy your Serverless application, right click the project in Solution Explorer and select *Publish to AWS Lambda*.

To view your deployed application open the Stack View window by double-clicking the stack name shown beneath the AWS CloudFormation node in the AWS Explorer tree. The Stack View also displays the root URL to your published application.

## Here are some steps to follow to get started from the command line:

Once you have edited your template and code you can deploy your application using the [Amazon.Lambda.Tools Global Tool](https://github.com/aws/aws-extensions-for-dotnet-cli#aws-lambda-amazonlambdatools) from the command line.

Install Amazon.Lambda.Tools Global Tools if not already installed.
```
    dotnet tool install -g Amazon.Lambda.Tools
```

If already installed check if new version is available.
```
    dotnet tool update -g Amazon.Lambda.Tools
```

Execute unit tests
```
    cd "Mutant/test/Mutant.Tests"
    dotnet test
```

Deploy application
```
    cd "Mutant/src/Mutant"
    dotnet lambda deploy-serverless
```

## End of automatic documentation

## Documentación del proyecto.

Este proyecto se creó sobre una función Serverless de c# con dotnet 6 y pruebas unitarias.

La arquitectura del proyecto está basada en clean code en donde se divide de la siguiente manera:

=>Serverless

    =>Proyecto serverless => Mutant

    =>Proyecto test => Mutant.Test
    
=>Core

    =>Domain
        =>Entities
        =>DTOS
        
    =>Application
        =>Services
        =>Interfaces
        
=>Infraestructure

    =>Persitence
        =>Contracts
        =>Repositories

La arquitectura de la infraestructura es una función serverless publicada en una aplicación lambda que implementa una función lambda de aws, conectado a DynamoDB, exponiendo sus apis a través de ApiGateway y con un load balancer.

Las pruebas unitarias se realizaron en NUnit con una cobertura del 92.3% del código desarrollado.
https://sergiocastrogu.github.io/HumanOrMutant/index.html


Colección de postman donde se encuentra el llamado al api gateway.
https://super-mutant.s3.amazonaws.com/Mutant.postman_collection

Documentacion swagger
https://github.com/sergiocastrogu/HumanOrMutant/blob/master/Serverless/Mutant/swaggerdoc.json

Url de la api
https://ef5uyese57.execute-api.us-east-1.amazonaws.com/Prod


/stats GET
https://ef5uyese57.execute-api.us-east-1.amazonaws.com/Prod/api/mutant/stats


/mutant POST
https://ef5uyese57.execute-api.us-east-1.amazonaws.com/Prod/api/mutant/mutant

JSON
{
  "dna": [
    "ABAAGA",
    "AGAAAA",
    "CGTGAA",
    "TCCAAG",
    "GGCCTT",
    "TTGGCC"
  ]
}