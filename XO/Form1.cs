using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XO {
    public partial class Form1 : Form {

        public Form1() {
            InitializeComponent();
            MinimumSize = new Size(300, 300);
            MaximumSize = new Size(1000, 1000);
        }

        private void button1_Click(object sender, EventArgs e) {
            Form2 form = new Form2();
            form.HeadForm = this;
            this.Hide();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e) {
            Close();
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            Size = new Size(Height, Height);
        }

        private void Form1_Resize(object sender, EventArgs e) {
            MaximumSize = new Size(Height, 600);
            MinimumSize = new Size(Height, 300);
        }
    }
}
