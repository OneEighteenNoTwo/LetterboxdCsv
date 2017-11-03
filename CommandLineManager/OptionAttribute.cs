using System;

namespace CommandLineManager
{
    [AttributeUsage(AttributeTargets.All)]
    public class OptionAttribute : Attribute
    {
        public readonly string shortCmd;
        public readonly string longCmd;
        private  bool required;

        public virtual bool Required
        {
            get
            {
                return required;
            }
            set
            {
                required = value;
            }
        }
        public OptionAttribute(string shortCmd, string longCmd)
        {
            this.shortCmd = shortCmd;
            this.longCmd = longCmd;
            this.required = false;
        }
    }
}
