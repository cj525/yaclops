﻿using System.Collections.Generic;
using System.Text;


namespace Yaclops.DocumentModel
{
    /// <summary>
    /// Simplistic implementation of a markdown-like language for text formatting
    /// </summary>
    internal class MarkLeft
    {

        public static IEnumerable<Paragraph> Parse(string content, ParagraphStyle style = null)
        {
            if (string.IsNullOrEmpty(content))
            {
                yield break;
            }

            if (style == null)
            {
                style = new ParagraphStyle();
            }

            // TODO - this is a HACK! Make this work right! (A blank line should start a new paragraph, for a start)

            StringBuilder builder = new StringBuilder();
            Paragraph para;
            int returnCount = 0;

            foreach (var c in content)
            {
                switch (c)
                {
                    case '\n':
                        break;

                    case '\r':
                        returnCount += 1;
                        if (returnCount > 1)
                        {
                            para = new Paragraph(style);
                            para.AddSpan(builder.ToString().Trim());
                            yield return para;
                            builder.Clear();
                        }
                        else
                        {
                            builder.Append(' ');
                        }
                        break;

                    default:
                        builder.Append(c);
                        returnCount = 0;
                        break;
                }
            }

            para = new Paragraph(style);
            para.AddSpan(builder.ToString().Trim());
            yield return para;
        }
    }
}
