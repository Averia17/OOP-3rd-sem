using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab06
{
    public class EmptyException : Exception
    {
        public EmptyException(string mess) : base(mess) { }
        public void GetInfo()
        {
            Console.WriteLine("CustomException: " + this.Message);
            Console.WriteLine(this.StackTrace);
            Console.WriteLine(this.TargetSite);
        }
    }
    public class PriceException : Exception
    {
        public PriceException(string mess) : base(mess) { }
        public void GetInfo()
        {
            Console.WriteLine("CustomException: " + this.Message);
            Console.WriteLine(this.StackTrace);
            Console.WriteLine(this.TargetSite);
        }
    }
    public class RangeException : Exception
    {
        public RangeException(string mess) : base(mess) { }
        public void GetInfo()
        {
            Console.WriteLine("CustomException: " + this.Message);
            Console.WriteLine(this.StackTrace);
            Console.WriteLine(this.TargetSite);
        }
    }
    public class IndexException : Exception
    {
        public IndexException(string mess) : base(mess) { }
        public void GetInfo()
        {
            Console.WriteLine("CustomException: " + this.Message);
            Console.WriteLine(this.StackTrace);
            Console.WriteLine(this.TargetSite);
        }
    }
}