using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friends.Domain.CalculoHistorico.Models;
using Friends.Domain.Friend.Models;
using Friends.Infrastructure.Sql.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Friends.Controllers
{
    [Route("api/friend")]
    [EnableCors("AllowSpecificOrigin")]
    public class FriendController : Controller
    {

        [EnableCors("AllowSpecificOrigin")]
        [Authorize("Bearer")]
        [HttpPost]
        public ActionResult Create([FromBody] FriendModel friend, [FromServices]FriendDAO friendDAO)
        {
            bool hasCreated = friendDAO.Create(friend);
            if (hasCreated)
                return StatusCode(201);
            return BadRequest();
        }

        [EnableCors("AllowSpecificOrigin")]
        [Authorize("Bearer")]
        [HttpGet]
        public ActionResult Get( [FromServices]FriendDAO friendDAO)
        {
            var list = friendDAO.Get();
            
            return Ok(list);
        }

        [EnableCors("AllowSpecificOrigin")]
        [Authorize("Bearer")]
        [HttpGet("check/{latitude}/{longitude}")]
        public ActionResult Get(int latitude, int longitude, [FromServices]FriendDAO friendDAO, [FromServices]CalculoHistoricoDAO calculoDAO)
        {
            var list = friendDAO.Get();
            var listFull = new List<FriendFullModel>();

            foreach (var item in list)
            {
                var latitudeItem = item.Latitude;
                var longitudeItem = item.Longitude;

                int resultLatitude = latitudeItem - latitude;
                int resultLongitude = longitudeItem - longitude;
                
                int resultDistancia = (resultLatitude < 0 ? resultLatitude * -1 : resultLatitude) + (resultLongitude < 0 ? resultLongitude * -1 : resultLongitude);

                listFull.Add(new FriendFullModel()
                {
                    Id = item.Id,
                    ResultDistancia = resultDistancia,
                    Name = item.Name,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude
                });

                var calculo = new CalculoHistoricoModel() {
                    ResultadoDistancia = resultDistancia,
                    DiferencaLatitude = resultLatitude,
                    DiferencaLongitude = resultLongitude,
                    DataCalculo = DateTime.Now,
                    FriendId = item.Id
                };

                calculoDAO.Create(calculo);
            }

            listFull = listFull.OrderBy(o => o.ResultDistancia).Take(3).ToList();

            return Ok(listFull);
        }
    }
}