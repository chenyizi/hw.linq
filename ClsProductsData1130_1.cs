
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//命名空間
namespace MyDataContext
{

    public class DataContext
    {
        public static IEnumerable<Product> Products = new ProducList();
        public static IEnumerable<Category> Categories = new CategoryList();



    }
    public class ProducList : List<Product>
    {

        public ProducList()
        {

            Product[] products = { 
                new Product {  ProductID = 1, ProductName = "蘋果", UnitsInStock = 10, UnitPrice =10 , CategoryID=1}, 
                new Product {  ProductID = 2, ProductName = "香蕉", UnitsInStock = 10, UnitPrice =10 , CategoryID=1}, 
                new Product {  ProductID = 3, ProductName = "櫻桃", UnitsInStock = 20, UnitPrice =40 , CategoryID=1}, 
                new Product {  ProductID = 4, ProductName = "鳳梨", UnitsInStock = 10, UnitPrice =20 , CategoryID=1},

                new Product {  ProductID = 5, ProductName = "豬肉", UnitsInStock = 15, UnitPrice =20 , CategoryID=2}, 
                new Product {  ProductID = 6, ProductName = "牛肉", UnitsInStock = 10, UnitPrice =10 , CategoryID=2}, 
                new Product {  ProductID = 7, ProductName = "羊肉", UnitsInStock = 10, UnitPrice =40 , CategoryID=2}, 
                new Product {  ProductID = 8, ProductName = "雞肉", UnitsInStock = 26, UnitPrice =20 , CategoryID=2} , 

                new Product {  ProductID = 9, ProductName = "青椒", UnitsInStock = 10, UnitPrice =70 , CategoryID=3}  ,
                                     
                new Product {  ProductID = 10, ProductName = "魚", UnitsInStock = 10, UnitPrice =70, CategoryID=null }  

                                 };




            base.AddRange(products);
        }
    }


    public class CategoryList : List<Category>
    {
        public CategoryList()
        {
            Category[] categories;

            categories = new Category[] 
                   { 
                    new Category { CategoryID = 1, CategoryName = "水果類" }, 
                    new Category { CategoryID = 2, CategoryName = "肉類" }, 
                    new Category { CategoryID = 3, CategoryName= "青菜類" } ,
                    new Category { CategoryID = 4, CategoryName= "海鮮類" } 
                                   
                    };


            base.AddRange(categories);

        }
    }


    public class Product
    {

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }

        public int? CategoryID { get; set; }

        // 導覽屬性
        public Category Category
        {
            get
            {
                return this.GetParentRow(this.CategoryID);
            }
        }

        private Category GetParentRow(int? categoryID)
        {
             var q = DataContext.Categories.SingleOrDefault(c => c.CategoryID == categoryID);

            return q;

        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}", this.ProductID, this.ProductName, this.UnitPrice, this.UnitsInStock);
        }

    }


    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        // 導覽屬性
        public IEnumerable<Product> Products
        {
            get
            {
                return this.GetChildRows(this.CategoryID);
            }
        }

        private IEnumerable<Product> GetChildRows(int categoryID)
        {
            var q = from p in DataContext.Products
                    where p.CategoryID == categoryID
                    select p;
            return q;


        }

    }



}
