using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quartz;
using Quartz.Impl;
using Quartz.Job;
using System.Diagnostics;

namespace QuartzEmail
{
    public class Program
    {
        //private static void Main(string[] args)
        public static void Start()

        {
            try
            {
                Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter
                {
                    Level = Common.Logging.LogLevel.Info
                };

                // Grab the Scheduler instance from the Factory 
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                Debug.WriteLine("step 1");
                // and start it off
                scheduler.Start();

                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<EmailJob>()
                    //.WithIdentity("EmailJob", "group1")
                    .Build();
                Debug.WriteLine("step 2");

                ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity("EmailJob", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(240)
                        .RepeatForever())
                    .Build();
                Debug.WriteLine("step 3");
                
                // Trigger the job to run at 9 am every day
                //ITrigger trigger = TriggerBuilder.Create()
                //    .WithIdentity("trigger1", "group1")
                //    .ForJob("EmailJob")
                //    .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(9, 0))
                //    .Build();

                // Tell quartz to schedule the job using our trigger
                scheduler.ScheduleJob(job, trigger);

                Debug.WriteLine("Job scheduled");
                // some sleep to show what's happening
                //Thread.Sleep(TimeSpan.FromSeconds(60));

                // and last shut down the scheduler when you are ready to close your program
                //scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }

        }

    }
}
