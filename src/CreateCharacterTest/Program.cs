using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CreateCharacterTest
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // Unfortunately, the only way to build test characters at this time is
            // to edit CharacterTest.tt (instructions in comments in the file).

            //if (args.Count() == 0 || args[0] == "-?" || args[0] == "/?")
            //{
            //    ShowHelp();
            //    Environment.ExitCode = 1;
            //}
            //else
            //{
            //    TestFactory testFactory;

            //    testFactory = new TestFactory();
            //    foreach (string arg in args)
            //    {
            //        try
            //        {
            //            testFactory.CreateTests(Assembly.LoadFrom(arg));
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.Error.WriteLine(string.Format("{0} : {1}", arg, ex.Message));
            //        }
            //    }
            //}
        }

        ///// <summary>
        ///// Show help.
        ///// </summary>
        //private static void ShowHelp()
        //{
        //    Console.Error.WriteLine("CreateCharacterTest <assembly1> <assembly2> <assembly3> ... <assemlbyN>");
        //}
    }
}
