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

namespace stocktake
{

    public partial class test : Form
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public WebReference.WebService ws = new global::stocktake.WebReference.WebService();
      
        public test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
         

        }
        private void test_Load(object sender, EventArgs e)
        {
            
    }
    }
}
   
    

