using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace meditatii.web.ScheduledTasks
{
    public class JobScheduler
    {
        public static async Task Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<SendAppoitmentNotificationJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                  .StartNow()
                  .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(3600)
                    .RepeatForever())
               .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public static void PingServer()
        {
            try
            {
                WebClient http = new WebClient();
                string Result = http.DownloadString("https://www.emeditatii.ro/");
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
            }
        }
    }
}