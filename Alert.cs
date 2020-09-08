using System;

namespace VitalsSimplification
{
    public abstract class Alert
    {
        public abstract void RaiseAlert(string alertMsg);
        
    }

    public class AlertInSMS : Alert
    {
        public override void RaiseAlert(string alertMsg)
        {
            Console.WriteLine("Alert in SMS: {0}", alertMsg);
        }
    }
    public class AlertInSound : Alert
    {
        public override void RaiseAlert(string alertMsg)
        {
            Console.WriteLine("Alert in Soound: {0}", alertMsg);
        }
    }
}
