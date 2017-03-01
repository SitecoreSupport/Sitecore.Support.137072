using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Pipelines.Rules.Taxonomy;

namespace Sitecore.Support.Pipelines.Rules.Taxonomy
{
    public class GetElementFolders: Sitecore.Pipelines.Rules.Taxonomy.GetElementFolders
    {
        protected override void Action(Dictionary<ID, Item> result, RuleElementsPipelineArgs args)
        {
            if (args.RuleContextFolder.Paths.FullPath == "/sitecore/client/Business Component Library/version 2/Layouts/Renderings/Resources/Rule/Rules" ||
                args.RuleContextFolder.Paths.FullPath == "/sitecore/client/Business Component Library/version 1/Layouts/Renderings/Resources/Rule/Rules")
            {
                foreach (Item item2 in from d in args.RuleContextFolder.Children select d)
                {
                    if (!result.ContainsKey(item2.ID))
                    {
                        result.Add(item2.ID, item2);
                    }
                }
            }
            else
            {
                foreach (Item item in args.Tags.Values)
                {
                    foreach (Item item2 in GetConditionFolders(item))
                    {
                        if (!result.ContainsKey(item2.ID))
                        {
                            result.Add(item2.ID, item2);
                        }
                    }
                }
            }
        }
    }
}