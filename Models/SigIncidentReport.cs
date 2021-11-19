using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapierBankMessaging.Models
{
    class SigIncidentReport : Email
    {
        private string dateOfIn { get; set; }
        private string sortCode { get; set; }
        private string natureOfIncident { get; set; }


        public SigIncidentReport(string header, string body, string sender, string date, string sortC, string natureOI) : base(header, body, sender)
        {
            dateOfIn = date;
            sortCode = sortC;
            natureOfIncident = natureOI;
        }
    }
}
