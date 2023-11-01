using Miro.Shared.AuthenticationModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miro.Shared.Validation
{
    public class UserValidation : IValidation<RegisterModel>
    {
        public bool IsValid { get; private set; }

        public ICollection<string> ValidationErrors { get; }

        public void Validate(RegisterModel registerModel)
        {
            if (ValidationErrors != null)
                ValidationErrors.Clear();

            if (string.IsNullOrWhiteSpace(registerModel.UserName))
            {
                IsValid = false;
                ValidationErrors.Add("Username is required.");
            }

            if (string.IsNullOrEmpty(registerModel.Password))
            {
                IsValid = false;
                ValidationErrors.Add("Email is required.");
            } 

            if (string.IsNullOrWhiteSpace(registerModel.Email))
            {
                IsValid = false;
                ValidationErrors.Add("Email is required.");
            };
        }
    }
}
