using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkDbFirstProject
{
    public partial class FormProduct : Form
    {
        DbFirstDbEntities _dbContext = new DbFirstDbEntities();


        public FormProduct()
        {
            InitializeComponent();
        }


        public void ProductList()
        {
            var products = _dbContext.Product.ToList();
            dataGridView1.DataSource = products;
        }


        private void FormProduct_Load(object sender, EventArgs e)
        {
            var categories = _dbContext.Category.ToList();

            comboBoxProductCategory.DisplayMember = "Name";
            comboBoxProductCategory.ValueMember = "Id";

            comboBoxProductCategory.DataSource = categories;
        }


        private void btnList_Click(object sender, EventArgs e)
        {
            ProductList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Product product = new Product();

            product.Name = textBoxProductName.Text;
            product.Price = textBoxProductPrice.Text;
            product.Stock = Int32.Parse(textBoxProductStock.Text);
            product.CategoryId = (int)comboBoxProductCategory.SelectedValue;

            _dbContext.Product.Add(product);
            _dbContext.SaveChanges();

            ProductList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(textBoxProductId.Text);
            var product = _dbContext.Product.Find(id);

            _dbContext.Product.Remove(product);
            _dbContext.SaveChanges();

            ProductList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBoxProductId.Text);
            var product = _dbContext.Product.Find(id);

            product.Name = textBoxProductName.Text;
            product.Price = textBoxProductPrice.Text;
            product.Stock = Int32.Parse(textBoxProductStock.Text);
            product.CategoryId = (int)comboBoxProductCategory.SelectedValue;

            _dbContext.SaveChanges();

            ProductList();
        }

        private void btnProductListWithCategory_Click(object sender, EventArgs e)
        {
            var values = _dbContext.Product.Join(_dbContext.Category, product => product.CategoryId, category => category.Id, (product, category) => new
            {
                ProductName = product.Name,
                ProductId = product.Id,
                ProductPrice = product.Price,
                ProductStock = product.Stock,
                CategoryId = category.Id,
                CategoryName = category.Name,

            }).ToList();

            dataGridView1.DataSource = values;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var searchValues = _dbContext.Product.Where(x => x.Name == textBoxProductName.Text).ToList();
            dataGridView1.DataSource = searchValues;
        }
    }
}
