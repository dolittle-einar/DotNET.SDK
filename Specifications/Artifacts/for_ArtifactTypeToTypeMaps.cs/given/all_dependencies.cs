using Dolittle.DependencyInversion;
using Dolittle.Types;
using Machine.Specifications;
using Moq;

namespace Dolittle.Artifacts.for_ArtifactTypeToTypeMaps.given
{
    public class all_dependencies
    {
        protected static Mock<ITypeFinder> type_finder;
        protected static Mock<IContainer> container;

        Establish context = ()=>
        {
            type_finder = new Mock<ITypeFinder>();
            container = new Mock<IContainer>();
        };
    }
}