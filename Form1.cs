using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ProductDAL _productDAL = new ProductDAL();

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _productDAL.GetAll();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _productDAL.Add(new Product
                {
                    Name = addName.Text.ToString(),
                    UnitPrice = Convert.ToDecimal(addPrice.Text),
                    StockAmount = Convert.ToInt32(addPrice.Text)
                });
            }
            catch
            {
                MessageBox.Show("Hata Oluştu, Ürün Eklenemedi.");
            }
            finally
            {
                dataGridView1.DataSource = _productDAL.GetAll();
                MessageBox.Show("Ürün Eklendi!");
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            Product prd = new Product
            {
                Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value),
                Name = updateName.Text,
                UnitPrice = Convert.ToDecimal(updatePrice.Text),
                StockAmount = Convert.ToInt32(updateStock.Text)
            };
            _productDAL.Update(prd);
            dataGridView1.DataSource = _productDAL.GetAll();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            updateName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            updatePrice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            updateStock.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {

            _productDAL.Delete(new Product
            {
                Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value)
            });
            dataGridView1.DataSource = _productDAL.GetAll();
        }

        public void Search(string key) 
        {
            dataGridView1.DataSource = _productDAL.GetAll().Where(p => p.Name.ToLower().Contains(key.ToLower())).ToList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Search(textBox1.Text);
            dataGridView1.DataSource=_productDAL.GetSearch(textBox1.Text);

        }
    }
}
