namespace testing_app.Data
{
    public class SiteSchedule
    {
        //BELOW IS ALL THE FIELDS (COLUMNS) FOR SITESCHEDULE, WHICH IS 1 ROW IN THE SITE SCHEDULES TABLE
        //----------------------------------------------------------------------------------------------
        public int Id { get; set; }
        public string Frequency
        {
            get{
                if (CronExpression == null)
                {
                    return "";
                }
                return frequencyForCron(CronExpression);
            }
        }
        public string? IntegrationName { get; set; }
        public string? Description { get; set; }
        public string? SiteName { get; set; }
        public string? CronExpression { get; set; }
        public bool Enabled { get; set; }
        public string? ConfigFilePath { get; set; }
        public bool RunAllScheduled { get; set; }
        public string? CustomerName { get; set; }
        public string? AppServiceName { get; set; }
        public int rowIndex {
            get{
                return counter;
            }
            set{
                counter++;
            }
        }
        public int counter = 0;

        //BELOW IS ALL THE LOGIC TO CONVERT A CRON EXPRESSION TO WORDS
        //-------------------------------------------------------------
        public string timeExpressionBuilder(string minutes, string hours) //this method builds a time in the form "X:XX"
        {
            return (hours + ":" + minutes);
        }
        
        //!FREQUENCY FOR CRON HAS ONLY LIMITED FUNTIONALITY!
        public string frequencyForCron(string cronExpression)
        {
            bool allMins = false, allHours = false; //ex. all mins would mean the expression would be of the form * X X X X
            string[] elements = cronExpression.Split(' '); //elements is each of the 5-6 fields that a cron expression can contain

            //determine if it is all time (or all hours/all minutes)
            if(elements[0] == "*")
            {
                allMins = true;
            }
            if (elements[1] == "*")
            {
                allHours = true;
            }
            
            //if there are no date restrictions
            if(elements[2] == "*" && elements[3] == "*" && elements[4] == "*")
            {
                if(allHours && allMins)
                {
                    return "Every minute";
                }

                else if(allHours && !allMins)
                {
                    return "Every " + elements[0] + " mins past the hour";
                }

                else if(!allHours && allMins)
                {
                    return "Every minute between " + elements[1] + ":00 and " + elements[1] + ":59";
                }
                else
                {
                    return "At " + timeExpressionBuilder(elements[0], elements[1]) + " daily";
                }
            }
            //no month or day of month restrictions
            else if(elements[2] == "*" && elements[3] == "*" && elements[4] != "*")
            {
                if(allHours && allMins)
                {
                    return "Every minute every " + elements[4];
                }

                else if(allHours && !allMins)
                {
                    return "Every " + elements[0] + " mins past the hour";
                }
                else if(!allHours && allMins) 
                {
                    return "Every minute between " + elements[1] + ":00 and " + elements[1] + ":59" + " on " + elements[4];
                }
                else
                {
                    return "At " + timeExpressionBuilder(elements[0], elements[1]) + " on " + elements[4];
                }

            }
            //there is a month condition and nothing else
            else if(elements[2] != "*" && elements[3] == "*" && elements[4] == "*")
            {
                string[] daysOfMonth = listDeterminer(elements[2]);
                if(allHours && allMins)
                {
                    return "Every minute on days " + elements[2] + " of the month";
                   
                }

                else if(allHours && !allMins)
                {
                    return "Every " + elements[0] + " mins past the hour on days " + elements[2] + " of the month";
                }
                else if(!allHours && allMins) 
                {
                    return "Every minute between " + elements[1] + ":00 and " + elements[1] + ":59" + " on days " + elements[2] + " of the month";
                }
                else
                {
                    return "At " + timeExpressionBuilder(elements[0], elements[1]) + " on days " + elements[2] + " of the month";
                }
            }
            return "error";
        }
        //this method is used if an element in the cron expression has more than one character that is seperated by commans (ex. 30 05 8,23 * * )
        public string[] listDeterminer(string element)
        {
            string[] items = element.Split(',');
            return items;
        }
    }
}
