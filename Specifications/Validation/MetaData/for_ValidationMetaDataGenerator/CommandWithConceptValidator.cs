﻿using Dolittle.FluentValidation.Commands;
using Dolittle.FluentValidation.Specs.for_ValidationMetaDataGenerator;
using FluentValidation;

namespace Dolittle.FluentValidation.Specs.MetaData.for_ValidationMetaDataGenerator
{
    public class CommandWithConceptValidator : CommandInputValidator<CommandWithConcept>
    {
        public CommandWithConceptValidator()
        {
            RuleForConcept(o => o.StringConcept)
                .NotNull()
                .SetValidator(new ConceptAsStringValidator());
            RuleForConcept(o => o.LongConcept)
                .NotNull()
                .SetValidator(new ConceptAsLongValidator());
            RuleFor(o => o.NonConceptObject)
                .NotNull()
                .SetValidator(new ObjectValidator());
        }
    }
}
