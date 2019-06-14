﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Services.Interfaces;
using BookStore.Business.Components.Interfaces;
using Microsoft.Practices.ServiceLocation;
using BookStore.Services.MessageTypes;

namespace BookStore.Services
{
    public class CatalogueService : ICatalogueService
    {

        private ICatalogueProvider CatalogueProvider
        {
            get
            {
                return ServiceFactory.GetService<ICatalogueProvider>();
            }
        }

        public List<Book> GetBook(int pOffset, int pCount)
        {
            var internalResult = CatalogueProvider.GetBook(pOffset, pCount);
            CatalogueProvider.UpdateStock(internalResult);
            var externalResult = MessageTypeConverter.Instance.Convert<
                List<BookStore.Business.Entities.Book>,
                List<BookStore.Services.MessageTypes.Book>>(internalResult);


            return externalResult;
        }


        public Book GetBookById(int pId)
        {
            var external = MessageTypeConverter.Instance.Convert<
                BookStore.Business.Entities.Book,
                BookStore.Services.MessageTypes.Book>(
                CatalogueProvider.GetBookById(pId));
            return external;
        }


    }
}
