using gNetComApi.Dto;
using gNetComApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gNetComApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly josericardo3_Context _db;
        DateTime localDate = DateTime.Now;

        public RoutesController(josericardo3_Context db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("listAllRoutes")]
        public async Task<IActionResult> GetUsuarios()
        {
            var lista = await _db.Routes.ToListAsync();

            if (lista.Count > 0)
            {
                var result = new List<RouteDTO>();
                foreach (var item in lista)
                {
                    var RouteDto = new RouteDTO();
                    RouteDto.RouteId = item.RouteId;
                    RouteDto.UserOwner = item.UserOwner;
                    RouteDto.Name = item.Name;
                    RouteDto.Description = item.Description;
                    RouteDto.DateCreated = item.DateCreated;
                    RouteDto.LatitudeInit = item.LatitudeInit;
                    RouteDto.LatitudeFin = item.LatitudeFin;
                    RouteDto.LongitudeInit = item.LongitudeInit;
                    RouteDto.LongitudeFin = item.LongitudeFin;
                    result.Add(RouteDto);
                }
                return Ok(result);
            }
            else
            {
                return Ok("No hay Rutas");
            }
        }

        [HttpPost("registrarRuta")]
        public async Task<ActionResult<Models.Route>> RegistrarRuta([FromBody] newRouteDto ruta)
        {
            if (ruta == null)
            {
                return BadRequest("ingrese datos correctos");
            }
            var nuevaRuta = new Models.Route();
            var validar = await _db.Routes.OrderByDescending(t => t.RouteId).FirstOrDefaultAsync();
            nuevaRuta.RouteId = (Convert.ToInt64(validar.RouteId) + 1).ToString();
            nuevaRuta.UserOwner = ruta.UserId;
            nuevaRuta.Name = ruta.Name;
            nuevaRuta.Description = ruta.Description;
            nuevaRuta.DateCreated = localDate.Date;
            await _db.Routes.AddAsync(nuevaRuta);
            await _db.SaveChangesAsync();
            var enlazar = new Models.UserRoute();
            var validar02 = await _db.UserRoutes.OrderByDescending(t => t.Id).FirstOrDefaultAsync();
            enlazar.Id = (Convert.ToInt64(validar02.Id) + 1).ToString();
            enlazar.UserId = ruta.UserId;
            enlazar.RouteId = nuevaRuta.RouteId;

            
            await _db.UserRoutes.AddAsync(enlazar);
            await _db.SaveChangesAsync();

            var newUser = await _db.Routes.OrderByDescending(t => t.RouteId).FirstOrDefaultAsync();



            return Ok(newUser);         
            
        }

        [HttpPost("buscarRoute")]
        public async Task<ActionResult<Models.Route>> BuscarRoute([FromBody] string ruta)
        {
            if (ruta == null)
            {
                return BadRequest("ingrese datos correctos");
            }
            var list = new List<Models.Route>();
            ruta = ruta.Trim().ToLower();
            var allRoutes = await _db.Routes.ToListAsync();
            foreach (var item in allRoutes)
            {
                var minusculas = item.Name.Trim().ToLower();
                if (minusculas.Contains(ruta))
                {
                    list.Add(item);
                }
            }
            return Ok(list);
            
        }

        [HttpPost("newPost")]
        public async Task<IActionResult> newPost([FromBody] newPostDto post)
        {
            if (post == null)
            {
                return BadRequest("ingrese datos correctos");
            }
            var nuevoPost = new Models.Post();
            var validar = await _db.Posts.OrderByDescending(t => t.PostId).FirstOrDefaultAsync();
            nuevoPost.PostId = (Convert.ToInt64(validar.PostId) + 1).ToString();
            nuevoPost.UserId = post.UserId;
            nuevoPost.RouteId = post.RouteId;
            nuevoPost.Title = post.Title;
            nuevoPost.Body = post.Body;
            nuevoPost.Image = post.Image;
            await _db.Posts.AddAsync(nuevoPost);
            await _db.SaveChangesAsync();
            
            var newPost = await _db.Posts.OrderByDescending(t => t.UserId == post.UserId).FirstOrDefaultAsync();



            return Ok(newPost);

        }

        [HttpPost("newCommet")]
        public async Task<IActionResult> newComment([FromBody] newCommentDto post)
        {
            if (post == null)
            {
                return BadRequest("ingrese datos correctos");
            }
            var nuevoPost = new Models.Commentary();
            var validar = await _db.Commentaries.OrderByDescending(t => t.CommentId).FirstOrDefaultAsync();
            nuevoPost.CommentId = (Convert.ToInt64(validar.CommentId) + 1).ToString();
            nuevoPost.UserId = post.UserId;
            nuevoPost.PostId = post.PostId;
            nuevoPost.Body = post.Body;
            await _db.Commentaries.AddAsync(nuevoPost);
            await _db.SaveChangesAsync();

            var newPost = await _db.Commentaries.OrderByDescending(t => t.UserId == post.UserId).FirstOrDefaultAsync();



            return Ok(newPost);

        }

        [HttpGet]
        [Route("listRoutesxUserCreated/{idUser}")]
        public async Task<IActionResult> GetRouterxUser(string idUser)
        {
            var lista = await _db.Routes.Where(x=>x.UserOwner == idUser).ToListAsync();

            if (lista.Count > 0)
            {
                var result = new List<RouteDTO>();
                foreach (var item in lista)
                {
                    var RouteDto = new RouteDTO();
                    RouteDto.RouteId = item.RouteId;
                    RouteDto.UserOwner = item.UserOwner;
                    RouteDto.Name = item.Name;
                    RouteDto.Description = item.Description;
                    RouteDto.DateCreated = item.DateCreated;
                    RouteDto.LatitudeInit = item.LatitudeInit;
                    RouteDto.LatitudeFin = item.LatitudeFin;
                    RouteDto.LongitudeInit = item.LongitudeInit;
                    RouteDto.LongitudeFin = item.LongitudeFin;
                    result.Add(RouteDto);
                }
                return Ok(result);
            }
            else
            {
                return Ok("No hay Rutas");
            }
        }

        [HttpGet]
        [Route("Route/{idRoute}")]
        public async Task<IActionResult> GetRouter(string idRoute)
        {
            var lista = await _db.Routes.Include(x=>x.Posts).Include(x => x.UserOwnerNavigation).Where(x => x.RouteId == idRoute).ToListAsync();

            if (lista.Count > 0)
            {
                var result = new List<RouteDTO>();
                foreach (var item in lista)
                {
                    var RouteDto = new RouteDTO();
                    RouteDto.RouteId = item.RouteId;
                    RouteDto.UserOwner = item.UserOwner;
                    RouteDto.Name = item.Name;
                    RouteDto.Description = item.Description;
                    RouteDto.DateCreated = item.DateCreated;
                    RouteDto.LatitudeInit = item.LatitudeInit;
                    RouteDto.LatitudeFin = item.LatitudeFin;
                    RouteDto.LongitudeInit = item.LongitudeInit;
                    RouteDto.LongitudeFin = item.LongitudeFin;
                    result.Add(RouteDto);
                }
                return Ok(lista[0]);
            }
            else
            {
                return Ok("No hay Rutas");
            }
        }

        [HttpGet]
        [Route("Post/{idPost}")]
        public async Task<IActionResult> GetPost(string idPost)
        {
            var lista = await _db.Posts.Include(x => x.Commentaries).Where(x => x.PostId == idPost).ToListAsync();

            if (lista.Count > 0)
            {
                return Ok(lista[0]);
            }
            else
            {
                return Ok("No hay Posts");
            }
        }

        [HttpGet]
        [Route("Comment/{idComment}")]
        public async Task<IActionResult> GetComment(string idComment)
        {
            var lista = await _db.Commentaries.Include(x => x.User).Where(x => x.CommentId == idComment).ToListAsync();

            if (lista.Count > 0)
            {
                return Ok(lista[0]);
            }
            else
            {
                return Ok("No hay Posts");
            }
        }

    }
}
