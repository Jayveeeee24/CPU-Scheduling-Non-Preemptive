Imports System.ComponentModel

Public Class MainForm
    Dim currentRowNumber As Integer = 1
#Region "        MAIN"
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mainTabControl.ItemSize = New Size(0, 1)

        btnFCFS.PerformClick()
    End Sub
#End Region

#Region "   NAVIGATIONS"
    Private Sub btnFCFS_Click(sender As Object, e As EventArgs) Handles btnFCFS.Click
        mainTabControl.SelectedTab = pageFcfs
        labelTitle.Text = "First Come First Serve (FCFS)"
        initialSetup(datagridInitial, datagridComputation, btnAddRow, labelAveTurn, labelAveWait)
        'datagridInitial.Enabled = True
        'btnAddRow.Enabled = True
        'btnRemoveRow.Enabled = true

        btnFCFS.BackColor = Color.FromArgb(52, 152, 219)
        btnSJF.BackColor = Color.FromArgb(25, 117, 211)
        btnPRIO.BackColor = Color.FromArgb(25, 117, 211)
    End Sub
    Private Sub btnSJF_Click(sender As Object, e As EventArgs) Handles btnSJF.Click
        mainTabControl.SelectedTab = pageSjf
        currentRowNumber = 1
        labelTitle.Text = "Shortest Job First (SJF)"

        btnSJF.BackColor = Color.FromArgb(52, 152, 219)
        btnFCFS.BackColor = Color.FromArgb(25, 117, 211)
        btnPRIO.BackColor = Color.FromArgb(25, 117, 211)
    End Sub
    Private Sub btnPRIO_Click(sender As Object, e As EventArgs) Handles btnPRIO.Click
        mainTabControl.SelectedTab = pagePriority
        labelTitle.Text = "Priority Scheduling"

        btnPRIO.BackColor = Color.FromArgb(52, 152, 219)
        btnSJF.BackColor = Color.FromArgb(25, 117, 211)
        btnFCFS.BackColor = Color.FromArgb(25, 117, 211)
    End Sub
#End Region


#Region "     FCFS CONTROLS"
    Private Sub btnAddRow_Click(sender As Object, e As EventArgs) Handles btnAddRow.Click
        datagridInitial.Rows.Add("P" & currentRowNumber.ToString, "", "")
        currentRowNumber += 1
    End Sub
    Private Sub btnRemoveRow_Click(sender As Object, e As EventArgs) Handles btnRemoveRow.Click
        If (datagridInitial.Rows.Count <> 0) Then
            datagridInitial.Rows.RemoveAt(datagridInitial.Rows.Count - 1)
            currentRowNumber -= 1
        Else
            currentRowNumber = 1
            MsgBox("There is no more rows to remove!", vbExclamation, "Warning")
        End If
    End Sub
    Private Sub Clear_Click(sender As Object, e As EventArgs) Handles Clear.Click
        currentRowNumber = 1
        tableGanttChart.Controls.Clear()
        tableGanttChart.ColumnStyles.Clear()
        tableGanttChart.ColumnCount = 0
        btnFCFS.PerformClick()
    End Sub
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        'cell validation
        If cellValidation() = True Then
            Exit Sub
        End If

        'checking of buttons if start or stop
        If (btnStart.Text = "Start") Then
            btnStart.Text = "Stop"
            btnFinish.Enabled = False

            btnStart.BackColor = Color.FromArgb(192, 57, 43)
            btnStart.FlatAppearance.MouseDownBackColor = Color.FromArgb(231, 76, 60)
            btnStart.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 76, 60)

        Else
            btnStart.Text = "Start"
            btnFinish.Enabled = True

            btnStart.BackColor = Color.FromArgb(39, 174, 96)
            btnStart.FlatAppearance.MouseDownBackColor = Color.FromArgb(46, 204, 113)
            btnStart.FlatAppearance.MouseOverBackColor = Color.FromArgb(46, 204, 113)

            Exit Sub
        End If

        If computeFCFS(0.6) = datagridInitial.Rows.Count - 1 Then
            MsgBox("CPU SCHEDULE FINISHED!", vbInformation, "PROCESS FINISHED")

            datagridInitial.ClearSelection()
            datagridComputation.ClearSelection()
            btnFinish.Enabled = True
            btnStart.Text = "Start"

            btnStart.BackColor = Color.FromArgb(39, 174, 96)
            btnStart.FlatAppearance.MouseDownBackColor = Color.FromArgb(46, 204, 113)
            btnStart.FlatAppearance.MouseOverBackColor = Color.FromArgb(46, 204, 113)
        End If
    End Sub
    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click
        If cellValidation() = True Then
            Exit Sub
        End If
        computeFCFS(0)
        MsgBox("CPU SCHEDULE FINISHED!", vbInformation, "PROCESS FINISHED")

        datagridInitial.ClearSelection()
        datagridComputation.ClearSelection()
    End Sub
    Private Sub datagridInitial_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles datagridInitial.CellValidating
        If (e.FormattedValue Is Nothing Or e.FormattedValue = "" Or String.IsNullOrWhiteSpace(e.FormattedValue)) Then
            MsgBox("Value cannot be null!", vbExclamation, "Null Data Input")
            e.Cancel = True
        End If
    End Sub
    Private Sub btnInfo_Click(sender As Object, e As EventArgs) Handles btnInfo.Click

    End Sub
#End Region


#Region "    Function Helpers"
    Private Sub wait(ByVal seconds As Integer)
        For j As Integer = 0 To seconds * 100
            System.Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next
    End Sub
    Private Function cellValidation() As Boolean
        Dim allInitialValues(datagridInitial.RowCount, 3) As String

        'datagrid and cell validation
        For i As Integer = 0 To datagridInitial.RowCount - 1
            For j As Integer = 0 To datagridInitial.ColumnCount - 1
                allInitialValues(i, j) = datagridInitial.Rows(i).Cells.Item(j).FormattedValue
                If (datagridInitial.Rows(i).Cells.Item(j).FormattedValue Is Nothing Or datagridInitial.Rows(i).Cells.Item(j).FormattedValue = "" Or String.IsNullOrWhiteSpace(datagridInitial.Rows(i).Cells.Item(j).FormattedValue)) Then
                    MsgBox("Please provide complete values!", vbExclamation, "Null Data Input")
                    Return True
                End If
            Next

            For k As Integer = i + 1 To datagridInitial.RowCount - 1
                If (datagridInitial.Rows(i).Cells.Item(0).FormattedValue) = (datagridInitial.Rows(k).Cells.Item(0).FormattedValue) Then
                    MsgBox("Process ID should be unique in each row!", vbExclamation, "Duplicate Data")
                    Return True
                End If
            Next
        Next
        Return False
    End Function
    Private Sub initialSetup(datagridInitial As DataGridView, datagridDestination As DataGridView, btnAdd As Button, labelTurn As Label, labelWait As Label)
        currentRowNumber = 1

        datagridInitial.Rows.Clear()
        datagridDestination.Rows.Clear()

        For i As Integer = 0 To 2
            btnAdd.PerformClick()
        Next

        labelTurn.Text = ""
        labelWait.Text = ""
    End Sub
    Private Function computeFCFS(waitTime As Integer) As Integer
        Dim loopCount As Integer

        'datagridInitial.Sort(datagridInitial.Columns("processID"), ListSortDirection.Ascending)
        'datagridInitial.Sort(datagridInitial.Columns("arrivalTime"), ListSortDirection.Ascending)

        SortDataGridView()

        datagridComputation.Rows.Clear()

        Dim currentTime As Integer = 0
        Dim totalWaitingTime As Integer = 0
        Dim totalTurnaroundTime As Integer = 0
        Dim completionTime As Integer = 0
        Dim turnAroundTime As Integer = 0
        For i As Integer = 0 To datagridInitial.Rows.Count - 1
            If currentTime < datagridInitial.Rows(i).Cells(1).Value Then
                currentTime = datagridInitial.Rows(i).Cells(1).Value
            End If

            'waiting time = previous arrival time - current a
            Dim waitingTime As Integer = currentTime - datagridInitial.Rows(i).Cells(1).Value
            totalWaitingTime += waitingTime

            currentTime += datagridInitial.Rows(i).Cells(2).Value 'add the current time and burst time for completion time

            completionTime = currentTime

            'MsgBox("Process " & (i + 1) & " - Waiting Time: " & waitingTime & " - Completion Time " & completionTime)

            turnAroundTime = completionTime - datagridInitial.Rows(i).Cells(1).Value
            totalTurnaroundTime += turnAroundTime

            datagridComputation.Rows.Add(datagridInitial.Rows(i).Cells(0).Value, "", "", "")
            wait(waitTime)
            datagridComputation.Rows(i).Cells(1).Value = completionTime
            wait(waitTime)
            datagridComputation.Rows(i).Cells(2).Value = turnAroundTime
            wait(waitTime)
            datagridComputation.Rows(i).Cells(3).Value = waitingTime
            wait(waitTime)

            loopCount = i
        Next

        'datagridInitial.Sort(datagridInitial.Columns(0), ListSortDirection.Ascending)
        'datagridComputation.Sort(datagridComputation.Columns(0), ListSortDirection.Ascending)

        labelAveWait.Text = totalWaitingTime / datagridInitial.Rows.Count
        labelAveTurn.Text = totalTurnaroundTime / datagridInitial.Rows.Count


        Return loopCount
    End Function
    Public Class Process
        Public Property ProcessID As String
        Public Property ArrivalTime As Integer
        Public Property BurstTime As Integer
    End Class
    Private Sub SortDataGridView()
        ' Assuming your DataGridView is named "dataGridView1"
        Dim dataGridView As DataGridView = datagridInitial

        ' Get the data from the DataGridView
        Dim data As New List(Of Process)()

        ' Iterate over the DataGridView rows and populate the data list
        For Each row As DataGridViewRow In dataGridView.Rows
            If Not row.IsNewRow Then
                Dim process As New Process() With {
                    .ProcessID = row.Cells("processID").Value.ToString(),
                    .ArrivalTime = Convert.ToInt32(row.Cells("arrivalTime").Value),
                    .BurstTime = Convert.ToInt32(row.Cells("burstTime").Value)
                }
                data.Add(process)
            End If
        Next

        ' Sort the data using LINQ
        data = data.OrderBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.ProcessID).ToList()

        tableGanttChart.Controls.Clear()
        tableGanttChart.ColumnStyles.Clear()
        tableGanttChart.ColumnCount = 0

        ' Add labels to the TableLayoutPanel
        Dim currentColumn As Integer = 0

        ' Clear the DataGridView
        dataGridView.Rows.Clear()
        Dim i As Integer = 0
        ' Add sorted data back to the DataGridView
        For Each process As Process In data


            Dim label As New Label()
            label.Text = process.ProcessID
            label.TextAlign = ContentAlignment.MiddleCenter
            label.BackColor = Color.LightBlue
            label.Dock = DockStyle.Fill

            ' Calculate the width of the column based on the burst time percentage
            Dim percentSize As Single = process.BurstTime / data.Sum(Function(p) p.BurstTime)
            Dim columnWidth As Integer = CInt(percentSize * tableGanttChart.Width)

            ' Set the column style and add the label to the TableLayoutPanel
            tableGanttChart.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, columnWidth))
            tableGanttChart.Controls.Add(label, currentColumn, 0)

            ' Increment the column index
            currentColumn += 1

            dataGridView.Rows.Add(process.ProcessID, process.ArrivalTime, process.BurstTime)
        Next
    End Sub
#End Region
End Class
