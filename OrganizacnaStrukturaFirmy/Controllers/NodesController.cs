using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizacnaStrukturaFirmy.Controllers.Filters;
using OrganizacnaStrukturaFirmy.Controllers.Filters.ActionFilters;
using OrganizacnaStrukturaFirmy.Controllers.Filters.ExceptionFilters;
using OrganizacnaStrukturaFirmy.Data;
using OrganizacnaStrukturaFirmy.Models;


namespace OrganizacnaStrukturaFirmy.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class NodesController : ControllerBase
    {

        private readonly DataContext _context;
        public NodesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Node>>> getAllNodes()
        {
            var nodes = await _context.Nodes.ToListAsync();
            return Ok(nodes);
        }

        [HttpGet("level")]
        public async Task<ActionResult<List<Node>>> getAllNodesWithLevel(int level)
        {
            var nodex = await _context.Nodes.Where(node => node.Level == level).ToListAsync();
            return Ok(nodex);
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(Node_ValidateNodeIdAttribute))]
        public async Task<ActionResult<Node>> getNodeById(int id)
        {
            var FoundNode = await _context.Nodes.FindAsync(id);

            return Ok(FoundNode);
        }

        [HttpPost]
        [ServiceFilter(typeof(Node_ValidateHeadEmployeeExistanceFilterAttribute))]
        [ServiceFilter(typeof(Node_ValidateParentNodeExistanceFilterAttribute))]
        public async Task<ActionResult<Node>> createNode(Node node)
        {
            _context.Nodes.Add(node);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(getNodeById), new {id = node.Id}, node);
        }

        [HttpDelete( "{id}")]
        [ServiceFilter(typeof(Node_ValidateNodeIdAttribute))]
        [ServiceFilter(typeof(Node_ValidateDeleteFilterAttribute))]
        public async Task<ActionResult<Node>> deleteNode(int id)
        {
            var FoundNode = await _context.Nodes.FindAsync(id);
            _context.Nodes.Remove(FoundNode);
            await _context.SaveChangesAsync();
            return Ok(FoundNode);
        }


        //todo forceDelete
        //vymaze vsetkych potomkov, setne employees workid na null
        [HttpDelete("force/{id}")]
        [ServiceFilter(typeof(Node_ValidateNodeIdAttribute))]
        public async Task<ActionResult<List<Node>>> forceDeleteNode(int id)
        {
            List<Node> nodes = new List<Node>();
            var FoundNode = await _context.Nodes.FindAsync(id);
            await RecursiveDeleteAsync(FoundNode, nodes);
            return Ok(nodes);
        }

        private async Task RecursiveDeleteAsync(Node node, List<Node> list)
        {
            var employees = await _context.Employees.Where(e => e.Id_workplace==node.Id).ToListAsync();//daj pracujucich v danom oddeleni
            foreach (var employee in employees)
            {
                employee.Id_workplace = null;
                //mozno treba dat sem save changes
            }
            await _context.SaveChangesAsync();
            var hasChild = await _context.Nodes.AnyAsync(n => n.Id_parentNode == node.Id);
            if (hasChild)
            {
                var child = await _context.Nodes.FirstAsync(n => n.Id_parentNode == node.Id);
                await RecursiveDeleteAsync(child, list);
            }

            await deleteNode(node.Id);
            list.Add(node);
            await _context.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(Node_ValidateNodeIdAttribute))]
        [Node_ValidateUpdateNodeFilter]
        [ServiceFilter(typeof(Node_HandleUpdateExceptionsFilterAttribute))]
        [ServiceFilter(typeof(Node_ValidateHeadEmployeeExistanceFilterAttribute))]
        //[ServiceFilter(typeof(Node_ValidateParentNodeExistanceFilterAttribute))] kontrolovane v levelAttribute 
        public async Task<ActionResult<Node>> editNode(int id, Node node)
        {
            var FoundNode = await _context.Nodes.FindAsync(node.Id);
            if (FoundNode is null)
            {
                return NotFound("Node not found");
            }
            FoundNode.NodeName = node.NodeName;
            FoundNode.NodeCode = node.NodeCode;
            FoundNode.Id_parentNode = node.Id_parentNode;
            FoundNode.Id_headEmployee = node.Id_headEmployee;
            FoundNode.Level = node.Level;
            await _context.SaveChangesAsync();
            return  NoContent();
        }
    }
}
