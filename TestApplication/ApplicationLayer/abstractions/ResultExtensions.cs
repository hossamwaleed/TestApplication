using Microsoft.AspNetCore.Mvc;

namespace TestApplication.ApplicationLayer.abstractions;

public static class ResultExtensions
{
    public static ObjectResult ToProblem(this Result result,int StatusCode)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("you cannot add problem to successState");
        var problem = Results.Problem(statusCode: StatusCode);
        var problemDetails = problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(problem) as ProblemDetails;

        problemDetails!.Extensions = new Dictionary<string, object>
        {
            {
                "errors" , new[]{result.Error}
            }
        };
        return new ObjectResult(problemDetails);
    }
}
