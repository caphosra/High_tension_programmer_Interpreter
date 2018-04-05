using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ハイテンション_プログラマー
{
    public static class ProgramException
    {
        public static void ThrowException(ExceptionType exception, int line)
        {
            string exceptionType = Enum.GetName(typeof(ExceptionType), exception);
            Console.WriteLine($"例外が発生したよ( ﾟДﾟ) {exceptionType} 行:{line}");
            Console.WriteLine("プログラムを終了するね(>_<)");
            Console.ReadLine();
            Environment.Exit(-1);
        }
    }

    public enum ExceptionType
    {
        NoExecutableFile,
        CanNotBeExecuted,

        ArgumentCountDifferent,
        ArgumentIsDifferent
    }
}
