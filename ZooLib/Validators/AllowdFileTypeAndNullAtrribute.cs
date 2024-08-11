using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ZooLib.Validators
{
    public class AllowdFileTypeAndNullAttribute : ValidationAttribute
    {
        private readonly string[]? _allowedFileType;

        public AllowdFileTypeAndNullAttribute(string[]? allowedFileType)
        {
            _allowedFileType = allowedFileType;
        }

        public override bool IsValid(object? value)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName); // getting the .png/.jpeg whatever it is
                if (string.IsNullOrEmpty(extention) || Array.IndexOf(_allowedFileType!, extention.ToLower()) < 0)
                    return false;
                return true;
            }
            return true;
        }
    }
}
