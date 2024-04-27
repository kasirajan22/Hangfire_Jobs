using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire_Jobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        [HttpGet("FireAndForgetJob")]
        public ActionResult CreateFireAndForgetJob()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("hello testing hangfire FireAndForgetJob"));
            return Ok();
        }

        [HttpGet("DelayedJob")]
        public ActionResult CreateDelayedJob()
        {
            BackgroundJob.Schedule(() => Console.WriteLine("hello testing hangfire DelayedJob"), TimeSpan.FromSeconds(60));
            return Ok();
        }

        [HttpGet("ContinuationJob")]
        public ActionResult CreateContinuasJob()
        {
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("hello testing hangfire ContinuationJob"));
            BackgroundJob.ContinueJobWith(jobId,() => Console.WriteLine("Continuation!"));
            return Ok();
        }

        [HttpGet("RecurringJob")]
        public IActionResult recurringJob()
        {
            RecurringJob.AddOrUpdate("myrecurringjob", () => Console.WriteLine("hello testing hangfire RecurringJob"), Cron.Minutely());
            //sendmail();
            return Ok("recurring job is done");
        }

        // [HttpGet("BatchJob")]
        // public IActionResult BatchJobs()
        // {
        //     var batchId = BackgroundJob.StartNew(x =>
        //     {
        //         x.Enqueue(() => Console.WriteLine("BatchJob Job 1"));
        //         x.Enqueue(() => Console.WriteLine("BatchJob Job 2"));
        //     });
        //     return Ok("BatchJob is done");
        // }
    }

    
}
