using System;
using Machine.Specifications;

namespace Dolittle.Artifacts.for_ArtifactTypeToTypeMaps
{

    public class when_mapping_to_type_with_existing_map : given.one_map
    {
        static Type result;
        
        Because of = () => result = maps.Map(artifact_type_map);

        It should_return_correct_type = () => result.ShouldEqual(typeof(IUnderlyingArtifact));
    }        
    
}