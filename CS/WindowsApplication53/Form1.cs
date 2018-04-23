using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraPivotGrid;

namespace WindowsApplication53
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateTable();
            pivotGridControl1.RefreshData();
            pivotGridControl1.BestFit();
        }
        private void PopulateTable()
        {
            DataTable myTable = dataSet1.Tables["Data"];
            myTable.Rows.Add(new object[] {"Aaa", DateTime.Today, 7});
            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today.AddDays(1), 4 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today, 12 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(1), 12 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today, 11 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1), 9 });

            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today.AddYears(1), 4 });
            myTable.Rows.Add(new object[] { "Aaa", DateTime.Today.AddYears(1).AddDays(1), 2 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddYears(1), 12 });
            myTable.Rows.Add(new object[] { "Bbb", DateTime.Today.AddDays(1).AddYears(1), 1 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddYears(1), 9 });
            myTable.Rows.Add(new object[] { "Ccc", DateTime.Today.AddDays(1).AddYears(1), 22 });
        }

        private void pivotGridControl1_CustomDrawCell(object sender, DevExpress.XtraPivotGrid.PivotCustomDrawCellEventArgs e)
        {
            int xl = FindLeftMostEqualCell(e);
            int xr = FindRightMostEqualCell(e, ((PivotGridControl)sender).Cells.ColumnCount)  ;

            if (xl == e.ColumnIndex && xr > e.ColumnIndex)
            {
                int width = e.Bounds.Width;
                int left = e.Bounds.Left - width * ( e.ColumnIndex - xl ) ;
                int right = e.Bounds.Right + width * (xr - e.ColumnIndex) - 1;
                Rectangle rec = new Rectangle(left, e.Bounds.Top , right - left, e.Bounds.Height -1 );
                
                e.Graphics.FillRectangle(e.Appearance.GetBackBrush(e.GraphicsCache) , rec );
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                
                e.Graphics.DrawString(e.DisplayText, e.Appearance.Font , e.Appearance.GetForeBrush(e.GraphicsCache ), rec, sf);
                e.Handled = true;
            }
            if (xl < e.ColumnIndex)
                e.Handled = true;
        }

        private int FindRightMostEqualCell(DevExpress.XtraPivotGrid.PivotCustomDrawCellEventArgs e, int columnCount)
        {
            int x = e.ColumnIndex;
            for (int i = e.ColumnIndex + 1 ; i < columnCount; i++)
            {
                if (e.GetCellValue(e.ColumnIndex, e.RowIndex).Equals(e.GetCellValue(i, e.RowIndex)))
                    x = i;
                else
                    return x;
            }
            return x;
        }

        private int FindLeftMostEqualCell(DevExpress.XtraPivotGrid.PivotCustomDrawCellEventArgs e)
        {
            int x = e.ColumnIndex;
            for (int i = e.ColumnIndex -1 ; i >= 0; i--)
            {
                if (e.GetCellValue(e.ColumnIndex, e.RowIndex).Equals(e.GetCellValue(i, e.RowIndex)))
                    x = i;
                else
                    return x;
            }
            return x;
        }
       
    }
}