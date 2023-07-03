Imports System.ComponentModel
Imports System.Threading

Public Class MainForm
    Dim currentRowNumber As Int32 = 1
    Dim currentPage As String
#Region "        MAIN"
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        btnFCFS.PerformClick()
    End Sub
#End Region

#Region "   NAVIGATIONS"
    Private Sub btnFCFS_Click(sender As Object, e As EventArgs) Handles btnFCFS.Click
        labelTitle.Text = "First Come First Serve (FCFS)"
        initialSetup(datagridInitial, datagridComputation, btnAddRow, labelAveTurn, labelAveWait)
        currentPage = "FCFS"
        If datagridInitial.Columns.Count = 4 Then
            datagridInitial.Columns.RemoveAt(3)
        End If

        btnFCFS.BackColor = Color.FromArgb(52, 152, 219)
        btnSJF.BackColor = Color.FromArgb(25, 117, 211)
        btnPRIO.BackColor = Color.FromArgb(25, 117, 211)
    End Sub
    Private Sub btnSJF_Click(sender As Object, e As EventArgs) Handles btnSJF.Click
        initialSetup(datagridInitial, datagridComputation, btnAddRow, labelAveTurn, labelAveWait)
        currentPage = "SJF"
        labelTitle.Text = "Shortest Job First (SJF)"
        If datagridInitial.Columns.Count = 4 Then
            datagridInitial.Columns.RemoveAt(3)
        End If

        btnSJF.BackColor = Color.FromArgb(52, 152, 219)
        btnFCFS.BackColor = Color.FromArgb(25, 117, 211)
        btnPRIO.BackColor = Color.FromArgb(25, 117, 211)
    End Sub
    Private Sub btnPRIO_Click(sender As Object, e As EventArgs) Handles btnPRIO.Click
        initialSetup(datagridInitial, datagridComputation, btnAddRow, labelAveTurn, labelAveWait)
        currentPage = "PRIO"
        labelTitle.Text = "Priority Scheduling"
        If datagridInitial.Columns.Count < 4 Then
            datagridInitial.Columns.Add("priority", "Priority")
        End If

        btnPRIO.BackColor = Color.FromArgb(52, 152, 219)
        btnSJF.BackColor = Color.FromArgb(25, 117, 211)
        btnFCFS.BackColor = Color.FromArgb(25, 117, 211)
    End Sub
#End Region


#Region "     FCFS CONTROLS"
    Private Sub btnAddRow_Click(sender As Object, e As EventArgs) Handles btnAddRow.Click
        datagridInitial.Rows.Add("P" & currentRowNumber.ToString("D2"), "", "")
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
    Private Sub Clear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        currentRowNumber = 1
        If currentPage = "FCFS" Then
            btnFCFS.PerformClick()
        ElseIf currentPage = "SJF" Then
            btnSJF.PerformClick()
        ElseIf currentPage = "PRIO" Then
            btnPRIO.PerformClick()
        End If
        tableGanttChart.Controls.Clear()
        tableGanttChart.ColumnStyles.Clear()
        tableGanttChart.ColumnCount = 0
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
            btnAddRow.Enabled = False
            btnRemoveRow.Enabled = False
            btnClear.Enabled = False
            btnRandom.Enabled = False

            btnStart.BackColor = Color.FromArgb(192, 57, 43)
            btnStart.FlatAppearance.MouseDownBackColor = Color.FromArgb(231, 76, 60)
            btnStart.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 76, 60)

        Else
            btnStart.Text = "Start"
            btnFinish.Enabled = True
            btnAddRow.Enabled = True
            btnRemoveRow.Enabled = True
            btnClear.Enabled = True
            btnRandom.Enabled = True

            btnStart.BackColor = Color.FromArgb(39, 174, 96)
            btnStart.FlatAppearance.MouseDownBackColor = Color.FromArgb(46, 204, 113)
            btnStart.FlatAppearance.MouseOverBackColor = Color.FromArgb(46, 204, 113)

            Exit Sub
        End If

        datagridComputation.Rows.Clear()
        If compute(0.6) = datagridInitial.Rows.Count - 1 And btnStart.Text = "Stop" Then
            MsgBox("CPU SCHEDULE FINISHED!", vbInformation, "PROCESS FINISHED")

            datagridInitial.ClearSelection()
            datagridComputation.ClearSelection()
            btnFinish.Enabled = True
            btnAddRow.Enabled = True
            btnRemoveRow.Enabled = True
            btnClear.Enabled = True
            btnRandom.Enabled = True
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
        compute(0)
        MsgBox("CPU SCHEDULE FINISHED!", vbInformation, "PROCESS FINISHED")

        datagridInitial.ClearSelection()
        datagridComputation.ClearSelection()
    End Sub
    Private Sub datagridInitial_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles datagridInitial.CellValidating
        If (e.FormattedValue Is Nothing Or e.FormattedValue = "" Or String.IsNullOrWhiteSpace(e.FormattedValue)) Then
            MsgBox("Value cannot be null!", vbExclamation, "Null Data Input")
            e.Cancel = True
            Exit Sub
        End If

        If e.ColumnIndex > 0 Then
            Dim value As String = e.FormattedValue.ToString()
            Dim number As Integer
            If Not Integer.TryParse(value, number) Then
                e.Cancel = True
                MsgBox("Please enter a valid number.", vbCritical, "Warning")
            End If
        End If
    End Sub
    Private Sub btnInfo_Click(sender As Object, e As EventArgs) Handles btnInfo.Click

    End Sub
    Private Sub btnRandom_Click(sender As Object, e As EventArgs) Handles btnRandom.Click
        Dim random As New Random()
        Dim randomRows As Integer = random.Next(3, 10)

        datagridInitial.Rows.Clear()
        datagridComputation.Rows.Clear()
        tableGanttChart.Controls.Clear()
        tableGanttChart.ColumnStyles.Clear()
        tableGanttChart.ColumnCount = 0
        currentRowNumber = 1
        For i As Integer = 0 To randomRows
            btnAddRow.PerformClick()
        Next

        For i As Integer = 0 To datagridInitial.Rows.Count - 1
            If currentPage = "PRIO" Then
                For j As Integer = 1 To 3
                    Dim randomArrival As Integer = random.Next(0, 10)
                    Dim randomBurst As Integer = random.Next(1, 10)
                    Dim randomPriority As Integer = random.Next(1, 5)
                    If j = 1 Then
                        datagridInitial.Rows(i).Cells(j).Value = randomArrival
                    ElseIf j = 2 Then
                        datagridInitial.Rows(i).Cells(j).Value = randomBurst
                    Else
                        datagridInitial.Rows(i).Cells(j).Value = randomPriority
                    End If
                Next
            Else
                For j As Integer = 1 To 2
                    Dim randomArrival As Integer = random.Next(0, 10)
                    Dim randomBurst As Integer = random.Next(1, 10)
                    If j = 1 Then
                        datagridInitial.Rows(i).Cells(j).Value = randomArrival
                    Else
                        datagridInitial.Rows(i).Cells(j).Value = randomBurst
                    End If
                Next
            End If
        Next


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
        tableGanttChart.Controls.Clear()
        tableGanttChart.ColumnStyles.Clear()
        tableGanttChart.ColumnCount = 0

        For i As Integer = 0 To 2
            btnAdd.PerformClick()
        Next

        labelTurn.Text = ""
        labelWait.Text = ""
    End Sub
    Private Function compute(waitTime As Integer) As Integer

        Dim loopCount As Integer

        SortDataGridView(waitTime)

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

        datagridInitial.Sort(datagridInitial.Columns(0), ListSortDirection.Ascending)
        datagridComputation.Sort(datagridComputation.Columns(0), ListSortDirection.Ascending)

        labelAveWait.Text = Math.Round(totalWaitingTime / datagridInitial.Rows.Count, 2)
        labelAveTurn.Text = Math.Round(totalTurnaroundTime / datagridInitial.Rows.Count, 2)


        Return loopCount
    End Function

    Public Class Process
        Public Property ProcessID As String
        Public Property ArrivalTime As Integer
        Public Property BurstTime As Integer

        Public Property Priority As Integer
    End Class

#Region "Sorting na walang idle states"
    '    Private Sub SortDataGridView()
    '        Dim dataGridView As DataGridView = datagridInitial

    '        ' Get the data from the DataGridView
    '        Dim data As New List(Of Process)()

    '        ' Iterate over the DataGridView rows and populate the data list
    '        For Each row As DataGridViewRow In dataGridView.Rows
    '            If Not row.IsNewRow Then
    '                If currentPage = "PRIO" Then
    '                    Dim process As New Process() With {
    '               .ProcessID = row.Cells("processID").Value.ToString(),
    '               .ArrivalTime = Convert.ToInt32(row.Cells("arrivalTime").Value),
    '               .BurstTime = Convert.ToInt32(row.Cells("burstTime").Value),
    '               .Priority = Convert.ToInt32(row.Cells("priority").Value)
    '           }
    '                    data.Add(process)
    '                Else
    '                    Dim process As New Process() With {
    '               .ProcessID = row.Cells("processID").Value.ToString(),
    '               .ArrivalTime = Convert.ToInt32(row.Cells("arrivalTime").Value),
    '               .BurstTime = Convert.ToInt32(row.Cells("burstTime").Value)
    '           }
    '                    data.Add(process)
    '                End If
    '            End If
    '        Next

    '        dataGridView.Rows.Clear()
    '        'actual sorting via list
    '        If currentPage = "FCFS" Then
    '            data = data.OrderBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.ProcessID).ToList()
    '            tableGanttChart.Controls.Clear()
    '            tableGanttChart.ColumnStyles.Clear()
    '            tableGanttChart.ColumnCount = 0
    '            Dim currentColumn As Integer = 0

    '            ' Add sorted data back to the DataGridView
    '            For Each process In data

    '                Dim label As New Label()
    '                label.Text = process.ProcessID
    '                label.TextAlign = ContentAlignment.MiddleCenter
    '                label.BackColor = Color.LightBlue
    '                label.Dock = DockStyle.Fill
    '                Dim percentSize As Single = process.BurstTime / data.Sum(Function(p) p.BurstTime)
    '                Dim columnWidth As Integer = CInt(percentSize * tableGanttChart.Width)

    '                ' Set the column style and add the label to the TableLayoutPanel
    '                tableGanttChart.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, columnWidth))
    '                tableGanttChart.Controls.Add(label, currentColumn, 0)

    '                ' Increment the column index
    '                currentColumn += 1

    '                dataGridView.Rows.Add(process.ProcessID, process.ArrivalTime, process.BurstTime)
    '            Next
    '        ElseIf currentPage = "SJF" Then

    '#Region "DATING CODE"
    '            'data = data.OrderBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.BurstTime).ThenBy(Function(p) p.ProcessID).ToList()

    '            'Dim counter As Integer = 0
    '            'Dim totalSum As Integer = dataGridView.Rows.Cast(Of DataGridViewRow)().Sum(Function(row1) Convert.ToInt32(row1.Cells("burstTime").Value))

    '            'While dataGridView.Rows.Count < data.Count

    '            '    If dataGridView.Rows.Count = 0 Then
    '            '        dataGridView.Rows.Add(data(0).ProcessID, data(0).ArrivalTime, data(0).BurstTime)
    '            '    Else

    '            '        'selects the value in 'data' list where its
    '            '        'arrivalTime <= totalSum and
    '            '        'its processId is not on the datagridview [to check for duplicate values]
    '            '        'tpos i sort naten sa pinakamababang burstTime
    '            '        '[tie breakers order]: burstTime, arrivalTime then processId
    '            '        Dim firstProcess As Process = data.Where(Function(p) p.ArrivalTime <= totalSum AndAlso Not dataGridView.Rows.Cast(Of DataGridViewRow)().Any(Function(row) row.Cells("processID").Value.ToString() = p.ProcessID)).OrderBy(Function(p) p.BurstTime).ThenBy(Function(p) p.ArrivalTime).FirstOrDefault()

    '            '        If firstProcess IsNot Nothing Then
    '            '            dataGridView.Rows.Add(firstProcess.ProcessID, firstProcess.ArrivalTime, firstProcess.BurstTime)
    '            '            totalSum = dataGridView.Rows.Cast(Of DataGridViewRow)().Sum(Function(row1) Convert.ToInt32(row1.Cells("burstTime").Value))
    '            '        Else
    '            '            totalSum += 1 'increments the totalBurstTime to 1 because there is no value to be next in line
    '            '        End If

    '            '    End If

    '            'End While
    '#End Region

    '            Dim currentTime As Integer = 0
    '            While dataGridView.Rows.Count < data.Count
    '                Dim eligibleProcesses = data.Where(Function(p) p.ArrivalTime <= currentTime AndAlso Not dataGridView.Rows.Cast(Of DataGridViewRow)().Any(Function(row) row.Cells("processID").Value.ToString() = p.ProcessID)).OrderBy(Function(p) p.BurstTime).ThenBy(Function(p) p.ArrivalTime).ToList()

    '                If eligibleProcesses.Count > 0 Then
    '                    Dim firstProcess As Process = eligibleProcesses(0)
    '                    dataGridView.Rows.Add(firstProcess.ProcessID, firstProcess.ArrivalTime, firstProcess.BurstTime)
    '                    currentTime += firstProcess.BurstTime
    '                Else
    '                    currentTime += 1 ' Increment the currentTime by 1 because there is no eligible process to be scheduled
    '                End If
    '            End While
    '        ElseIf currentPage = "PRIO" Then
    '            Dim currentTime As Integer = 0
    '            While dataGridView.Rows.Count < data.Count
    '                Dim eligibleProcesses = data.Where(Function(p) p.ArrivalTime <= currentTime AndAlso Not dataGridView.Rows.Cast(Of DataGridViewRow)().Any(Function(row) row.Cells("processID").Value.ToString() = p.ProcessID)).OrderBy(Function(p) p.Priority).ThenBy(Function(p) p.ArrivalTime).ToList()

    '                If eligibleProcesses.Count > 0 Then
    '                    Dim firstProcess As Process = eligibleProcesses(0)
    '                    dataGridView.Rows.Add(firstProcess.ProcessID, firstProcess.ArrivalTime, firstProcess.BurstTime)
    '                    currentTime += firstProcess.BurstTime
    '                Else
    '                    currentTime += 1 ' Increment the currentTime by 1 because there is no eligible process to be scheduled
    '                End If
    '            End While

    '        End If

    '    End Sub
#End Region

#Region "Sorting with the idle state"
    'Private Sub SortDataGridView()
    '    Dim dataGridView As DataGridView = datagridInitial

    '    ' Get the data from the DataGridView
    '    Dim data As New List(Of Process)()

    '    ' Iterate over the DataGridView rows and populate the data list
    '    For Each row As DataGridViewRow In dataGridView.Rows
    '        If Not row.IsNewRow Then
    '            If currentPage = "PRIO" Then
    '                Dim process As New Process() With {
    '                    .ProcessID = row.Cells("processID").Value.ToString(),
    '                    .ArrivalTime = Convert.ToInt32(row.Cells("arrivalTime").Value),
    '                    .BurstTime = Convert.ToInt32(row.Cells("burstTime").Value),
    '                    .Priority = Convert.ToInt32(row.Cells("priority").Value)
    '                }
    '                data.Add(process)
    '            Else
    '                Dim process As New Process() With {
    '                    .ProcessID = row.Cells("processID").Value.ToString(),
    '                    .ArrivalTime = Convert.ToInt32(row.Cells("arrivalTime").Value),
    '                    .BurstTime = Convert.ToInt32(row.Cells("burstTime").Value)
    '                }
    '                data.Add(process)
    '            End If
    '        End If
    '    Next

    '    ' Sort the data based on the scheduling algorithm
    '    If currentPage = "FCFS" Then
    '        data = data.OrderBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.ProcessID).ToList()
    '    ElseIf currentPage = "SJF" Then
    '        data = data.OrderBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.BurstTime).ThenBy(Function(p) p.ProcessID).ToList()
    '    ElseIf currentPage = "PRIO" Then
    '        data = data.OrderBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.Priority).ThenBy(Function(p) p.ProcessID).ToList()
    '    End If

    '    dataGridView.Rows.Clear()
    '    tableGanttChart.Controls.Clear()
    '    tableGanttChart.ColumnStyles.Clear()
    '    tableGanttChart.ColumnCount = 0
    '    Dim currentColumn As Integer = 0
    '    Dim currentTime As Integer = 0

    '    ' Add sorted data and idle states to the Gantt Chart
    '    For Each process In data
    '        ' Add idle state if there is a gap between processes
    '        If process.ArrivalTime > currentTime Then
    '            Dim idleTime = process.ArrivalTime - currentTime

    '            Dim idleLabel As New Label()
    '            idleLabel.Text = "Idle"
    '            idleLabel.TextAlign = ContentAlignment.MiddleCenter
    '            idleLabel.BackColor = Color.Gray
    '            idleLabel.Dock = DockStyle.Fill
    '            Dim idleColumnWidth As Integer = CInt((idleTime / data.Sum(Function(p) p.BurstTime)) * tableGanttChart.Width)
    '            tableGanttChart.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, idleColumnWidth))
    '            tableGanttChart.Controls.Add(idleLabel, currentColumn, 0)
    '            currentColumn += 1

    '            currentTime += idleTime
    '        End If

    '        ' Add the process to the Gantt Chart
    '        Dim label As New Label()
    '        label.Text = process.ProcessID
    '        label.TextAlign = ContentAlignment.MiddleCenter
    '        label.BackColor = Color.LightBlue
    '        label.Dock = DockStyle.Fill
    '        Dim columnWidth As Integer = CInt((process.BurstTime / data.Sum(Function(p) p.BurstTime)) * tableGanttChart.Width)
    '        tableGanttChart.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, columnWidth))
    '        tableGanttChart.Controls.Add(label, currentColumn, 0)
    '        currentColumn += 1

    '        ' Add the process to the DataGridView
    '        dataGridView.Rows.Add(process.ProcessID, process.ArrivalTime, process.BurstTime)
    '        currentTime += process.BurstTime
    '    Next
    'End Sub
#End Region

    Private Sub SortDataGridView(waitTime As Integer)
        Dim dataGridView As DataGridView = datagridInitial

        ' Get the data from the DataGridView
        Dim data As New List(Of Process)()

        ' Iterate over the DataGridView rows and populate the data list
        For Each row As DataGridViewRow In dataGridView.Rows
            If Not row.IsNewRow Then
                If currentPage = "PRIO" Then
                    Dim process As New Process() With {
                    .ProcessID = row.Cells("processID").Value.ToString(),
                    .ArrivalTime = Convert.ToInt32(row.Cells("arrivalTime").Value),
                    .BurstTime = Convert.ToInt32(row.Cells("burstTime").Value),
                    .Priority = Convert.ToInt32(row.Cells("priority").Value)
                }
                    data.Add(process)
                Else
                    Dim process As New Process() With {
                    .ProcessID = row.Cells("processID").Value.ToString(),
                    .ArrivalTime = Convert.ToInt32(row.Cells("arrivalTime").Value),
                    .BurstTime = Convert.ToInt32(row.Cells("burstTime").Value)
                }
                    data.Add(process)
                End If
            End If
        Next

        ' Sort the data based on the scheduling algorithm
        If currentPage = "FCFS" Then
            data = data.OrderBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.ProcessID).ToList()
        ElseIf currentPage = "SJF" Then
            data = data.OrderBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.BurstTime).ThenBy(Function(p) p.ProcessID).ToList()
        ElseIf currentPage = "PRIO" Then
            data = data.OrderBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.Priority).ThenBy(Function(p) p.ProcessID).ToList()
        End If

        dataGridView.Rows.Clear()
        tableGanttChart.Controls.Clear()
        tableGanttChart.ColumnStyles.Clear()
        tableGanttChart.ColumnCount = 0
        Dim currentColumn As Integer = 0
        Dim currentTime As Integer = 0

        ' Add sorted data and idle states to the Gantt Chart
        For Each process In data
            ' Add idle state if there is a gap between processes
            If process.ArrivalTime > currentTime Then
                Dim idleTime = process.ArrivalTime - currentTime

                Dim idleLabel As New Label()
                idleLabel.Text = "Idle"
                idleLabel.TextAlign = ContentAlignment.MiddleCenter
                idleLabel.BackColor = Color.Gray
                idleLabel.Dock = DockStyle.Fill
                Dim idleColumnWidth As Integer = CInt((idleTime / data.Sum(Function(p) p.BurstTime)) * (tableGanttChart.Width - 1)) ' Subtracting 1 to account for the last column adjustment
                tableGanttChart.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, idleColumnWidth))
                tableGanttChart.Controls.Add(idleLabel, currentColumn, 0)
                currentColumn += 1

                currentTime += idleTime
            End If

            ' Add the process to the Gantt Chart
            Dim label As New Label()
            label.Text = process.ProcessID
            label.TextAlign = ContentAlignment.MiddleCenter
            label.BackColor = Color.LightBlue
            label.Dock = DockStyle.Fill
            Dim columnWidth As Integer = CInt((process.BurstTime / data.Sum(Function(p) p.BurstTime)) * (tableGanttChart.Width - 1)) ' Subtracting 1 to account for the last column adjustment
            wait(waitTime)
            tableGanttChart.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, columnWidth))
            tableGanttChart.Controls.Add(label, currentColumn, 0)

            currentColumn += 1
            currentTime += process.BurstTime

            ' Add the process to the DataGridView
            dataGridView.Rows.Add(process.ProcessID, process.ArrivalTime, process.BurstTime)
        Next

        ' Adjust the last column width to ensure it is fully visible
        tableGanttChart.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100)) ' Adding a last column with percent size of 100 to fill the remaining space
    End Sub

#End Region
End Class
