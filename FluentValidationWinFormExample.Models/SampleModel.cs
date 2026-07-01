using System;
using System.Collections.Generic;

namespace FluentValidationWinFormExample.Models
{
    public class SampleModel
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int? Age { get; set; }

        public double Income { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfGraduation { get; set; }

        public IEnumerable<OptionModel> Options { get; set; }

        public SubModel SubModel { get; set; }
    }
}
