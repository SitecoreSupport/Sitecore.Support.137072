using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.Rules.Taxonomy;
using Sitecore.Rules;

namespace Sitecore.Support.Pipelines.Rules.Taxonomy
{
    public class GetTags : Sitecore.Pipelines.Rules.Taxonomy.GetTags
    {
        protected override void Action(Dictionary<ID, Item> result, RuleElementsPipelineArgs args)
        {
            Assert.IsNotNull(args.RuleContextFolder, "ruleContextItem");

            if (args.RuleContextFolder.Paths.FullPath == "/sitecore/client/Business Component Library/version 2/Layouts/Renderings/Resources/Rule/Rules" ||
                args.RuleContextFolder.Paths.FullPath == "/sitecore/client/Business Component Library/version 1/Layouts/Renderings/Resources/Rule/Rules")
            {
                foreach (Item item2 in from d in args.RuleContextFolder.Children
                                       from f in d.Children
                                       select f)
                {

                    if (!result.ContainsKey(item2.ID))
                    {
                        result.Add(item2.ID, item2);
                    }

                }
            }
            else
            {
                foreach (Item item2 in from d in args.RuleContextFolder.Children
                                       from f in d.Children
                                       where (d.TemplateID == RuleIds.TagDefinitionsFolderTemplateID) && (f.TemplateID == RuleIds.TagDefinitionTemplateID)
                                       select f)
                {
                    MultilistField field = item2.Fields["Tags"];
                    foreach (Item item3 in field.GetItems())
                    {
                        if (!result.ContainsKey(item3.ID))
                        {
                            result.Add(item3.ID, item3);
                        }
                    }
                }
            }
        }
    }
}