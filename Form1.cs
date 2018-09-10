using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tree1
{
    public partial class Form1 : Form
    {
        MyTree myTree = new MyTree();
        MyTour myTour = new MyTour();
        bool drawing = false;
        bool flag = false;
        List<int> ar = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)//СОздать дерево
        {
            try
            {
                myTour = new MyTour();
                int L = 0;
                for (int i= 0; i < richTextBox1.Lines.Count() ; i++)
                if (richTextBox1.Lines[i] != "")
                    L++;
                myTour.a = new int[L];
                myTour.array = new Node[0];
                for (int i = 0; i < L; i++)
                    if (richTextBox1.Lines[i] != "")
                        myTour.a[i] = Convert.ToInt32(richTextBox1.Lines[i]);
                myTour.head = myTour.HeapCreate(ClientRectangle.Height);
                myTour.g = this.CreateGraphics();
                myTour.Draw();
                btnSort.Enabled = true;
                btnAutoSort.Enabled = true;
            }
            catch
            {

            }

            
        }

        void MyDraw()
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)//щелк
        {
            if (myTree.selectNode!=null)
            myTree.selectNode.visit = false;
            myTree.DeSelect(myTree.top);
            myTree.selectNode = myTree.FindNode(myTree.top, e.X, e.Y);
             drawing = (myTree.selectNode != null);
            if (drawing)
            {
                myTree.selectNode.visit = true;
                flag = true;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                if (drawing)
                    myTree.Delta(myTree.selectNode, myTree.selectNode.x - e.X, myTree.selectNode.y - e.Y);
                else
                {
                    myTree.DeSelect(myTree.top);
                    myTree.selectNode = myTree.FindNode(myTree.top, e.X, e.Y);
                    if (myTree.selectNode != null)
                        myTree.selectNode.visit = true;
                }
                myTree.g.Clear(Color.WhiteSmoke);
                myTree.g = this.CreateGraphics();
                myTree.DrawNode(myTree.top);


             //myTree.Draw(myTree.selectNode);
            }
        }//Двиг

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (flag)
            {
                drawing = false;
                flag = false;
            }
        }//Разщелк

        private void BtnSortAr_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            richTextBox1.Text = myTree.SetStrSort(myTree.top);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int data = Convert.ToInt32(tbDelete.Text);
                myTree.Delete(data, ref myTree.top);
                myTree.g.Clear(Color.WhiteSmoke);
                myTree.g = this.CreateGraphics();
                myTree.DrawNode(myTree.top);
            }
            catch
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int data = Convert.ToInt32(tbAdd.Text);
            myTree.Insert(ref myTree.top, data, myTree.top.x, myTree.top.y);
            myTree.g.Clear(Color.WhiteSmoke);
            myTree.g = this.CreateGraphics();
            myTree.DrawNode(myTree.top);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int data = Convert.ToInt32(tbFind.Text);
            if (myTree.selectNode != null)
                myTree.selectNode.visit = false;
            myTree.selectNode = myTree.FindNodeVal(data, myTree.top);
            if (myTree.selectNode != null)
                myTree.selectNode.visit = true;
            myTree.g.Clear(Color.WhiteSmoke);
            myTree.g = this.CreateGraphics();
            myTree.DrawNode(myTree.top);
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            try
            {
                //myTour.g.Clear(Color.WhiteSmoke);
                ar.Add(Convert.ToInt32(myTour.head.data));
               myTour.Competition(ref myTour.head);
                myTour.Draw();
            }
            catch
            {
                richTextBox1.Text = "";
                string s="";
                for(int i = 0; i<ar.Count; i++)
                {
                    s += ar[i] + "\r\n";
                }
                ar.Clear();
                richTextBox1.Text = s;
                btnSort.Enabled = false;
                btnAutoSort.Enabled = false;
            }
        }

        private void btnAutoSort_Click(object sender, EventArgs e)
        {
            btnAutoSort.Enabled = false;
            btnSort.Enabled = false;
            int L = 0;
            for (int i = 0; i < richTextBox1.Lines.Count(); i++)
                if (richTextBox1.Lines[i] != "")
                    L++;
            for (int i = 0; i < L; i++)
            {
                ar.Add(Convert.ToInt32(myTour.head.data));
                myTour.Competition(ref myTour.head);
            }
            richTextBox1.Text = "";
            string s = "";
            for (int i = 0; i < ar.Count; i++)
            {
                s += ar[i] + "\r\n";
            }
            ar.Clear();
            richTextBox1.Text = s;
            btnSort.Enabled = false;
            btnAutoSort.Enabled = false;

            myTour = new MyTour();
             L = 0;
            for (int i = 0; i < richTextBox1.Lines.Count(); i++)
                if (richTextBox1.Lines[i] != "")
                    L++;
            myTour.a = new int[L];
            myTour.array = new Node[0];
            for (int i = 0; i < L; i++)
                if (richTextBox1.Lines[i] != "")
                    myTour.a[i] = Convert.ToInt32(richTextBox1.Lines[i]);
            myTour.head = myTour.HeapCreate(ClientRectangle.Height);
            myTour.g = this.CreateGraphics();
            myTour.Draw();
            btnSort.Enabled = true;
            btnAutoSort.Enabled = true;
        }
    }
}
