﻿using doLittle.Read;
using doLittle.Read.Validation;
using doLittle.Rules;
using doLittle.Security;
using Machine.Specifications;

namespace doLittle.Specs.Read.for_QueryCoordinator.given
{
    public class a_query_coordinator : all_dependencies
    {
        protected static QueryCoordinator coordinator;
        protected static QueryValidationResult validation_result;

        Establish context = () =>
        {
            fetching_security_manager.Setup(f => f.Authorize(Moq.It.IsAny<IQuery>())).Returns(new AuthorizationResult());
            validation_result = new QueryValidationResult(new BrokenRule[0]);
            query_validator.Setup(q => q.Validate(Moq.It.IsAny<IQuery>())).Returns(validation_result);
            
            coordinator = new QueryCoordinator(
                type_finder.Object, 
                container.Object, 
                fetching_security_manager.Object, 
                query_validator.Object,
                read_model_filters.Object,
                exception_publisher.Object);
        };
    }
}
