using System;
using System.Collections.Generic;
using System.Xml;
using ShopManager.Controller.CacheManager;
using ShopManager.Model.DataModels;

namespace ShopManager.Controller.XmlDataManager
{
    public static class ItemsXmlSerializer
    {
        public static XmlDocument SerializeSalesCache()
        {
            try
            {
                XmlDocument doc = CreateDocument("sales");

                List<Sale> sales = SalesCache.GetAllSales();
                foreach (Sale sale in sales)
                {
                    doc.DocumentElement.AppendChild(SerializeSale(sale, doc));
                }

                return doc;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return null;
            }
        }

        public static XmlDocument SerializeProductsCache()
        {
            try
            {
                XmlDocument doc = CreateDocument("products");

                List<Product> products = ProductCache.GetAllProducts();
                foreach (Product product in products)
                {
                    doc.DocumentElement.AppendChild(SerializeProduct(product, doc));
                }

                return doc;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return null;
            }
        }

        public static XmlDocument SerializeCategoriesCache()
        {
            try
            {
                XmlDocument doc = CreateDocument("categories");

                List<ProductCategory> categories = CategoriesCache.GetAllCategories();
                foreach (ProductCategory category in categories)
                {
                    doc.DocumentElement.AppendChild(SerializeCategory(category, doc));
                }

                return doc;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
                return null;
            }
        }

        private static XmlDocument CreateDocument(string rootName)
        {
            XmlDocument doc = new XmlDocument();

            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
            doc.AppendChild(xmlDeclaration);

            XmlElement rootElement = doc.CreateElement(rootName);
            doc.AppendChild(rootElement);

            return doc;
        }


        private static XmlElement SerializeSale(Sale sale, XmlDocument doc)
        {
            XmlElement element = doc.CreateElement("sale");

            XmlAttribute idAttr = doc.CreateAttribute("id");
            idAttr.Value = sale.ID.ToString();
            element.Attributes.Append(idAttr);

            XmlElement property = doc.CreateElement("product-id");
            property.InnerText = sale.ProductID.ToString();
            element.AppendChild(property);

            property = doc.CreateElement("quantity");
            property.InnerText = sale.Quantity.ToString();
            element.AppendChild(property);

            return element;
        }

        private static XmlElement SerializeProduct(Product product, XmlDocument doc)
        {
            XmlElement element = doc.CreateElement("product");

            XmlAttribute idAttr = doc.CreateAttribute("id");
            idAttr.Value = product.ID.ToString();
            element.Attributes.Append(idAttr);

            XmlElement property = doc.CreateElement("name");
            property.InnerText = product.Name.ToString();
            element.AppendChild(property);

            property = doc.CreateElement("description");
            property.InnerText = product.Description.ToString();
            element.AppendChild(property);

            property = doc.CreateElement("price");
            property.InnerText = product.Price.ToString();
            element.AppendChild(property);

            property = doc.CreateElement("price-per-kg");
            property.InnerText = product.PricePerKg.ToString();
            element.AppendChild(property);

            property = doc.CreateElement("purchase-date");
            property.InnerText = product.PurchaseDate.ToString("u");
            element.AppendChild(property);

            property = doc.CreateElement("expiry-date");
            property.InnerText = product.ExpiryDate.ToString("u");
            element.AppendChild(property);

            property = doc.CreateElement("quantity");
            property.InnerText = product.Quantity.ToString();
            element.AppendChild(property);

            property = doc.CreateElement("category-id");
            property.InnerText = product.CategoryID.ToString();
            element.AppendChild(property);

            return element;
        }

        private static XmlElement SerializeCategory(ProductCategory category, XmlDocument doc)
        {
            XmlElement element = doc.CreateElement("sale");

            XmlAttribute idAttr = doc.CreateAttribute("id");
            idAttr.Value = category.ID.ToString();
            element.Attributes.Append(idAttr);

            XmlElement property = doc.CreateElement("name");
            property.InnerText = category.Name.ToString();
            element.AppendChild(property);

            return element;
        }
    }
}
