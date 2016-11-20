using RemotingConsumer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserMaintenance.Models;
using Utilities.Exceptions;

namespace UserMaintenance.Controllers
{
    public class UserController : ApiController
    {
        private UserLogic userLogic;
        private LogValidator logValidator;
        public UserController()
        {
            userLogic = new UserLogic();
            logValidator = new LogValidator();
        }
        [Route("api/Users/{name}/{password}")]
        [HttpGet]
        public IHttpActionResult AdminLogin(string name,string password)
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
        [Route("api/Users/{name}/{password}")]
        [HttpPost]
        public IHttpActionResult Add(string name, string password) {
            try
            {

                if (logValidator.AdminLogged(Request.Headers))
                {
                    userLogic.AddUser(name, password);
                    return Ok();
                }else
                {
                    return BadRequest("Usuario o contrasenia incorrectos");
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/Users/{name}")]
        [HttpPut]
        public IHttpActionResult Update(string name,User user)
        {
           try
            {
                if (logValidator.AdminLogged(Request.Headers))
                {
                    if (userLogic.UpdateUser(name, user.name, user.password))
                        return Ok();
                    else
                        return NotFound();
                }
                else {
                    return BadRequest("Usuario o contrasenia incorrectos");
                }
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
                if (logValidator.AdminLogged(Request.Headers))
                {
                    if (userLogic.DeleteUser(name))
                        return Ok();
                    else
                        return NotFound();
                }
                else {
                    return BadRequest("Usuario o contrasenia invalidos");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}