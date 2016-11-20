using RemotingConsumer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Utilities.Exceptions;

namespace UserMaintenance.Controllers
{
    public class UserController : ApiController
    {
        private UserLogic userLogic;
        public UserController()
        {
            userLogic = new UserLogic();
        }
        [Route("api/Users/{name}/{password}")]
        [HttpGet]
        public IHttpActionResult Login(string name,string password)
        {
            try
            {
                if (userLogic.LoginUser(name, password))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Nombre de usuario o contrasenia incorrectos");
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/Users")]
        [HttpPost]
        public IHttpActionResult Add(string name, string password) {
            try
            {
                userLogic.AddUser(name, password);
                return Ok();
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/Users/{name}")]
        [HttpPut]
        public IHttpActionResult Update(string name,string newName,string password)
        {
           try
            {
                if (userLogic.UpdateUser(name, newName, password))
                    return Ok();
                else
                    return NotFound();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/Users/{name}")]
        [HttpDelete]
        public IHttpActionResult Delete(string name)
        {
            try
            {
                if (userLogic.DeleteUser(name))
                    return Ok();
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}