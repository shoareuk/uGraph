using Our.Umbraco.uGraph.BackOffice.DataAccess;
using Our.Umbraco.uGraph.BackOffice.Interfaces;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Mapping;

namespace Our.Umbraco.uGraph.BackOffice.Components
{
    public class UGraphUserComposer: IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<UGraphComponent>();

            composition.Register<IUGraphDataAccess, UGraphDataAccess>();

            composition.WithCollectionBuilder<MapDefinitionCollectionBuilder>().Add<UGraphMapDefinitions>();
        }
    }
}