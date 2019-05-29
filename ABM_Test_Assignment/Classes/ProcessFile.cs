using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

public class ProcessFile
    {
        public static int ProcessFileXml(XmlDocument xmlDocument)
        {
        bool DEF = xmlDocument.SelectSingleNode("/InputDocument/DeclarationList/Declaration[@Command='DEFAULT']").Attributes[0].Value == "DEFAULT";

        if (!DEF)
        {
            return -1;
        }

        bool DUB = xmlDocument.SelectSingleNode("/InputDocument/DeclarationList/Declaration/DeclarationHeader/SiteID").InnerText == "DUB";

        if (!DUB)
        {
            return -2;
        }

        return 0;

        }
}
