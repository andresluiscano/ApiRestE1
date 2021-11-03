using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace ApiRestE1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpPost("masMenos")]
        public IActionResult masMenos([FromBody] string data)
        {
            try
            {
                var json = JsonConvert.DeserializeObject<ArrRequestModel>(data);

                double cant = json.array.Count();
                double positive = json.array.Where(x => x > 0).Count();
                double negative = json.array.Where(x => x < 0).Count();
                double zero = json.array.Where(x => x == 0).Count();

                return Ok(new double[] { positive / cant, zero / cant, negative / cant });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }

        public class ArrRequestModel
        {
            public int[] array { get; set; }
        }

    }
}
