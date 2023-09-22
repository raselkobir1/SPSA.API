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
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        instance ??= new Singleton();
                    }
                }
                return instance;
            }
        }
        public string SomeMethod(string message) => $"Hello! {message}";
    }
}
