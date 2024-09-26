using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkDbFirstProject
{
    public partial class FormCategory : Form
    {
        public FormCategory()
        {
            InitializeComponent();
        }

        DbFirstDbEntities _dbContext = new DbFirstDbEntities();

        public void CategoryList()
        {
            var categories = _dbContext.Category.ToList();
            dataGridView1.DataSource = categories;
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            CategoryList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Category category = new Category();

            category.Name = textBoxCategoryName.Text;
            _dbContext.Category.Add(category);
            _dbContext.SaveChanges();

            CategoryList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBoxCategoryId.Text);
            var category = _dbContext.Category.Find(id);
            
            _dbContext.Category.Remove(category);
            _dbContext.SaveChanges();

            CategoryList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBoxCategoryId.Text);
            var category = _dbContext.Category.Find(id);

            category.Name = textBoxCategoryName.Text;
            _dbContext.SaveChanges();

            CategoryList();
        }
    }
}
