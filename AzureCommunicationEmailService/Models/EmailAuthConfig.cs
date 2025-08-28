using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureCommunicationEmailService.Models
{
    internal class EmailAuthConfig
    {
        private object? _value;

        public EmailAuthConfig(ValueType valueType)
        {
            ValType = valueType;

        }

        public enum ValueType
        {
            String,
            Integer,
            Boolean
        }

        public string Name { get; set; }

        public object? Value
        {
            get { return _value; }
            set
            {
                switch (ValType)
                {
                    case ValueType.String:
                        SetStringValue(value);
                        break;
                    case ValueType.Integer:
                        SetIntegerValue(value);
                        break;
                    case ValueType.Boolean:
                        SetBooleanValue(value);
                        break;
                }
            }
        }

        public ValueType ValType { get; private set; }

        public bool IsReadOnly { get; set; }

        public bool IsPassword { get; set; }

        public List<int> AuthTypes { get; set; }

        public string Notes { get; set; }

        private void SetIntegerValue(object? value)
        {
            int intValue;

            if (value == null)
            {
                _value = null;
            }
            else if (int.TryParse(value.ToString(), out intValue) == false)
            {
                _value = null;
            }
            else
            {
                _value = intValue;
            }
        }

        public int? GetIntegerValue()
        {
            if (ValType != ValueType.Integer)
            {
                return null;
            }
            if (Value == null)
            {
                return null;
            }

            return (int)Value;
        }

        private void SetBooleanValue(object? value)
        {
            bool boolValue;

            if (value == null || bool.TryParse(value.ToString(), out boolValue) == false)
            {
                _value = false;
            }
            else
            {
                _value = boolValue;
            }
        }

        public bool? GetBooleanValue()
        {
            if (ValType != ValueType.Boolean)
            {
                return null;
            }
            if (Value == null)
            {
                return null;
            }

            return (bool)Value;
        }

        private void SetStringValue(object? value)
        {
            _value = value;
        }

        public string? GetStringValue()
        {
            if (ValType != ValueType.String)
            {
                return null;
            }
            if (Value == null)
            {
                return null;
            }

            return (string)Value;
        }

        public string GetMaskedValue()
        {
            return (Value != null) ? new string('●', Value.ToString().Length) : "";
        }
    }
}
