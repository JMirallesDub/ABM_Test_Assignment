using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.AspNetCore.Mvc;

namespace ABM_Test_Assignment.Controlers
{
    public enum ReturnStatusCodeWithMessage
    {
        [Description("The document was structured correctly")]
        OK = 0,
        [Description("Invalid command specified")]
        InvalidCommandSpecified = -1,
        [Description("Invalid Site specified")]
        InvalidSiteSpecified = -2,
    }
    [Route("api/[controller]")]
    [ApiController]
    public class ParserController : ControllerBase
    {
        public async Task<JsonResult> Index(string stringToProcess)
        {
            //stringToProcess = "Hello";
            string HardCodeString = @"<InputDocument>
			<DeclarationList>
			  <Declaration Command='DEFAULT' Version='5.13'>
				<DeclarationHeader>
				  <Jurisdiction>IE</Jurisdiction>
				  <CWProcedure>IMPORT</CWProcedure>
				  <DeclarationDestination>CUSTOMSWAREIE</DeclarationDestination>
				  <DocumentRef>71Q0019681</DocumentRef>
				  <SiteID>DUB</SiteID>
				  <AccountCode>G0779837</AccountCode>
				  </DeclarationHeader>
				</Declaration>
			</DeclarationList>
		  </InputDocument>
		";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(HardCodeString);

            try
            {
                doc.LoadXml(string.IsNullOrEmpty(stringToProcess) ? HardCodeString : stringToProcess);


                var result = ProcessFile.ProcessFileXml(doc);
                string message = string.Empty;
                switch (result)
                {
                    case 0:
                        message = "The document was structured correctly";
                            break;
                    case -1:
                        message = "Invalid command specified";
                        break;
                    case -2:
                        message = "Invalid Site specified";
                        break;
                    default:
                        break;
                }

                return new JsonResult(new { StatusCode = result, Message = message });

            }
            catch (XmlException ex)
            {
                return new JsonResult(ex.ToString());
            }
        }

    }
}