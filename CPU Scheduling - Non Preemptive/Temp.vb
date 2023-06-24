'Public Class Temp

'#Region "FCFS/SJF Buttons"
'    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click






'    End Sub
'    Private Sub btnFinish_Click(sender As Object, e As EventArgs) Handles btnFinish.Click

'    End Sub

'#End Region


'#Region "Function Helpers"


'    Private Function compute(datagridInitial As DataGridView, datagridDestination As DataGridView, cpuSchedType As String, waitTime As Integer) As Integer

'        Dim table As New DataTable()
'        Dim bs As New BindingSource
'        table.Columns.Add("Process ID", GetType(String))
'        table.Columns.Add("Arrival Time", GetType(Integer))
'        table.Columns.Add("Burst Time", GetType(Integer))

'        For i As Integer = 0 To datagridInitial.Rows.Count - 1
'            table.Rows.Add(datagridInitial.Rows(i).Cells(0).Value, datagridInitial.Rows(i).Cells(1).Value, datagridInitial.Rows(i).Cells(2).Value)
'        Next

'        If cpuSchedType = "FCFS" Then
'            'datagridInitial.Sort(datagridInitial.Columns(0), ListSortDirection.Descending)
'            'datagridInitial.Sort(datagridInitial.Columns(1), ListSortDirection.Ascending)


'            table.DefaultView.Sort = "Arrival Time, Process ID"
'            datagridInitial.Columns.Clear()
'            datagridInitial.DataSource = table

'        ElseIf cpuSchedType = "SJF" Then
'            'datagridInitial.Sort(datagridInitial.Columns(2), ListSortDirection.Ascending)
'            'datagridInitial.Sort(datagridInitial.Columns(1), ListSortDirection.Ascending)


'            bs.DataSource = table
'            bs.Sort = "Burst Time ASC, Arrival Time ASC"

'            'table.DefaultView.Sort = "Burst Time, Arrival Time"
'            datagridInitial.Columns.Clear()
'            datagridInitial.DataSource = bs
'        ElseIf cpuSchedType = "PRIO" Then

'        End If

'        datagridInitial.ClearSelection()
'        datagridDestination.Rows.Clear()

'        Dim loopCount As Integer
'        Dim currentTime As Integer = 0
'        Dim totalWaitingTime As Integer = 0
'        Dim totalTurnaroundTime As Integer = 0
'        Dim completionTime As Integer = 0
'        Dim turnAroundTime As Integer = 0
'        For i As Integer = 0 To datagridInitial.Rows.Count - 1
'            If currentTime < datagridInitial.Rows(i).Cells(1).Value Then
'                currentTime = datagridInitial.Rows(i).Cells(1).Value
'            End If

'            Dim waitingTime As Integer = currentTime - datagridInitial.Rows(i).Cells(1).Value
'            totalWaitingTime += waitingTime

'            currentTime += datagridInitial.Rows(i).Cells(2).Value

'            completionTime = currentTime

'            'MsgBox("Process " & (i + 1) & " - Waiting Time: " & waitingTime & " - Completion Time " & completionTime)

'            turnAroundTime = completionTime - datagridInitial.Rows(i).Cells(1).Value
'            totalTurnaroundTime += turnAroundTime

'            datagridDestination.Rows.Add(datagridInitial.Rows(i).Cells(0).Value, "", "", "")
'            wait(waitTime)
'            datagridDestination.Rows(i).Cells(1).Value = completionTime
'            wait(waitTime)
'            datagridDestination.Rows(i).Cells(2).Value = turnAroundTime
'            wait(waitTime)
'            datagridDestination.Rows(i).Cells(3).Value = waitingTime
'            wait(waitTime)

'            loopCount = i
'        Next


'        datagridDestination.ClearSelection()
'        'datagridInitial.Sort(datagridInitial.Columns(0), ListSortDirection.Ascending)
'        'datagridDestination.Sort(datagridDestination.Columns(0), ListSortDirection.Ascending)


'        labelAveWait.Text = totalWaitingTime / datagridInitial.Rows.Count
'        labelAveTurn.Text = totalTurnaroundTime / datagridInitial.Rows.Count
'        Return loopCount
'    End Function




'#End Region
'End Class





'Dim table As New DataTable()
'Dim bs As New BindingSource
'table.Columns.Add("Process ID", GetType(String))
'table.Columns.Add("Arrival Time", GetType(Integer))
'table.Columns.Add("Burst Time", GetType(Integer))

'For i As Integer = 0 To datagridInitial.Rows.Count - 1
'table.Rows.Add(datagridInitial.Rows(i).Cells(0).Value, datagridInitial.Rows(i).Cells(1).Value, datagridInitial.Rows(i).Cells(2).Value)
'Next

'datagridInitial.Sort(datagridInitial.Columns(0), ListSortDirection.Descending)
'datagridInitial.Sort(datagridInitial.Columns(1), ListSortDirection.Ascending)
'table.DefaultView.Sort = "Arrival Time, Process ID"
'datagridInitial.Columns.Clear()
'datagridInitial.DataSource = table

'datagridInitial.ClearSelection()
'datagridComputation.Rows.Clear()

'Dim loopCount As Integer
'Dim currentTime As Integer = 0
'Dim totalWaitingTime As Integer = 0
'Dim totalTurnaroundTime As Integer = 0
'Dim completionTime As Integer = 0
'Dim turnAroundTime As Integer = 0
'For i As Integer = 0 To datagridInitial.Rows.Count - 1
'If currentTime < datagridInitial.Rows(i).Cells(1).Value Then
'currentTime = datagridInitial.Rows(i).Cells(1).Value
'End If

'Dim waitingTime As Integer = currentTime - datagridInitial.Rows(i).Cells(1).Value
'totalWaitingTime += waitingTime

'currentTime += datagridInitial.Rows(i).Cells(2).Value

'completionTime = currentTime

'MsgBox("Process " & (i + 1) & " - Waiting Time: " & waitingTime & " - Completion Time " & completionTime)

'turnAroundTime = completionTime - datagridInitial.Rows(i).Cells(1).Value
'totalTurnaroundTime += turnAroundTime

'datagridComputation.Rows.Add(datagridInitial.Rows(i).Cells(0).Value, "", "", "")
'wait(waitTime)
'datagridComputation.Rows(i).Cells(1).Value = completionTime
'wait(waitTime)
'datagridComputation.Rows(i).Cells(2).Value = turnAroundTime
'wait(waitTime)
'datagridComputation.Rows(i).Cells(3).Value = waitingTime
'wait(waitTime)

'loopCount = i
'Next


'datagridComputation.ClearSelection()
'datagridInitial.Sort(datagridInitial.Columns(0), ListSortDirection.Ascending)
'datagridComputation.Sort(datagridComputation.Columns(0), ListSortDirection.Ascending)


'labelAveWait.Text = totalWaitingTime / datagridInitial.Rows.Count
'labelAveTurn.Text = totalTurnaroundTime / datagridInitial.Rows.Count
