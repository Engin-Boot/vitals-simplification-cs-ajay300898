using System;
using System.Collections.Generic;

namespace VitalsSimplification
{
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
}
