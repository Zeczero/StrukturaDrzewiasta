using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TreeStruc.DataAccess;
using TreeStruc.Models;

namespace TreeStruc.Controllers
{
    public class TreeController : Controller
    {
        // GET: TreeController

        private readonly Context _ctx;

        public TreeController(Context context)
        {
            _ctx = context;
        }

        public ActionResult Index()
        {
            try
            {
                // get all nodes from the database and list them.
                var tree = _ctx.Nodes.Include(c => c.Children)
                .AsEnumerable()
                .Where(x => x.Parent == null)
                .ToList();

                return View(tree);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        // POST: TreeController/Create
        [HttpPost]
        public ActionResult Create([FromBody] Node node)
        {
            if (!ModelState.IsValid)  // If the model provided is incorrect, push the user back to the website.
            {
                return View(node);
            }
            try
            {
                _ctx.Nodes.Add(node);
                _ctx.SaveChanges();
                return new JsonResult(node);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        // If user wants to edit his node, this is the method that does it.
        public ActionResult EditPart(int id)
        {
            var categories = _ctx.Nodes.ToList(); // get all nodes to list
            var category = _ctx.Nodes.Where(c => c.Id == id).FirstOrDefault(); // filters the nodes based on the Id.
            ViewBag.catId = id;
            ViewBag.catList = new SelectList(categories, "Id", "Name");

            return PartialView("EditModal");
        }
        public ActionResult Edit(Node node)
        {
            try
            {
                _ctx.Entry(_ctx.Nodes.FirstOrDefault(n => n.Id == node.Id)).CurrentValues.SetValues(node);
                _ctx.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }

        public void DeleteRecursively(Node node)
        {
            var nodeC = _ctx.Nodes.Where(c => c.ParentID == node.Id).ToList();
            node.Children = nodeC;

            if (node.Children.Count == 0) // if there are no children of the given node, just remove it
            {
                _ctx.Nodes.Remove(node);
            }
            else // otherwise remove it all the children in a given node recursively
            {
                _ctx.Nodes.Remove(node);
                foreach (var c in node.Children)
                {
                    DeleteRecursively(c);
                }
            }
            _ctx.SaveChanges();
        }
          
        // HTTP request to the controller in order to remove the node.
        public ActionResult Delete(int id)
        {
            var n = _ctx.Nodes.Where(c => c.Id == id).FirstOrDefault();
            DeleteRecursively(n);
            _ctx.SaveChanges();
            return new JsonResult(n);
        }

        // POST: TreeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
