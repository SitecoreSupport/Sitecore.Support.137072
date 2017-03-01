using System.Linq;
using Sitecore.Data;
using Sitecore.Pipelines.Rules.Taxonomy;
using Sitecore.Rules;
using Sitecore.Data.Items;

namespace Sitecore.Support.Pipelines.Rules.Taxonomy
{
    public class GetContextFolder : Sitecore.Pipelines.Rules.Taxonomy.GetContextFolder

    {
        public override void Process(RuleElementsPipelineArgs args)
        {
            if (args.RuleContextFolder == null)
            {
                Database contentDatabase = Client.ContentDatabase;
                Item item = contentDatabase.GetItem(args.ContextItemId);
                if (args.RulesPath.ToLowerInvariant().StartsWith("query:"))
                {
                    string query = args.RulesPath.Substring("query:".Length);
                    args.RuleContextFolder = item.Axes.SelectSingleItem(query);
                }
                else
                {
                    args.RuleContextFolder = contentDatabase.GetItem(args.RulesPath);
                }

                if (args.RuleContextFolder.Paths.FullPath !=
                    "/sitecore/client/Business Component Library/version 2/Layouts/Renderings/Resources/Rule/Rules" &&
                    args.RuleContextFolder.Paths.FullPath !=
                    "/sitecore/client/Business Component Library/version 1/Layouts/Renderings/Resources/Rule/Rules")
                {
                    if ((args.RuleContextFolder != null) &&
                        (args.RuleContextFolder.TemplateID != RuleIds.RulesContextFolderTemplateID))
                    {
                        args.RuleContextFolder = null;
                    }
                }
                if (((args.RuleContextFolder == null) && (item != null)) && item.Children.Any<Item>(delegate(Item c1)
                    {
                        if (!(c1.TemplateID == RuleIds.TagDefinitionsFolderTemplateID))
                        {
                            return false;
                        }
                        return c1.Children.Any<Item>(c2 => (c2.TemplateID == RuleIds.TagDefinitionTemplateID));
                    }))
                {
                    args.RuleContextFolder = item;
                }
                if (args.RuleContextFolder == null)
                {
                    args.AbortPipeline();
                }
            }
        }
    }
}
