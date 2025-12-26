using LibreriaWeb.AppDbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using peliculas.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace peliculas.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly DataContext _context;

        public PeliculasController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: Peliculas
        public async Task<IActionResult> Index()
        {
           var result = from p in _context.Peliculas
                         join g in _context.Generos on p.GeneroId equals g.Id
                         join d in _context.Directores on p.DirectorId equals d.Id
                         select new Pelicula { Id= p.Id,Titulo=  p.Titulo, Sipnosis = p.Sipnosis,FechaEstreno = p.FechaEstreno,Duracion = p.Duracion, GeneroId = p.GeneroId,  Genero = g.Nombre, DirectorId = p.DirectorId, Director = d.Nombre, Imagen =p.Imagen };

            return View(await result.ToListAsync());
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public async Task<IActionResult> Create()
        {
            var generos = await _context.Generos.ToListAsync();
            ViewBag.listgeneros = new SelectList(generos.AsEnumerable(), "Id", "Nombre");
            var directores = await _context.Directores.ToListAsync();
            ViewBag.listdirectores = new SelectList(directores.AsEnumerable(), "Id", "Nombre");

            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Sipnosis,FechaEstreno,Duracion,GeneroId,DirectorId,Imagen,ImagenFile")] Pelicula pelicula)
        {
            var generos = await _context.Generos.ToListAsync();
            ViewBag.listgeneros = new SelectList(generos.AsEnumerable(), "Id", "Nombre");
            var directores = await _context.Directores.ToListAsync();
            ViewBag.listdirectores = new SelectList(directores.AsEnumerable(), "Id", "Nombre");

            using (var memoryStream = new MemoryStream())
            {
                await pelicula.ImagenFile.CopyToAsync(memoryStream);
                pelicula.Imagen = memoryStream.ToArray();
            }

            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            var generos = await _context.Generos.ToListAsync();
            ViewBag.listgeneros = new SelectList(generos.AsEnumerable(), "Id", "Nombre");
            var directores = await _context.Directores.ToListAsync();
            ViewBag.listdirectores = new SelectList(directores.AsEnumerable(), "Id", "Nombre");

           
            if (pelicula == null)
            {
                return NotFound();
            }
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Titulo,Sipnosis,FechaEstreno,Duracion,GeneroId,DirectorId,Imagen,ImagenFile")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (pelicula.ImagenFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await pelicula.ImagenFile.CopyToAsync(memoryStream);
                    pelicula.Imagen = memoryStream.ToArray();
                }
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(short id)
        {
            return _context.Peliculas.Any(e => e.Id == id);
        }
    }
}
