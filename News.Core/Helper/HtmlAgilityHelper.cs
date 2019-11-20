using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace News.Core.Helper
{
    public static class HtmlAgilityHelper
    {
        public static void RemoveUnwantedHtmlTags(this HtmlNode html, List<string> unwantedTags)
        {
            var tryGetNodes = html?.SelectNodes("./*|./text()");

            if (tryGetNodes == null || !tryGetNodes.Any()) return;

            var nodes = new Queue<HtmlNode>(tryGetNodes);

            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();
                var parentNode = node.ParentNode;

                var childNodes = node.SelectNodes("./*|./text()");

                if (childNodes != null)
                {
                    foreach (var child in childNodes)
                    {
                        nodes.Enqueue(child);
                    }
                }

                if (unwantedTags.Any(tag => tag == node.Name))
                {
                    if (childNodes != null)
                    {
                        foreach (var child in childNodes)
                        {
                            parentNode.InsertBefore(child, node);
                        }
                    }

                    parentNode.RemoveChild(node);

                }
            }
        }

        public static void RemoveAttributes(this HtmlNode html, List<string> shouldRemoveList)
        {
            foreach (var childNode in html.ChildNodes)
            {
                foreach (var shouldRemove in shouldRemoveList)
                {
                    childNode.Attributes.Remove(shouldRemove);
                    RemoveAttributes(childNode, shouldRemoveList);
                }
            }
        }
    }
}
