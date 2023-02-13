using Microsoft.AspNetCore.Mvc; // ApiController required
//using System.Reflection.Emit; // Not used at moment
//using System.Text; // Not used at moment
using System.Xml.Linq;
//using System; // Not used at moment
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using System.Text;

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
            // Todo nombre de parametro json debe ir en comillas dobles, y el valor si puede ser con comillas dobles o sin comillas. ej: "activo": true
            ls_json = "{\"SquidGame\":{\"Genre\":\"Thriller\",\"Rating\":{\"@Type\":\"Imdb\",\"#text\":\"8.1\"},\"Stars\":[\"Lee Jung-jae\"],\"Budget\":null}}";

            // COSAS A TENER EN CUENTA
            // - XML must have a single root element – Which means we either need a top - level JSON node or ensure that the resulting XML has a dummy one.

            // - Element name can’t have whitespace – If JSON has any such property name, the whitespaces will turn to their URL - encoded representation i.e. “_x0020_”.

            // - Element name can’t start with a digit – Similar to whitespace it will turn to its URL - encoded value.

            XDeclaration defaultDeclaration = new("1.0", null, null);


            var doc = JsonConvert.DeserializeXNode(ls_json)!; // opcional pasamos segundo parametro para crear una etiqueta root que hace wrap de todo el contenido, ejemplo: JsonConvert.DeserializeXNode(ls_json, "movie")

            // definimos el declaration header ej: <?xml version="1.0"?>
            var declaration = doc.Declaration ?? defaultDeclaration;

            string ls_xml = $"{declaration}{Environment.NewLine}{doc}";
            return ls_xml;
            //return ls_json;


            //Example returned code
            //<? xml version = "1.0" ?>
            //< SquidGame >
            //  < Genre > Thriller </ Genre >
            //  < Rating Type = "Imdb" > 8.1 </ Rating >
            //  < Stars > Lee Jung - jae </ Stars >
            //  < Budget />
            //</ SquidGame >
        }

        public class PersonaJson
        {
            public string nombre { get; set; }
            public string edad { get; set; }
            public string correo { get; set; }
        }

        // Creamos el dataResponse de toda la vida xD
        public class ResponseJson
        {
            public int status { get; set; }
            public string message { get; set; }
            public List<PersonaJson> data { get; set; }
        }

        // Definimos method POST y retornamos string
        [HttpPost]
        public dynamic Post([FromBody] PersonaJson lo_json)
        {
            

            List<PersonaJson> la_data = new(){
                new PersonaJson { nombre = "sandra", edad = "20", correo = "sandra@gmail.com" },
                new PersonaJson { nombre = "manuel", edad = "27", correo = "manuel@gmail.com" }
            };

            ResponseJson stdClass = new() {
                status=1, 
                message="Proceso finalizado con exito", 
                data= la_data
            };

            return JsonConvert.SerializeObject(stdClass);
        }

    }

}