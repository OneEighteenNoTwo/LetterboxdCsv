using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace CommandLineManager
{
    public class CLArgumentParser {
        public void ParseCommandLineArguments(string[] args,  object commandLineOptions)
        {
            var properties = commandLineOptions.GetType().GetTypeInfo().GetProperties();
            foreach(var prop in properties)
            {
                OptionAttribute attribute = prop.GetCustomAttribute<OptionAttribute>();
                if (attribute == null) continue;
                var indexOf = Array.FindIndex(args, arg => arg == $"-{attribute.shortCmd}");
                string value = String.Empty;
                if (indexOf >= 0)
                {
                    var isLastArgument = indexOf == args.Length - 1;
                    var noOptions = isLastArgument ? true : args[indexOf + 1][0] == '-';
                    if (isLastArgument || noOptions)
                    {
                        value = String.Empty;
                    }
                    else
                    {
                        value = args[indexOf + 1];
                    }

                    prop.SetValue(commandLineOptions, Convert.ChangeType(value, prop.PropertyType), null);
                }
                else if (attribute.Required)
                {
                    throw new MissingCommandLineArgumentException($"Required Parameter {attribute.shortCmd} not found.");
                }

            }
        }

    }
}
