using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ハイテンション_プログラマー
{
    class MainProgram
    {
        public static int Main(string[] arg)
        {
            if(arg.Length == 0)
            {
                Console.WriteLine("そのような引数を取るオプションはないよ('ω')");
                return -1;
            }

            string code = null;

            try
            {
                code = File.ReadAllText(arg[0], Encoding.Unicode);
            }
            catch(Exception e)
            {
                ProgramException.ThrowException(ExceptionType.NoExecutableFile, -1);
            }

            Identification.Init(code);

            Interpreter.Define();

            var exitCode = Interpreter.Run();

            Console.ReadKey();

            return exitCode;
        }
    }
}
