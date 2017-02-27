using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hrm_v5
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FechaMayor : ValidationAttribute
    {
        public FechaMayor(string dateToCompareToFieldName)
        {
            DateToCompareToFieldName = dateToCompareToFieldName;
        }

        public string DateToCompareToFieldName { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            DateTime earlierDate = (DateTime)value;
            DateTime laterDate = (DateTime)validationContext.ObjectType.GetProperty(DateToCompareToFieldName).GetValue(validationContext.ObjectInstance, null);

            if (laterDate < earlierDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("La Fecha Final debe ser posterior a la inicial.");
            }

        }
    }
}