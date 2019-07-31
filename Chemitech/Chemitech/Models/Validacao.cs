using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Chemitech.Models
{
    public class Validacao
    {
        public class PasswordForteAttribute : ValidationAttribute
        {
            public PasswordForteAttribute() : base("Senha tem que conter até 8 caracteres, números, letras maiúsculas e minúsculas") { }

            private static readonly String PasswordPattern = @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{3,8})";

            private static readonly Regex PasswordRegex = new Regex(PasswordPattern);
            public override bool IsValid(object value)
            {
                String password = value.ToString();
                return PasswordRegex.IsMatch(password);
            }
        }
    }
}