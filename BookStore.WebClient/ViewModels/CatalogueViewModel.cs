using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Services.Interfaces;
using BookStore.Services.MessageTypes;

namespace BookStore.WebClient.ViewModels
{
    public class CatalogueViewModel
    {

        private ICatalogueService CatalogueService
        {
            get
            {
                return  ServiceFactory.Instance.CatalogueService;
            }
        }

        public List<Book> Items
        {
            get
            {
                IList<Book> books = CatalogueService.GetBook(0, Int32.MaxValue);
                return books.ToList();
            }
        }
    }
}