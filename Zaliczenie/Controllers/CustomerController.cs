using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp;
using WebApp.Models;
using Zaliczenie.Models;
using Zaliczenie.Models.Gravity;

namespace Zaliczenie.Controllers;

public class CustomerController(GravityContext context) : Controller
{
    // GET
    public IActionResult Index(int page = 1, int size = 10)
    {
        return View( PagingListAsync<CustomerViewModel>.Create(
            (p, s) => 
                context.Customers
                    .Include(c => c.CustOrders)
                    .ThenInclude(co => co.DestAddress)
                    .ThenInclude(a => a!.Country)
                    .Select(c => new CustomerViewModel 
                    {
                        Id = c.CustomerId, 
                        FirstName = c.FirstName, 
                        LastName = c.LastName, 
                        Email = c.Email, 
                        OrderCount = c.CustOrders.Count,
                        CountryName = c.CustomerAddresses.First().Address.Country!.CountryName
                    })
                    .OrderBy(c => c.Id)
                    .Skip((p - 1) * s)
                    .Take(s)
                    .AsAsyncEnumerable(), 
            context.Customers.Count(),
            page,
            size));
    }

    public IActionResult CustomerOrders(int? id, int page = 1, int size = 10)
    {
        if (id == null) return NotFound();

        var customer = context.Customers.Find(id);
        
        if (customer == null) return NotFound();

        var orderQuery = context.CustOrders.Where(co => co.CustomerId == id);

        var paging = PagingListAsync<CustomerOrderItemViewModel>.Create(
            (p, s) =>
                orderQuery.Select(co => new CustomerOrderItemViewModel
                    {
                        OrderId = co.OrderId,
                        OrderDate = co.OrderDate
                    })
                    .OrderBy(c => c.OrderId)
                    .Skip((p - 1) * s)
                    .Take(s)
                    .AsAsyncEnumerable(),
            orderQuery.Count(),
            page,
            size);
        
        var model = new CustomerOrdersViewModel()
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            OrderCount = orderQuery.Count(),
            Data = paging
        };

        return View(model);
    }
}