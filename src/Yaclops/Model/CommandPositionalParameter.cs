﻿

namespace Yaclops.Model
{
    internal class CommandPositionalParameter
    {
        public CommandPositionalParameter(string propertyName, bool isList, bool isRequired)
        {
            PropertyName = propertyName;
            IsList = isList;
            IsRequired = isRequired;
        }

        public string PropertyName { get; private set; }
        public bool IsList { get; private set; }
        public bool IsRequired { get; private set; }
    }
}