//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       timop
//
// Copyright 2004-2015 by OM International
//
// This file is part of OpenPetra.org.
//
// OpenPetra.org is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// OpenPetra.org is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with OpenPetra.org.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Ict.Common;

namespace Ict.Common.IO
{
    /// <summary>
    /// TYml2Xml is able to parse a YML file and store it in an XmlDocument
    ///
    /// Simple YAML to XML converter
    /// could have also used http://yaml-net-parser.sourceforge.net/default.html#intro
    /// But I prefer to write YAML files but to work with XML in the program.
    /// The yaml net parser would just be another interface to understand how to step through the document.
    ///
    /// See also the spec http://yaml.org/spec/1.2/
    ///
    /// We only support:
    ///   indentation: http://yaml.org/spec/1.2/#id2577368
    ///        only use spaces, no tabs for indentation
    ///        Each node must be indented further than its parent node.
    ///        All sibling nodes must use the exact same indentation level.
    ///
    ///        comment lines start with #, can be indented
    ///        separators are comma and colon, lists are defined by curly and square brackets {} and []
    ///        values can be quoted to escape the colon or comma
    ///
    ///        name and colon (name:) is converted into an XML element as a parent for the following indented lines
    ///          indention only happens after node and colon without content
    ///        scalar: name and colon and a literal is converted into an attribute of the current XML element
    ///        sequence: [element1, element2, element3] are translated to &lt;element name=&quot;element1&quot;/&gt;&lt;element name=&quot;element2&quot;/&gt; below the parent node
    ///        mapping: name and value assignments {size=10, help=Test with spaces} are converted into attributes of the parent node
    ///
    ///        not supported:
    ///           node names cannot be quoted to escape colons
    ///           {params: size=10, help=Test with spaces}: leave out the params
    ///           [=name, =id, address] sorting columns are not identified this way
    ///
    ///        This class supports the loading of several derived files,
    ///        and overwriting the data of the base files;
    ///        todo: make a tag so that everything is pushed into base, before the last file is loaded
    ///              change the code: move only attributes into base tag
    ///                               add an attribute to elements that have been in base, base=&quot;yes&quot;
    ///                               this way the order of elements can be maintained easily,
    ///                               but it is still known which elements and attributes have been added since the tag
    ///              reactivate the check again, in ProcessXAML.cs: if (TXMLParser.GetAttribute(rootNode, &quot;ClassType&quot;) != &quot;abstract&quot;)
    ///        todo: save the last file after modification have been done to the xml structure (eg. loaded from csharp file)
    /// </summary>
    public class TYml2Xml
    {
        /// <summary>
        /// contains the lines of the yml document
        /// </summary>
        protected string[] lines = null;
        /// <summary>
        /// the current line that we are parsing
        /// </summary>
        protected Int32 currentLine = -1;
        /// <summary>
        /// the filename of the file that we are parsing
        /// </summary>
        protected string filename = "";

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="AFilename"></param>
        public TYml2Xml(string AFilename)
        {
            filename = AFilename;

            // file should be in unicode encoding
            // StreamReader DetectEncodingFromByteOrderMarks does not work for ANSI?
            TextReader reader = new StreamReader(filename, TTextFile.GetFileEncoding(filename), false);
            lines = reader.ReadToEnd().Replace("\r", "").Split(new char[] { '\n' });
            reader.Close();
            currentLine = -1;
        }
    }
}
