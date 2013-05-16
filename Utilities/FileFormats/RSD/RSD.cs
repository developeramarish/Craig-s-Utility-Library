﻿/*
Copyright (c) 2012 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

#region Usings
using System;
using System.IO;
using System.Text;
using System.Xml;
using Utilities.FileFormats.BaseClasses;
using Utilities.IO.ExtensionMethods;
#endregion

namespace Utilities.FileFormats.RSD
{
    /// <summary>
    /// Basic helper for RSD
    /// </summary>
    public class RSD : StringFormatBase<RSD>
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public RSD()
        {
            APIs = new APIs();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="FileContent">Content of the RSD file</param>
        public RSD(string FileContent)
        {
            if (string.IsNullOrEmpty(FileContent))
                throw new ArgumentNullException("FileContent");
            LoadContent(FileContent);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Engine name
        /// </summary>
        public virtual string EngineName { get; set; }

        /// <summary>
        /// Link to the engine
        /// </summary>
        public virtual string EngineLink { get; set; }

        /// <summary>
        /// Link to the home page
        /// </summary>
        public virtual string HomePageLink { get; set; }

        /// <summary>
        /// API definitions
        /// </summary>
        public virtual APIs APIs { get; set; }

        #endregion

        #region Public Overridden Functions

        /// <summary>
        /// Internal load function
        /// </summary>
        /// <param name="Location">Location of the file</param>
        /// <returns>This</returns>
        protected override RSD InternalLoad(string Location)
        {
            string Content = new FileInfo(Location).Read();
            LoadContent(Content);
            return this;
        }

        private void LoadContent(string Content)
        {
            XmlDocument Document = new XmlDocument();
            Document.LoadXml(Content);
            foreach (XmlNode Children in Document.ChildNodes)
            {
                if (Children.Name.Equals("RSD", StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (XmlNode Child in Children.ChildNodes)
                    {
                        if (Child.Name.Equals("service", StringComparison.CurrentCultureIgnoreCase))
                        {
                            foreach (XmlNode ServiceChild in Child.ChildNodes)
                            {
                                if (ServiceChild.Name.Equals("engineName", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    EngineName = ServiceChild.InnerText;
                                }
                                else if (ServiceChild.Name.Equals("engineLink", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    EngineLink = ServiceChild.InnerText;
                                }
                                else if (ServiceChild.Name.Equals("homePageLink", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    HomePageLink = ServiceChild.InnerText;
                                }
                                else if (ServiceChild.Name.Equals("apis", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    APIs = new APIs((XmlElement)ServiceChild);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Outputs the RSD file
        /// </summary>
        /// <returns>return the properly formatted RSD file</returns>
        public override string ToString()
        {
            StringBuilder Builder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?><rsd version=\"1.0\"><service><engineName>");
            Builder.Append(EngineName).Append("</engineName><engineLink>").Append(EngineLink).Append("</engineLink><homePageLink>")
                .Append(HomePageLink).Append("</homePageLink>");
            Builder.Append(APIs.ToString());
            Builder.Append("</service></rsd>");
            return Builder.ToString();
        }

        #endregion
    }
}