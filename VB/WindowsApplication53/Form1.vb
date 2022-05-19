Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraPivotGrid

Namespace WindowsApplication53

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            PopulateTable()
            pivotGridControl1.RefreshData()
            pivotGridControl1.BestFit()
        End Sub

        Private Sub PopulateTable()
            Dim myTable As DataTable = dataSet1.Tables("Data")
            myTable.Rows.Add(New Object() {"Aaa", Date.Today, 7})
            myTable.Rows.Add(New Object() {"Aaa", Date.Today.AddDays(1), 4})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today, 12})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today.AddDays(1), 12})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today, 11})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddDays(1), 9})
            myTable.Rows.Add(New Object() {"Aaa", Date.Today.AddYears(1), 4})
            myTable.Rows.Add(New Object() {"Aaa", Date.Today.AddYears(1).AddDays(1), 2})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today.AddYears(1), 12})
            myTable.Rows.Add(New Object() {"Bbb", Date.Today.AddDays(1).AddYears(1), 1})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddYears(1), 9})
            myTable.Rows.Add(New Object() {"Ccc", Date.Today.AddDays(1).AddYears(1), 22})
        End Sub

        Private Sub pivotGridControl1_CustomDrawCell(ByVal sender As Object, ByVal e As PivotCustomDrawCellEventArgs)
            Dim xl As Integer = FindLeftMostEqualCell(e)
            Dim xr As Integer = FindRightMostEqualCell(e, CType(sender, PivotGridControl).Cells.ColumnCount)
            If xl = e.ColumnIndex AndAlso xr > e.ColumnIndex Then
                Dim width As Integer = e.Bounds.Width
                Dim left As Integer = e.Bounds.Left - width * (e.ColumnIndex - xl)
                Dim right As Integer = e.Bounds.Right + width * (xr - e.ColumnIndex) - 1
                Dim rec As Rectangle = New Rectangle(left, e.Bounds.Top, right - left, e.Bounds.Height - 1)
                e.Graphics.FillRectangle(e.Appearance.GetBackBrush(e.GraphicsCache), rec)
                Dim sf As StringFormat = New StringFormat()
                sf.Alignment = StringAlignment.Center
                sf.LineAlignment = StringAlignment.Center
                e.Graphics.DrawString(e.DisplayText, e.Appearance.Font, e.Appearance.GetForeBrush(e.GraphicsCache), rec, sf)
                e.Handled = True
            End If

            If xl < e.ColumnIndex Then e.Handled = True
        End Sub

        Private Function FindRightMostEqualCell(ByVal e As PivotCustomDrawCellEventArgs, ByVal columnCount As Integer) As Integer
            Dim x As Integer = e.ColumnIndex
            For i As Integer = e.ColumnIndex + 1 To columnCount - 1
                If e.GetCellValue(e.ColumnIndex, e.RowIndex).Equals(e.GetCellValue(i, e.RowIndex)) Then
                    x = i
                Else
                    Return x
                End If
            Next

            Return x
        End Function

        Private Function FindLeftMostEqualCell(ByVal e As PivotCustomDrawCellEventArgs) As Integer
            Dim x As Integer = e.ColumnIndex
            For i As Integer = e.ColumnIndex - 1 To 0 Step -1
                If e.GetCellValue(e.ColumnIndex, e.RowIndex).Equals(e.GetCellValue(i, e.RowIndex)) Then
                    x = i
                Else
                    Return x
                End If
            Next

            Return x
        End Function
    End Class
End Namespace
