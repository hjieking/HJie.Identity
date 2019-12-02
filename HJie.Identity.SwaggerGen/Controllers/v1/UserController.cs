using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HJie.Identity.SwaggerGen.Controllers.v1
{
    /// <summary>
    /// gggggggggggggg
    /// </summary>
    [Route("api/v1/User/")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// gggggggggggg
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// lllllllllllllll
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}