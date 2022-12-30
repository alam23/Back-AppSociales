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
            var validar = await _db.Routes.OrderByDescending(t => t.RouteId).ToListAsync();
            int comparador = 0;
            var validarNombre = "";
            foreach (var item in validar)
            {
                if (Convert.ToInt32(item.RouteId) > comparador)
                {
                    validarNombre = item.RouteId;
                    comparador = Convert.ToInt32(item.RouteId);
                }
            }
            nuevaRuta.RouteId = (comparador + 1).ToString();
            nuevaRuta.UserOwner = ruta.UserId;
            nuevaRuta.Name = ruta.Name;
            nuevaRuta.Description = ruta.Description;
            nuevaRuta.DateCreated = localDate.Date;
            await _db.Routes.AddAsync(nuevaRuta);
            await _db.SaveChangesAsync();
            var enlazar = new Models.UserRoute();
            var validar02 = await _db.UserRoutes.OrderByDescending(t => t.Id).ToListAsync();
            int comparador02 = 0;
            var validarNombre02 = "";
            foreach (var item in validar02)
            {
                if (Convert.ToInt32(item.Id) > comparador02)
                {
                    //validarNombre = item.UserName;
                    comparador02 = Convert.ToInt32(item.Id);
                }
            }
            enlazar.Id = (comparador02 + 1).ToString();
            enlazar.UserId = ruta.UserId;
            enlazar.RouteId = nuevaRuta.RouteId;

            
            await _db.UserRoutes.AddAsync(enlazar);
            await _db.SaveChangesAsync();

            var validar03 = await _db.Routes.OrderByDescending(t => t.RouteId).ToListAsync();
            int comparador03 = 0;
            //var validarNombre = "";
            foreach (var item in validar03)
            {
                if (Convert.ToInt32(item.RouteId) > comparador03)
                {
                    //validarNombre = item.UserName;
                    comparador03 = Convert.ToInt32(item.RouteId);
                }
            }
            var newUser = await _db.Routes.Where(x => x.RouteId == comparador03.ToString()).ToListAsync();



            return Ok(newUser[0]);         
            
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
            var validar = await _db.Posts.OrderByDescending(t => t.PostId).ToListAsync();
            int comparador = 0;
            var validarNombre = "";
            foreach (var item in validar)
            {
                if (Convert.ToInt32(item.PostId) > comparador)
                {
                    //validarNombre = item.UserName;
                    comparador = Convert.ToInt32(item.PostId);
                }
            }
            nuevoPost.PostId = (comparador + 1).ToString();
            nuevoPost.UserId = post.UserId;
            nuevoPost.RouteId = post.RouteId;
            nuevoPost.Title = post.Title;
            nuevoPost.Body = post.Body;
            nuevoPost.Image = post.Image;
            await _db.Posts.AddAsync(nuevoPost);
            await _db.SaveChangesAsync();
            
            var newPost = await _db.Posts.Select(x=>x.PostId == nuevoPost.PostId).ToListAsync();



            return Ok(newPost[0]);

        }

        [HttpPost("newCommet")]
        public async Task<IActionResult> newComment([FromBody] newCommentDto post)
        {
            if (post == null)
            {
                return BadRequest("ingrese datos correctos");
            }
            var nuevoPost = new Models.Commentary();
            var validar = await _db.Commentaries.OrderByDescending(t => t.CommentId).ToListAsync();
            int comparador = 0;
            //var validarNombre = "";
            foreach (var item in validar)
            {
                if (Convert.ToInt32(item.CommentId) > comparador)
                {
                    //validarNombre = item.UserName;
                    comparador = Convert.ToInt32(item.CommentId);
                }
            }
            nuevoPost.CommentId = (comparador + 1).ToString();
            nuevoPost.UserId = post.UserId;
            nuevoPost.PostId = post.PostId;
            nuevoPost.Body = post.Body;
            await _db.Commentaries.AddAsync(nuevoPost);
            await _db.SaveChangesAsync();

            var newPost = await _db.Commentaries.Where(t => t.CommentId == nuevoPost.CommentId).FirstOrDefaultAsync();

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
