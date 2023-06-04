using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Side_Scrolling_Game_MOO_ICT
{
    public partial class FormLoading : Form
    {
        public FormLoading()
        {
            InitializeComponent();
        }
        private const int MaxValue = 100;
        private const int Delay = 50;

      
        private int currentValue;

        private void FormLoading_Load(object sender, EventArgs e)
        {
           
            currentValue = 0;
            progressBar1.Minimum = 0 ;
            progressBar1.Maximum = MaxValue;
            progressBar1.Value = 0;

            timer1.Interval = Delay;
            timer1.Tick += timer1_Tick;

            timer1.Start();
        }

        private bool formHomeShown = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (currentValue >= MaxValue)
            {
                timer1.Stop();
                this.Hide();
                if (!formHomeShown)
                {
                    formHomeShown = true;
                    FormHome formHome = new FormHome();
                    formHome.Show();
                }
            }
            else
            {
                currentValue++;
                progressBar1.Value = currentValue;


            }

        }
    }
}
