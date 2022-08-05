namespace DemoUnitTesting.Extensions
{
    using DemoUnitTesting.Domain;

    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;

    public static class ResultExtensions
    {
        public static async Task<IActionResult> ToActionResult<TData>(this Task<TData> resultTask)
        {
            var result = await resultTask;

            if (result == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }

        public static async Task<IActionResult> ToActionResult(this Task<Result> resultTask)
        {
            var result = await resultTask;

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result);
            }

            return new OkObjectResult(result);
        }

        public static async Task<IActionResult> ToActionResult<TData>(this Task<Result<TData>> resultTask)
        {
            var result = await resultTask;

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(result);
            }

            return new OkObjectResult(result);
        }
    }
}
