using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedFolder.Blog.Markdown.Transformers
{
    public class BaseTransformer : ITransformer
    {
        ITransformer _innerTransformer;

        public BaseTransformer()
        {
        }
        public BaseTransformer(ITransformer innerTransformer)
        {
            _innerTransformer = innerTransformer;
        }

        public string TransformMarkdown(JObject meta, string markdown)
        {
            var partialTransform = PreTransform(meta, markdown);

            if (_innerTransformer != null)
            {
                partialTransform = _innerTransformer.TransformMarkdown(meta, partialTransform);
            }

            partialTransform = PostTransform(meta, partialTransform);

            return partialTransform;
        }

        protected virtual string PreTransform(JObject meta, string markdown)
        {
            return markdown;
        }

        protected virtual string PostTransform(JObject meta, string markdown)
        {
            return markdown;
        }
    }
}
