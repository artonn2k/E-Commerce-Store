using API.RequestHelpers;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {   
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery]ProductParams productParams)
        {
            var query = _context.Products
                .Sort(productParams.OrderBy)    // .Sort -> the methods that comes from ProductExtensions class
                .Search(productParams.SearchTerm)  // .Search -> the methods that comes from ProductExtensions class
                .Filter(productParams.Brands, productParams.Types)  // .Filter -> the methods that comes from ProductExtensions class
                .AsQueryable(); 

            var products = await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

            Response.AddPaginationHeader(products.MetaData);

            return products;

            //filter for ordering -> price low to high , high to low, alphabetical order, searching product
        }
       
        //this one is going to get from the root ex. api/produktet/3 - 3shi means ID produktit
        [HttpGet("{id}")]   
        public  async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if(product == null) return NotFound();

            return product;
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var brands = await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
            var types = await _context.Products.Select(p => p.Type).Distinct().ToListAsync();

            return Ok(new{brands, types});
        }
    }
}