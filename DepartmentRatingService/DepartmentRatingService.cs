using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DepartmentRatingService.DataBase;

namespace DepartmentRatingService
{
    public partial class DepartmentRatingService : ServiceBase
    {
        UpdateDB updateDb;
        private int eventId = 1;
        public DepartmentRatingService()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;

            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("DepartmentRating"))
            {
                System.Diagnostics.EventLog.CreateEventSource("DepartmentRating", "DepartmentRatingLog");
            }

            eventLog1.Source = "DepartmentRating";
            eventLog1.Log = "DepartmentRatingLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In onStart.");

            System.Timers.Timer timer = new System.Timers.Timer
            {
                Interval = 60000 // 60 seconds
            };
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();

            updateDb = new UpdateDB();
            updateDb.Notify += EventLogWrite;
            updateDb.Start();
        }
        
        public void EventLogWrite(string message)
        {
            eventLog1.WriteEntry(message, EventLogEntryType.Information, eventId++);
            
        }

        
        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            updateDb.Update();
        }
        protected override void OnStop()
        {
        }
    }
}
