using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreeStruc.DataAccess;
using TreeStruc.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TreeStruc.ViewModel
{
    [ViewComponent]
    public class TreeComponents : ViewComponent
    {
        private readonly Context context;

        public TreeComponents (Context context)
        {
            this.context = context;
        }
        public Task<IViewComponentResult> InvokeAsync(List<Node> nodes, bool IsFirstElement)
        {
            if (IsFirstElement)
            {
                // gets all nodes from the database
                nodes = context.Nodes.Include(c => c.Children).AsEnumerable().Where(x => x.Parent == null).ToList();
            }
            var viewModel = new TreeViewModel { IsFirstElement = IsFirstElement, Nodes = nodes };
            return Task.FromResult<IViewComponentResult>(View(viewModel));
        }

    }
}
