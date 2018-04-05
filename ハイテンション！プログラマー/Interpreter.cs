using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ハイテンション_プログラマー
{
    public static class Interpreter
    {
        public static Dictionary<string, Data> values = new Dictionary<string, Data>();

        public static Dictionary<string, int> labels = new Dictionary<string, int>();

        public static void Define()
        {
            Identification.Scope = new ProgramScope(ScopeType.Global, null);

            for (int line = 0; line < Identification.Commands.Count; line++)
            {
                Command command = Identification.Commands[line];

                switch (command.CommandName)
                {
                    //分岐操作
                    case "ここに来たことある！":
                        {
                            if (command.Arg.Length == 1)
                            {
                                labels.Add(command.Arg[0], line);
                            }
                            else
                            {
                                ProgramException.ThrowException(ExceptionType.ArgumentCountDifferent, line);
                            }
                        }
                        break;
                }
            }
        }

        public static int Run()
        {
            int line = 0;

            Identification.Scope = new ProgramScope(ScopeType.Global, null);

            for(; line < Identification.Commands.Count; )
            {
                Command command = Identification.Commands[line];

                switch (command.CommandName)
                {
                    case "文書保存":
                        {
                            if (command.Arg.Length == 2)
                            {
                                if(CheckData(values, command.Arg[0], typeof(string)))
                                {
                                    values[command.Arg[0]] = 
                                        new Data(typeof(string), command.Arg[1]);
                                }
                                else
                                {
                                    values.Add(
                                        command.Arg[0],
                                        new Data(typeof(string), command.Arg[1])
                                        );
                                }
                            }
                            else
                            {
                                ProgramException.ThrowException(ExceptionType.ArgumentCountDifferent, line);
                            }
                        }
                        break;
                        
                        //ファイル入出力
                    case "(..)φメモメモ":
                        {
                            if (command.Arg.Length == 2)
                            {
                                if (CheckData(values, command.Arg[0], typeof(string)) &&
                                    CheckData(values, command.Arg[1], typeof(string)))
                                {
                                    System.IO.File.WriteAllText(
                                        values[command.Arg[0]].Item.ToString(),
                                        values[command.Arg[1]].Item.ToString()
                                        );
                                }
                                else
                                {
                                    ProgramException.ThrowException(ExceptionType.ArgumentIsDifferent, line);
                                }
                            }
                            else
                            {
                                ProgramException.ThrowException(ExceptionType.ArgumentCountDifferent, line);
                            }
                        }
                        break;

                        //入出力
                    case "つぶやく！":
                        {
                            if(command.Arg.Length == 1)
                            {
                                if (CheckData(values, command.Arg[0], typeof(string)) ||
                                    CheckData(values, command.Arg[0], typeof(int)) ||
                                    CheckData(values, command.Arg[0], typeof(float)))
                                {
                                    Console.WriteLine(values[command.Arg[0]].Item);
                                }
                                else
                                {
                                    ProgramException.ThrowException(ExceptionType.ArgumentIsDifferent, line);
                                }
                            }
                            else
                            {
                                ProgramException.ThrowException(ExceptionType.ArgumentCountDifferent, line);
                            }
                        }
                        break;

                        //分岐操作
                    case "思い出の場所へ":
                        {
                            if (command.Arg.Length == 1)
                            {
                                if (CheckData(labels, command.Arg[0]))
                                {
                                    line = labels[command.Arg[0]];
                                    continue;
                                }
                                else
                                {
                                    ProgramException.ThrowException(ExceptionType.ArgumentIsDifferent, line);
                                }
                            }
                            else
                            {
                                ProgramException.ThrowException(ExceptionType.ArgumentCountDifferent, line);
                            }
                        }
                        break;

                        //定義済み
                    case "ここに来たことある！":
                        {

                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
                line++;
            }

            return 0;
        }

        private static bool CheckData(Dictionary<string, Data> pairs, string name, Type type)
        {
            return pairs.ContainsKey(name) &&
                pairs[name].Type == type;
        }
        private static bool CheckData(Dictionary<string, int> pairs, string name)
        {
            return pairs.ContainsKey(name);
        }
    }

    public class Data
    {
        public Type Type { get; set; }
        public object Item { get; set; }

        public Data(Type type, object item)
        {
            Type = type;
            Item = item;
        }
    }
}
