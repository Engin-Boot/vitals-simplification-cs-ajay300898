using System;
using System.Collections.Generic;
using System.Diagnostics;

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
    public class Limits
    {
        public int lowLimit;
        public int highLimit;
        public Limits(int low,int high)
        {
            this.lowLimit = low;
            this.highLimit = high;
        }

        public bool IsInRange(int value)
        {
            return (value >= lowLimit && value <= highLimit);
        }
        public bool IsLessThan(int value)
        {
            return value < lowLimit;
        }
    }
    public class VitalInformation
    {
        public static Dictionary<string,Limits> vitalInformation = new Dictionary<string,Limits>();
        static VitalInformation()
        {
            vitalInformation.Add("bp", new Limits(70, 150));
            vitalInformation.Add("spo2", new Limits(90, 100));
            vitalInformation.Add("respRate", new Limits(30, 95));
        }
        public static bool AddNewVitalInfo(string vName,int low,int high)
        {
            if (vitalInformation.ContainsKey(vName))
            {
                Console.WriteLine("Vital already Exists...!!");
                return false;
            }
            vitalInformation.Add(vName, new Limits(low, high));
            return true;
        }
    }
    
    public class Vital
    {
        public string vitalName;
        public int value;
        public Vital(string vName, int value)
        {
            this.vitalName = vName;
            this.value = value;
        }
    }
    public class PatientVitalInfo
    {
        public string patientName;
        public List<Vital> patientVitalInfo = new List<Vital>();
        public PatientVitalInfo(string patientName,List<Vital> vInfo)
        {
            this.patientName = patientName;
            this.patientVitalInfo = vInfo;
        }
        public PatientVitalInfo(string patientName)
        {
            this.patientName = patientName;
        }
        public void AddPatientVitalInfo(string vName, int value)
        {
            patientVitalInfo.Add(new Vital(vName, value));
        }
    }
    public class Checker
    {
        static Alert alert;
        static Checker()
        {
            alert = new AlertInSMS();
        }
        static bool AreVitalsOk(PatientVitalInfo patientVitals)
        {
            bool result = true;
            foreach(Vital v in patientVitals.patientVitalInfo)
            {
                if (VitalInformation.vitalInformation.ContainsKey(v.vitalName))
                {
                    result &= isVitalOk(patientVitals.patientName, v.vitalName, v.value);
                          
                }
                else
                {
                    Console.WriteLine("Invalid vital {0}", v.vitalName);
                }
            }
            return result;
        }
        static bool isVitalOk(string patientName, string vitalName, int value)
        {
            if (!VitalInformation.vitalInformation[vitalName].IsInRange(value))
            {
                string alertMsg = patientName + " " + vitalName + " is";
                if (VitalInformation.vitalInformation[vitalName].IsLessThan(value))
                {
                    alertMsg += " BELOW than the normal value";
                }
                else
                {
                    alertMsg += " ABOVE than the normal value";
                }
                alert.RaiseAlert(alertMsg);
                return false;
            }
            return true;
        }
        static void ExpectTrue(bool expression)
        {
            if (!expression)
            {
                Console.WriteLine("Expected true, but got false");
                Environment.Exit(1);
            }
        }
        static void ExpectFalse(bool expression)
        {
            if (expression)
            {
                Console.WriteLine("Expected false, but got true");
                Environment.Exit(1);
            }
        }
        static int Main()
        {
            
            PatientVitalInfo patient1 = new PatientVitalInfo("Mukesh",new List<Vital>{ new Vital("bp",100), new Vital("spo2",95), new Vital("respRate",60)});
            PatientVitalInfo patient2 = new PatientVitalInfo("Suresh", new List<Vital> { new Vital("bp", 40), new Vital("spo2", 85), new Vital("respRate", 100) });
            PatientVitalInfo patient3 = new PatientVitalInfo("Rajesh", new List<Vital> { new Vital("bp", 155), new Vital("spo2", 85), new Vital("respRate", 20) });
            ExpectTrue(AreVitalsOk(patient1));
            ExpectFalse(AreVitalsOk(patient2));
            alert = new AlertInSound();
            ExpectFalse(AreVitalsOk(patient3));
            Console.WriteLine("All ok");
            return 0;
        }
    }
}
