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

        [HttpGet("/level")]
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

            if (FoundNode is null)
            {
                return NotFound("Node with given id not found");
            }
            return Ok(FoundNode);
        }

        [HttpPost]
        public async Task<ActionResult<Node>> createNode(Node node)
        {
            _context.Nodes.Add(node);
            await _context.SaveChangesAsync();

            return Ok(node);
        }

        [HttpDelete( "{id}")]
        [ServiceFilter(typeof(Node_ValidateNodeIdAttribute))]

        public async Task<ActionResult<Node>> deleteNode(int id)
        {
            var FoundNode = await _context.Nodes.FindAsync(id);

            if (FoundNode is null)
            {
                return NotFound("Node not found");
            }

            _context.Nodes.Remove(FoundNode);
            await _context.SaveChangesAsync();
            return Ok(FoundNode);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(Node_ValidateNodeIdAttribute))]
        [Node_ValidateUpdateNodeFilter]
        [ServiceFilter(typeof(Node_HandleUpdateExceptionsFilterAttribute))]
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
