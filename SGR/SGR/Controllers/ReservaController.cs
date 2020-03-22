﻿using System.Diagnostics;
using System.Threading.Tasks;
using SGR.Models;
using SGR.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace SGR.Controllers
{
    public class ReservaController : Controller
    {

        private SGRContext db;

        public ReservaController(SGRContext context)
        {
            db = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await db.Reserva.ToListAsync());
        }


        // GET: Reserva/Detalhes/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Reserva r = db.Reserva.Find(id);
            if (r == null)
            {
                return RedirectToAction("Index");
            }
            return View(r);
        }

        // GET: Reserva/Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: Reserva/Adicionar
        [HttpPost]
        public async Task<IActionResult> Adicionar(Reserva reseva)
        {
            if (!ModelState.IsValid)
                return View(reseva);

            db.Add(reseva);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Reserva/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Reserva f = db.Reserva.Find(id);
            if (f == null)
            {
                return RedirectToAction("Index");
            }
            return View(f);
        }

        // POST: Reserva/Editar/5
        [HttpPost, ActionName("Editar")]
        public async Task<IActionResult> EditarPost(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                db.Update(reserva);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(reserva);
        }

        // GET: Reserva/Eliminar/5
        public ActionResult Eliminar(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Eliminar falhou. Tente outra vez, e se o problema persistir contacte o administrador.";
            }
            Reserva f = db.Reserva.Find(id);
            if (f == null)
            {
                return NotFound();
            }
            return View(f);
        }

        // POST: Reserva/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Reserva f = db.Reserva.Find(id);
                db.Reserva.Remove(f);
                await db.SaveChangesAsync();
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Eliminar", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

