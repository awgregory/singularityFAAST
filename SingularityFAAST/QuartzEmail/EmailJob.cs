using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using SingularityFAAST.Services.Services;


namespace QuartzEmail
{
    //class EmailJob
    //{
    //    {
    //The Job listener
    public class EmailJob : IJob
        {
            public void Execute(IJobExecutionContext context)
            {
                LoanMasterServices lm_services = new LoanMasterServices();
                lm_services.NotifyEmail();
                Console.WriteLine("Call Email");
                Debug.WriteLine("Call Email");

            }
        }
    }

