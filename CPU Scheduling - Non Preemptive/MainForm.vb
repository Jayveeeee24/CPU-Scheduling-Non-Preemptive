﻿Imports System.ComponentModel

Public Class MainForm
    Dim currentRowNumber As Integer = 1
    Dim currentPage As String
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
        currentPage = "FCFS"

        btnFCFS.BackColor = Color.FromArgb(52, 152, 219)
        btnSJF.BackColor = Color.FromArgb(25, 117, 211)
        btnPRIO.BackColor = Color.FromArgb(25, 117, 211)
    End Sub
    Private Sub btnSJF_Click(sender As Object, e As EventArgs) Handles btnSJF.Click
        mainTabControl.SelectedTab = pageFcfs
        initialSetup(datagridInitial, datagridComputation, btnAddRow, labelAveTurn, labelAveWait)
        currentPage = "SJF"
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

        datagridInitial.Sort(datagridInitial.Columns(0), ListSortDirection.Ascending)
        datagridComputation.Sort(datagridComputation.Columns(0), ListSortDirection.Ascending)

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

        'actual sorting via list
        If currentPage = "FCFS" Then
            data = data.OrderBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.ProcessID).ToList()
        ElseIf currentPage = "SJF" Then
            data = data.OrderBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.BurstTime).ThenBy(Function(p) p.ProcessID).ToList()
        End If


        dataGridView.Rows.Clear()

        ' Add sorted data back to the DataGridView
        If currentPage = "FCFS" Then
            For Each process In data
                dataGridView.Rows.Add(process.ProcessID, process.ArrivalTime, process.BurstTime)
            Next
        Else

            Dim counter As Integer = 0
            Dim totalSum As Integer = dataGridView.Rows.Cast(Of DataGridViewRow)().Sum(Function(row1) Convert.ToInt32(row1.Cells("burstTime").Value))

            While dataGridView.Rows.Count < data.Count
                If dataGridView.Rows.Count = 0 Then
                    dataGridView.Rows.Add(data(0).ProcessID, data(0).ArrivalTime, data(0).BurstTime)
                Else

                    'selects the value in 'data' list where its
                    'arrivalTime <= totalSum and
                    'its processId is not on the datagridview [to check for duplicate values]
                    'tpos i sort naten sa pinakamababang burstTime
                    '[tie breakers order]: burstTime, arrivalTime then processId
                    Dim firstProcess As Process = data.Where(Function(p) p.ArrivalTime <= totalSum AndAlso Not dataGridView.Rows.Cast(Of DataGridViewRow)().Any(Function(row) row.Cells("processID").Value.ToString() = p.ProcessID)).OrderBy(Function(p) p.BurstTime).ThenBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.ProcessID).FirstOrDefault()

                    If firstProcess IsNot Nothing Then
                        dataGridView.Rows.Add(firstProcess.ProcessID, firstProcess.ArrivalTime, firstProcess.BurstTime)
                        totalSum = dataGridView.Rows.Cast(Of DataGridViewRow)().Sum(Function(row1) Convert.ToInt32(row1.Cells("burstTime").Value))
                    Else
                        totalSum += 1 'increments the totalBurstTime to 1 because there is no value to be next in line
                    End If

                    'If temp.Count = 0 AndAlso counter = 1 Then
                    '    totalSum = dataGridView.Rows.Cast(Of DataGridViewRow)().Sum(Function(row1) Convert.ToInt32(row1.Cells("burstTime").Value)) + 1
                    'ElseIf temp.Count = 0 AndAlso counter = 0 Then
                    '    totalSum = dataGridView.Rows.Cast(Of DataGridViewRow)().Sum(Function(row1) Convert.ToInt32(row1.Cells("burstTime").Value))
                    '    counter = 1
                    'End If

                    'For Each process In data
                    '    If process.ArrivalTime <= totalSum Then
                    '        MsgBox("processID:" & process.ProcessID & " arrival:" & process.ArrivalTime.ToString & " totalSum:" & totalSum.ToString)
                    '        temp.Add(process)
                    '    End If
                    'Next

                    'For Each row As DataGridViewRow In dataGridView.Rows
                    '    For Each process In data
                    '        If process.ProcessID <> row.Cells("processID").Value.ToString() AndAlso process.ArrivalTime <= totalSum Then
                    '            temp.Add(process)
                    '            temp = temp.OrderBy(Function(p) p.BurstTime).ThenBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.ProcessID).ToList()

                    '            dataGridView.Rows.Add(temp(0).ProcessID, temp(0).ArrivalTime, temp(0).BurstTime)
                    '        End If
                    '    Next
                    'Next

#Region "hatdog"
                    'Dim temp As New List(Of Process)()

                    'For Each process In data
                    '    MsgBox("arrivalTime: " & process.ArrivalTime.ToString & "totalSum: " & totalSum.ToString)
                    '    If process.ArrivalTime <= totalSum Then
                    '        temp.Add(process)
                    '    End If
                    'Next

                    'If temp.Count = 0 Then
                    '    totalSum += 1
                    '    Continue While
                    'Else
                    '    totalSum = dataGridView.Rows.Cast(Of DataGridViewRow)().Sum(Function(row1) Convert.ToInt32(row1.Cells("burstTime").Value))
                    'End If

                    'For i As Integer = 0 To dataGridView.Rows.Count - 1
                    '    MsgBox(temp(i).ProcessID.ToString & dataGridView.Rows(i).Cells("processID").Value.ToString)
                    '    If temp(i).ProcessID = dataGridView.Rows(i).Cells("processID").Value Then
                    '        MsgBox(temp(i).ProcessID)
                    '        temp.RemoveAt(i)
                    '    End If
                    'Next

                    'If temp.Count <> 0 Then
                    '    temp = temp.OrderBy(Function(p) p.BurstTime).ThenBy(Function(p) p.ArrivalTime).ThenBy(Function(p) p.ProcessID).ToList()

                    '    dataGridView.Rows.Add(temp(0).ProcessID, temp(0).ArrivalTime, temp(0).BurstTime)
                    'End If
#End Region


                End If


            End While

        End If

    End Sub
#End Region
End Class
