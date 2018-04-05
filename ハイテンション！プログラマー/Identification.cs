using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ハイテンション_プログラマー
{
    public static class Identification
    {
        public static string Code { get; private set; }

        public static List<Command> Commands { get; private set; }

        public static readonly ReadOnlyCollection<string> ReservedWord =
            Array.AsReadOnly(new string[]
            {
                "つぶやく！"
            });

        public static ProgramScope Scope { get; set; }

        public static void Init(string code)
        {
            Code = code;
            Commands = new List<Command>();

            foreach(var command in code.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                string name = null;
                List<string> arg = new List<string>();

                foreach (var commandArg in command.Split(' ').ToList())
                {
                    //コマンドなし
                    if (commandArg == "")
                        continue;

                    if (name == null)
                        name = commandArg;
                    else
                        arg.Add(commandArg);
                }
                if(name != null)
                {
                    Commands.Add(new Command(name, arg.ToArray()));
                }
            }
        }
    }

    public class Command
    {
        public string CommandName { get; private set; }
        public string[] Arg { get; private set; }

        public Command(string name, params string[] arg)
        {
            CommandName = name;
            Arg = arg;
        }
    }

    public class ProgramScope
    {
        public ScopeType ScopeType { get; set; }
        public object Arg { get; set; }

        public ProgramScope(ScopeType scopeType, object arg)
        {
            ScopeType = ScopeType;
            Arg = arg;
        }
    }

    public enum ScopeType
    {
        Global,
        InUsersFunc_Run,
        InUsersFunc_Define
    }
}
