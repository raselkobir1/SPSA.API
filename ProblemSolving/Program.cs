using ProblemSolving.DesignPattern;
using ProblemSolving.LeetCode;

//Singleton object1 = Singleton.Instance;
//Singleton object2 = Singleton.Instance;
//object1.SomeMethod("object one");
//object2.SomeMethod("object two");

//Console.WriteLine(object1);
//Console.WriteLine(object2);

ValidParentheses x = new();
var res = x.IsValid("x(){}x");
Console.ReadLine(); 