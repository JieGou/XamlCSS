﻿using System.Linq;
using XamlCSS.CssParsing;
using XamlCSS.Dom;

namespace XamlCSS
{
    public class FirstOfTypeMatcher : SelectorMatcher
    {
        public FirstOfTypeMatcher(CssNodeType type, string text) : base(type, text)
        {
        }

        public override MatchResult Match<TDependencyObject>(StyleSheet styleSheet, ref IDomElement<TDependencyObject> domElement, SelectorMatcher[] fragments, ref int currentIndex)
        {
            var tagname = domElement.TagName;
            var namespaceUri = domElement.NamespaceUri;

            var children = domElement.Parent?.ChildNodes
                .Where(x => x.TagName == tagname && x.NamespaceUri == namespaceUri)
                .ToList();

            if (children == null)
            {
                return MatchResult.ItemFailed;
            }
            return children.IndexOf(domElement) == 0 ? MatchResult.Success : MatchResult.ItemFailed;
        }
    }
}