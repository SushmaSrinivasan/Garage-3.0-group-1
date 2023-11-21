using Garage3.Core.Entities;
using Garage3.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Web.Validations
{
    public class CheckFnameLname: ValidationAttribute
    {
       // private readonly string? _firstName;
        //private readonly string? _lastName;
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            const string? errorMessage = "firstName and lastName are equal";
            if(value is string input)
            {
                if(validationContext.ObjectInstance is AddMemberViewModel model)
                {
                return model.FirstName != model.LastName ?
                        ValidationResult.Success :
                        new ValidationResult(errorMessage);
                }


            }
            return new ValidationResult(errorMessage);
        }
    }
}
