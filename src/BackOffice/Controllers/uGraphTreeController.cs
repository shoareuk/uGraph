using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http.ModelBinding;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web.Actions;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Mvc;
using Umbraco.Web.Trees;
using Umbraco.Web.WebApi.Filters;

using Constants = Umbraco.Core.Constants;

namespace Our.Umbraco.uGraph.BackOffice.Controllers
{
    [Tree("uGraph", "uGraphTree", TreeTitle = "uGraph Config", TreeGroup = "uGraph", SortOrder = 1)]
    [PluginController("uGraph")]
    public class uGraphTreeController: TreeController
    {
        private const string DocTypesRootNodeId = "UGRAPH-ROOT-DOCTYPES";

        protected override TreeNodeCollection GetTreeNodes(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormDataCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();

            // check if we're rendering the root node's children
            if (id == Constants.System.Root.ToInvariantString())
            {
                nodes.Add(CreateTreeNode(DocTypesRootNodeId, "-1", queryStrings, "DocTypes", "icon-item-arrangement", true));
            }
            else
            {
                if (id.Equals(DocTypesRootNodeId, StringComparison.OrdinalIgnoreCase))
                    id = "-1";

                // THIS CODE TAKEN FROM Umbraco.Web.Trees.ContentTypeTreeController 
                var intId = id.TryConvertTo<int>();

                if (intId == false)
                    throw new InvalidOperationException("Id must be an integer");

                nodes.AddRange(
                Services.EntityService.GetChildren(intId.Result, UmbracoObjectTypes.DocumentTypeContainer)
                    .OrderBy(entity => entity.Name)
                    .Select(dt =>
                    {
                        var node = CreateTreeNode(dt.Id.ToString(), id, queryStrings, dt.Name, "icon-folder", dt.HasChildren, "");

                        node.Path = dt.Path;
                        node.NodeType = "container";
                        
                        // TODO: This isn't the best way to ensure a no operation process for clicking a node but it works for now.
                        node.AdditionalData["jsClickCallback"] = "javascript:void(0);";

                        return node;
                    }));

                //if the request is for folders only then just return
                if (queryStrings["foldersonly"].IsNullOrWhiteSpace() == false && queryStrings["foldersonly"] == "1")
                    return nodes;

                nodes.AddRange(
                    Services.EntityService.GetChildren(intId.Result, UmbracoObjectTypes.DocumentType)
                        .OrderBy(entity => entity.Name)
                        .Select(dt =>
                        {
                            // since 7.4+ child type creation is enabled by a config option. It defaults to on, but can be disabled if we decide to.
                            // need this check to keep supporting sites where children have already been created.
                            var hasChildren = dt.HasChildren;
                            var node = CreateTreeNode(dt, Constants.ObjectTypes.DocumentType, id, queryStrings, Constants.Icons.ContentType, hasChildren);

                            node.Path = dt.Path;

                            return node;
                        }));
            }

            return nodes;
        }

        protected override MenuItemCollection GetMenuForNode(string id, [ModelBinder(typeof(HttpQueryStringModelBinder))] FormDataCollection queryStrings)
        {
            return null;
        }
    }
}
