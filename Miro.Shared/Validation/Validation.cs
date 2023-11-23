using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Miro.Shared.Validation
{
    public class ValidationObject<T>
    {
        private readonly T _entity;

        public ValidationObject(T entity)
        {
            _entity = entity;
        }

        public ValidationObject<T> NotNull(string message = "Argument is null")
        {
            if (_entity == null) throw new Exception(message);

            return this;
        }

        public ValidationObject<T> NotEmpty(string message = "String is null or Empty")
        {
            if (string.IsNullOrEmpty(_entity?.ToString())
                    || string.IsNullOrWhiteSpace(_entity?.ToString())) throw new Exception(message);

            return this;
        }

        public ValidationObject<T> Length(int minLength)
        {
            if (Convert.ToString(_entity).Length < minLength) throw new Exception(minLength.ToString());

            return this;
        }

        public ValidationObject<T> IsFalse(string? message = null)
        {
            if (_entity.Equals(false)) throw new Exception(message);

            return this;
        }

        public ValidationObject<T> IsTrue(string? message = null)
        {
            if (_entity.Equals(true)) throw new Exception(message);

            return this;
        }

        public ValidationObject<T> Equals(T equalEntity, string? message = null)
        {
            if (Convert.ToString(_entity) != Convert.ToString(equalEntity)) throw new Exception(message);

            return this;

        }

        public ValidationObject<T> MinValue(decimal minValue, string? message = null)
        {
            if (Convert.ToInt64(_entity) < minValue) throw new Exception(message + " " + Convert.ToString(minValue));

            return this;
        }

        public ValidationObject<T> MaxValue(decimal maxValue,string? message = null)
        {
            if (Convert.ToInt64(_entity) >= maxValue) throw new Exception(message + " " + Convert.ToString(maxValue));

            return this;
        }

        public ValidationObject<T> MinLength(decimal minValue, string? message = null)
        {
            if (Convert.ToString(_entity).Length < minValue) throw new Exception();

            return this;
        }

        public ValidationObject<T> MaxLength(decimal maxValue, string? message = null)
        {
            if (Convert.ToString(_entity).Length > maxValue) throw new Exception();

            return this;
        }
        public ValidationObject<T> Regex(string regexString, string? message = null)
        {
            Regex regex = new Regex(regexString);
            if (!regex.IsMatch(Convert.ToString(_entity))) throw new Exception(message);

            return this;
        }
    }
    public static class Validate
    {
        public static ValidationObject<T> For<T>(T entity)
        {
            return new ValidationObject<T>(entity);
        }
    }
}
