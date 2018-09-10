using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tree1
{
    class MyTour
    {
        public Node[] array;
        public int[] a;
        public Node head = null;
        
        int deltaX = 30;
        int deltaY = 30;
        int diametr = 24;
        int R = 12;
        public Graphics g;
        Pen MyPen = new Pen(Color.Black);
        SolidBrush MyBrush;
        Font MyFont = new Font("Arial", 10);
        public Node Competition(ref Node ph)//сортировка
        {
            if (ph.left != null)//идем внутрь
            {
                if (ph.left.data == ph.data)
                    ph.left = Competition(ref ph.left);
                else
                    ph.right = Competition(ref ph.right);

            }
            else
            {
                if (ph.right != null)
                    ph.right = Competition(ref ph.right);
            }
            if ((ph.left == null) && (ph.right == null))
                ph = null;
            else//состязание сыновей
            {
                
                if (ph.left == null)
                    ph.data = ph.right.data;
                else
                {
                    if (ph.right == null)
                        ph.data = ph.left.data;
                    else
                    {
                        if (Convert.ToInt32(ph.left.data) > Convert.ToInt32(ph.right.data))
                            ph.data = ph.right.data;
                        else
                            ph.data = ph.left.data;
                    }
                }

                
            }
            return ph;
        }
        public Node HeapCreate(int H)//создание дерева
        {
            Node pLevel, pNew, pOld, comp1, comp2;
            int x = deltaX;
            int y = H - diametr - deltaY;
            pLevel = new Node(null, null, a[0], x, y);//первый эл
            pOld = pLevel;
            pNew = null;
            for (int i = 1; i < a.Length; i++)//включение в список элементов массива
            {
                x = x + deltaX + R;
                pNew = new Node(null, null, a[i], x, y);
                pOld.next = pNew;//линейный список
                pOld = pNew;
            }
            pNew.next = null;
            //построение уровней
            while (pLevel.next != null)//цикл до вершины пирамиды
            {
                y = y - deltaY - diametr;
                comp1 = pLevel;
                pLevel = null;
                //начало нового уровня
                while (comp1 != null)//цикл по очередному уровню
                {
                    comp2 = comp1.next;//адреса потомков из пред уровня
                    pNew = new Node(comp1, comp2, 0, x, y);//связывание в список
                    if (pLevel == null)
                        pLevel = pNew;
                    else
                        pOld.next = pNew;
                    pOld = pNew;
                    if ((comp2 == null) || (Convert.ToInt32(comp2.data) > Convert.ToInt32(comp1.data)))//состязание за выход на уровень
                        pNew.data = comp1.data;
                    else
                        pNew.data = comp2.data;
                    if (comp2 == null)
                        x = comp1.x;
                    else
                        x = (comp1.x + comp2.x) / 2;
                    pNew.x = x;
                    pNew.y = y;
                    //переход к следующей паре
                    if (comp2 != null)
                        comp1 = comp2.next;
                    else
                        comp1 = null;

                }
            }
            return pLevel;
        }
        public void DrawNode(Node p)//НАрисовать дерево
        {
            try
            {
                int R = 12;
                if (p.left != null)
                    g.DrawLine(MyPen, p.x, p.y, p.left.x, p.left.y);
                if (p.right != null)
                    g.DrawLine(MyPen, p.x, p.y, p.right.x, p.right.y);
                
                    MyBrush = (SolidBrush)Brushes.LightYellow;
                g.FillEllipse(MyBrush, p.x - R, p.y - R, 2 * R, 2 * R);
                g.DrawEllipse(MyPen, p.x - R, p.y - R, 2 * R, 2 * R);
                string s = Convert.ToString(p.data);
                SizeF size = g.MeasureString(s, MyFont);
                g.DrawString(s, MyFont, Brushes.Black, p.x - size.Width / 2, p.y - size.Height / 2);
                if (p.left != null)
                    DrawNode(p.left);
                if (p.right != null)
                    DrawNode(p.right);
            }
            catch
            {

            }

        }
        public void Draw()
        {
            Color cl = Color.FromArgb(255, 255, 255);
            g.Clear(cl);
            MyPen = Pens.Black;
            if (head != null)
                DrawNode(head);
            int L = array.Length;
            for (int i = 0; i<L; i++)
            {
                g.FillEllipse(MyBrush, array[i].x - R, array[i].y - R, 2 * R, 2 * R);
                g.DrawEllipse(MyPen, array[i].x - R, array[i].y - R, 2 * R, 2 * R);
                string s = Convert.ToString(array[i].data);
                SizeF size = g.MeasureString(s, MyFont);
                g.DrawString(s, MyFont, Brushes.Black, array[i].x - size.Width / 2, array[i].y - size.Height / 2);
            }
            
        }

    }
}
