﻿namespace NovaShop.Web.Controllers;

public class CatalogController : ApiBaseController
{
    #region constrcutor

    private readonly IRepository<CatalogItem> _catalogItemsRepository;
    private readonly IRepository<CatalogBrand> _catalogBrandRepository;
    private readonly IRepository<CatalogCategory> _catalogCategory;
    private readonly IMapper _mapper;
    private readonly CatalogSettings _catalogSettings;

    public CatalogController(IRepository<CatalogItem> catalogItemsRepository, IMapper mapper, IOptions<CatalogSettings> catalogSetting, IRepository<CatalogBrand> catalogBrandRepository, IRepository<CatalogCategory> catalogCategory)
    {
        _catalogItemsRepository = catalogItemsRepository;
        _mapper = mapper;
        _catalogBrandRepository = catalogBrandRepository;
        _catalogCategory = catalogCategory;
        _catalogSettings = catalogSetting.Value;
    }

    #endregion

    #region filter catalog

    [HttpGet]
    [ProducesResponseType(typeof(FilterProductDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Filter list of catalog items",
        Description = "Filter list of catalog items",
        OperationId = "Catalog.GetCatalogItems",
        Tags = new[] { "Catalog" })
    ]
    public async Task<ActionResult<FilterProductDTO>> GetCatalogItems(
        [FromQuery, SwaggerParameter("Current page number")] int pageId = 1,
        [FromQuery, SwaggerParameter("Search in catalog names")] string? search = null,
        [FromQuery, SwaggerParameter("Search in catalog brands")] string? brand = null, 
        [FromQuery, SwaggerParameter("Search in catalog category")] string? category = null,
        [FromQuery, SwaggerParameter("Take items in current page")] int take = 6,
        [FromQuery, SwaggerParameter(Description = "Sort list", Required = true)] FilterProductOrderBy orderBy = FilterProductOrderBy.CreateData_Des
        )
    {
        var filter = new FilterProductDTO
        {
            Search = search,
            Category = category,
            Brand = brand,
            OrderBy = orderBy,
            PageId = pageId,
            Take = take,
            HowManyShowPageAfterAndBefore = 3
        };

        var filterCatalogSpec =
            new FilterCatalogSpec(filter.Search, filter.Brand, filter.Category,
                (filter.PageId - 1) * filter.Take, filter.Take, (int)filter.OrderBy);

        var pager = Pager.Build(filter.PageId,
            await _catalogItemsRepository.CountAsync(filterCatalogSpec),
            filter.Take, filter.HowManyShowPageAfterAndBefore);

        var allEntities = await _catalogItemsRepository.ListAsync(filterCatalogSpec);
        FillProductPictureUris(allEntities);
        var allCatalogs = _mapper.Map<List<CatalogItemDTO>>(allEntities);

        return Ok(filter.SetProducts(allCatalogs).SetPaging(pager));
    }

    #endregion

    #region get catalog by id

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CatalogItemDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get a catalog item with all catalogGallery",
        Description = "Get a catalog item with all catalogGallery",
        OperationId = "Catalog.GetCatalogItemById",
        Tags = new[] { "Catalog" })
    ]
    public async Task<ActionResult<CatalogItemDTO>> GetCatalogItemById(int id)
    {
        var getCatalogItemSpec = new GetCatalogItemSpec(id);
        var catalogItem = await _catalogItemsRepository.SingleOrDefaultAsync(getCatalogItemSpec);

        if (catalogItem == null) return NotFound();
        catalogItem.UpdatePictureUri(_catalogSettings.CatalogPictureBaseUri);
        FillProductGalleryPictureUris(catalogItem.Galleries);

        var catalogItemDto = _mapper.Map<CatalogItemDTO>(catalogItem);

        return Ok(catalogItemDto);
    }

    #endregion

    #region get brands

    [HttpGet("brands")]
    [ProducesResponseType(typeof(List<CatalogBrandDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Get all catalog brands",
        Description = "Get all catalog brands",
        OperationId = "Catalog.GetAllBrands",
        Tags = new[] { "Catalog" })]
    public async Task<ActionResult<List<CatalogBrandDTO>>> GetAllBrands()
    {
        var brands = await _catalogBrandRepository.ListAsync();
        return Ok(_mapper.Map<List<CatalogBrandDTO>>(brands));
    }

    #endregion

    #region get categories

    [HttpGet("categories")]
    [ProducesResponseType(typeof(List<CatalogCategoryDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Get all catalog category",
        Description = "Get all catalog category",
        OperationId = "Catalog.GetAllCategory",
        Tags = new[] { "Catalog" })]
    public async Task<ActionResult<List<CatalogCategoryDTO>>> GetAllCategory()
    {
        var categories = await _catalogCategory.ListAsync();
        return Ok(_mapper.Map<List<CatalogCategoryDTO>>(categories));
    }

    #endregion

    #region utilities

    private void FillProductPictureUris(List<CatalogItem> items)
    {
        var baseUri = _catalogSettings.CatalogPictureBaseUri;
        foreach (var item in items)
        {
            item.UpdatePictureUri(baseUri);
        }
    }

    private void FillProductGalleryPictureUris(IReadOnlyCollection<CatalogGallery>? items)
    {
        if (items == null || !items.Any()) return;

        var baseUri = _catalogSettings.CatalogGalleryPictureBaseUri;
        foreach (var item in items)
        {
            item.UpdatePictureUri(baseUri);
        }
    }

    #endregion
}