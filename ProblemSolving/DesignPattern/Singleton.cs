using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolving.DesignPattern
{
    public sealed class Singleton 
    {
        private static Singleton instance;
        private static readonly object lockObject = new object();
        private Singleton() { }

        public static Singleton Instance
        {
            get
            {
                // Double-check locking for thread safety
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton();
                        }
                    }
                }
                return instance;
            }
        }

        public void SomeMethod(string message)
        {
            Console.WriteLine($"Singleton instance method called.  {message}");
        }
    }
}
