using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.DbOperations
{
    public class ProductsDb
    {
        private static ProductsDb _instance;
        private ProductsDb()
        {

        }
        public static ProductsDb GetInstance()
        {
            return _instance ?? new ProductsDb();
        }

        public List<Products> GetProductsList()
        {
            try
            {
                using (var context = new DAL.Entities.Entities())
                {
                    var list = context.Products.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Products AddNewProduct(Products _products)
        {
            try
            {
                using (var context = new Entities.Entities())
                {
                    context.Products.Add(_products);
                    var numberOfAdded = context.SaveChanges();
                    return numberOfAdded > 0 ? _products : null;
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public bool UpdateProducts(Products _products)
        {
            try
            {
                using (var context = new Entities.Entities())
                {
                    context.Products.Update(_products);
                    var numberOfAdded = context.SaveChanges();
                    return numberOfAdded > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeleteProduct(int _productId)
        {
            try
            {
                using (var context = new Entities.Entities())
                {
                    var product = context.Products.FirstOrDefault(x => x.Id == _productId);
                    context.Products.Remove(product);
                    var numberOfAdded = context.SaveChanges();
                    return numberOfAdded > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public Products GetProductById(int _productId)
        {
            try
            {
                using (var context = new Entities.Entities())
                {
                    var product = context.Products.FirstOrDefault(x => x.Id == _productId);
                    return product;

                }
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }
    }
}
