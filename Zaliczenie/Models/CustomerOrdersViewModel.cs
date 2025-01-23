using WebApp;

namespace Zaliczenie.Models;

public class CustomerOrdersViewModel
{
    public int CustomerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    
    public int OrderCount { get; set; }
    
    public PagingListAsync<CustomerOrderItemViewModel> Data { get; set; }
}