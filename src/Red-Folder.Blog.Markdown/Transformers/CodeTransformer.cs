﻿using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace RedFolder.Blog.Markdown.Transformers
{
    public class CodeTransformer: BaseTransformer
    {
        public CodeTransformer() : base()
        {

        }

        public CodeTransformer(ITransformer innerTransformer) : base(innerTransformer)
        {

        }

        protected override string PreTransform(JObject meta, string markdown)
        {
            // Relacing Back Ticks (```) with <code> before it goes into Markdown avoids it seeing code as markdown
            return ReplaceCodeElement(ReplaceBackTicks(markdown));
        }

        private string ReplaceBackTicks(string markdown)
        {
            var ticksReplacer = new TicksReplacer();
            var ticksPattern = new Regex("```");
            return ticksPattern.Replace(markdown, new MatchEvaluator(ticksReplacer.Replace));
        }

        private string ReplaceCodeElement(string markdown)
        {
            return markdown.Replace("<code>", "<pre><code>").Replace("</code>", "</code></pre>").Replace("<code>\n", "<code>");
        }

        private class TicksReplacer
        {
            private bool _isOpen = false;

            public string Replace(Match m)
            {
                if (_isOpen)
                {
                    _isOpen = false;
                    return "</code>";
                }
                else
                {
                    _isOpen = true;
                    return "<code>";
                }
            }
        }
    }
}
