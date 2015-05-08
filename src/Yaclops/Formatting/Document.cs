﻿
namespace Yaclops.Formatting
{
    internal class Document : AbstractDocumentItem
    {
        public override string Tag
        {
            get { return "body"; }
        }

        // Convenience methods
        public Paragraph AddParagraph()
        {
            Paragraph para = new Paragraph();
            Add(para);
            return para;
        }
    }
}
