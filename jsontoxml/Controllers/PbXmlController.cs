using Microsoft.AspNetCore.Mvc; // ApiController required
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;
using System;
using Newtonsoft.Json;

namespace jsontoxml.Controllers 
{
    // anteponer 'prefix/[controller]' para definir prefijo: ejemplo api/ClassDefinedName
    [Route("[controller]")]
    [ApiController]

    // classname will be endpoint: /PbXml
    public class PbXmlController : ControllerBase
    {
        // Definimos method GET y retornamos string
        [HttpGet]
        public string Get()
        {
            //https://localhost:7082/PbXml

            string ls_json;
            ls_json = "{\"SquidGame\":{\"Genre\":\"Thriller\",\"Rating\":{\"@Type\":\"Imdb\",\"#text\":\"8.1\"},\"Stars\":[\"Lee Jung-jae\"],\"Budget\":null}}";

            // COSAS A TENER EN CUENTA
            //XML must have a single root element – Which means we either need a top - level JSON node or ensure that the resulting XML has a dummy one.

            //Element name can’t have whitespace – If JSON has any such property name, the whitespaces will turn to their URL - encoded representation i.e. “_x0020_”.

            //Element name can’t start with a digit – Similar to whitespace it will turn to its URL - encoded value.

            XDeclaration defaultDeclaration = new("1.0", null, null);
            var doc = JsonConvert.DeserializeXNode(ls_json)!;
            var declaration = doc.Declaration ?? defaultDeclaration;

            return $"{declaration}{Environment.NewLine}{doc}";
            //return ls_json;
        }
        
    }

}