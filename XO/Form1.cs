﻿using System;
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
    }
}