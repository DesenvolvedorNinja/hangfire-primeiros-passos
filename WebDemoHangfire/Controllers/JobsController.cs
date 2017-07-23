using System;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using System.Diagnostics;

namespace WebDemoHangfire.Controllers
{
    [Route("api/[controller]")]
    public class JobsController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            Console.WriteLine($"Request: {DateTime.Now}");
            var jobFireForget = BackgroundJob.Enqueue(() => Debug.WriteLine($"Fire and forget: {DateTime.Now}"));
            var jobDelayed = BackgroundJob.Schedule(() => Debug.WriteLine($"Delayed: {DateTime.Now}"), TimeSpan.FromSeconds(30));
            BackgroundJob.ContinueWith(jobDelayed, () => Debug.WriteLine($"Continuation: {DateTime.Now}"));
            RecurringJob.AddOrUpdate(() => Debug.WriteLine($"Recurring: {DateTime.Now}"), Cron.Minutely);
            return "Jobs criados com sucesso!";
        }
    }
}
