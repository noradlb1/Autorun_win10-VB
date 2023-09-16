Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms

Namespace USBAutoRun
    Public Partial Class MainForm
        Inherits Form
        Public Sub New()
            InitializeComponent()
        End Sub

        Private AlreadyReadDrives As Dictionary(Of String, String) = New Dictionary(Of String, String)()
        Private NotReadyList As List(Of String) = New List(Of String)()
        'bool FirstRun = true;

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            If Not backgroundWorker1.IsBusy Then backgroundWorker1.RunWorkerAsync()
        End Sub

        Private Sub backgroundWorker1_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
            Debug.WriteLine("bg worker started")

            While True
                Threading.Thread.Sleep(500) ' maybe make this a customizable setting

                Dim RemovableDrives = DriveInfo.GetDrives().Where(Function(drive) drive.DriveType = DriveType.Removable)
                Dim NonRemovableDrives = DriveInfo.GetDrives().Where(Function(drive) drive.DriveType <> DriveType.Removable)
                Dim DrivesByName As List(Of String) = New List(Of String)()

                For Each d In RemovableDrives
                    If Not DriveReady(d) Then Continue For

                    RemovableDrivesList.Invoke(New Action(Sub()
                                                              Dim lvi = RemovableDrivesList.FindItemWithText(d.Name)
                                                              If lvi Is Nothing Then
                                                                  Dim FindINF = False
                                                                  If d.RootDirectory.GetFiles("Autorun.inf").Count() > 0 Then FindINF = True

                                                                  Dim RowData As String() = {d.VolumeLabel, d.DriveType.ToString(), d.DriveFormat, FindINF.ToString()}
                                                                  RemovableDrivesList.Items.Add(d.Name).SubItems.AddRange(RowData)
                                                              End If
                                                              DrivesByName.Add(d.Name) ' used to detect if drive was removed
                                                          End Sub))

                    If Not AlreadyReadDrives.ContainsKey(d.Name) Then
                        ReadAutoInfo(d.RootDirectory)
                        AlreadyReadDrives.Add(d.Name, d.VolumeLabel)
                    End If
                Next

                ' remove drives from list that have been removed physically
                RemovableDrivesList.Invoke(New Action(Sub()
                                                          Dim Tmplvi As List(Of ListViewItem) = New List(Of ListViewItem)()
                                                          For Each lvi As ListViewItem In RemovableDrivesList.Items
                                                              Tmplvi.Add(lvi)
                                                          Next
                                                          For Each lvi In Tmplvi
                                                              If Not DrivesByName.Contains(lvi.Text) Then ' if our listview item is no longer connected, remove it
                                                                  AlreadyReadDrives.Remove(lvi.Text)
                                                                  RemovableDrivesList.FindItemWithText(lvi.Text).Remove()
                                                              End If
                                                          Next
                                                      End Sub))

                For Each d In NonRemovableDrives
                    If Not DriveReady(d) Then Continue For

                    Try
                        Dim FindINF = False
                        If d.RootDirectory.GetFiles("Autorun.inf").Count() > 0 Then FindINF = True

                        Dim RowData As String() = {d.VolumeLabel, d.DriveType.ToString(), d.DriveFormat, FindINF.ToString()}

                        NonRemovableDrivesList.Invoke(New Action(Sub()
                                                                     If NonRemovableDrivesList.FindItemWithText(d.Name) Is Nothing Then NonRemovableDrivesList.Items.Add(d.Name).SubItems.AddRange(RowData)
                                                                 End Sub))
                    Catch err As Exception
                        'MessageBox.Show(err.ToString());
                        LogBox.Invoke(New Action(Sub() LogBox.Text += "Drive " & d.Name & err.ToString() & Environment.NewLine))
                    End Try

                    If Properties.Settings.Default IsNot Nothing Then
                        If Properties.Settings.Default.NonRemovableToo Then
                            If Not AlreadyReadDrives.ContainsKey(d.Name) Then
                                ReadAutoInfo(d.RootDirectory)
                                AlreadyReadDrives.Add(d.Name, d.VolumeLabel)
                            End If
                        End If
                    End If
                Next
                'FirstRun = false;
            End While
        End Sub

        Private Function DriveReady(ByVal d As DriveInfo) As Boolean
            If Not d.IsReady Then
                If Not NotReadyList.Contains(d.Name) Then
                    LogBox.Invoke(New Action(Sub() LogBox.Text += "Drive " & d.Name & " is not ready" & Environment.NewLine))
                    NotReadyList.Add(d.Name)
                    AlreadyReadDrives.Remove(d.Name)

                    RemovableDrivesList.Invoke(New Action(Sub()
                                                              Dim lvi = RemovableDrivesList.FindItemWithText(d.Name)
                                                              If lvi IsNot Nothing Then lvi.Remove()
                                                          End Sub))
                    NonRemovableDrivesList.Invoke(New Action(Sub()
                                                                 Dim lvi = NonRemovableDrivesList.FindItemWithText(d.Name)
                                                                 If lvi IsNot Nothing Then lvi.Remove()
                                                             End Sub))
                End If
                Return False
            Else
                If NotReadyList.Contains(d.Name) Then NotReadyList.Remove(d.Name)
            End If
            Return True
        End Function

        Private Sub ReadAutoInfo(ByVal di As DirectoryInfo)
            ' read and execute Autorun.info files
            For Each file In di.GetFiles("Autorun.inf")
                Debug.WriteLine("Found Autorun.info at:" & file.FullName)
                Dim lines = IO.File.ReadAllLines(file.FullName)
                For Each line In lines
                    If line.ToLower().Contains("open=") AndAlso Not line.ToLower().Contains(";open=") Then
                        Dim exe As String = di.ToString() & "\" & line.Split("="c)(1)
                        Debug.WriteLine("Starting EXE from open:" & exe)
                        Try
                            Process.Start(exe)
                        Catch
                            Return ' drive wasnt ready, prevent crash
                        End Try
                    ElseIf line.ToLower().Contains("shellexecute=") AndAlso Not line.ToLower().Contains(";shellexecute=") Then
                        Dim exe As String = di.ToString() & "\" & line.Split("="c)(1)
                        Debug.WriteLine("Starting EXE from shellexecute:" & exe)
                        Try
                            Process.Start(exe)
                        Catch
                            Return ' drive wasnt ready, prevent crash
                        End Try
                    End If
                Next
            Next
        End Sub

        Private Sub MainForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)
            backgroundWorker1.CancelAsync()
        End Sub

        Private Sub MainForm_Resize(ByVal sender As Object, ByVal e As EventArgs)
            If WindowState = FormWindowState.Minimized Then
                Hide()
                notifyIcon1.Visible = True
            End If
        End Sub

        Private Sub notifyIcon1_DoubleClick(ByVal sender As Object, ByVal e As EventArgs)
            Show()
            notifyIcon1.Visible = False
            WindowState = FormWindowState.Normal
        End Sub

        Private Sub MainForm_Shown(ByVal sender As Object, ByVal e As EventArgs)
            If Properties.Settings.Default IsNot Nothing Then nonremovabletoo.Checked = Properties.Settings.Default.NonRemovableToo
        End Sub

        Private Sub nonremovabletoo_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
            If Properties.Settings.Default IsNot Nothing Then
                Properties.Settings.Default.NonRemovableToo = nonremovabletoo.Checked
                Properties.Settings.Default.Save()
            End If
        End Sub

        Private Sub ReDetectDrives_Click(ByVal sender As Object, ByVal e As EventArgs)
            LogBox.Text += "Re-detecting drives." & Environment.NewLine
            RemovableDrivesList.Items.Clear()
            NonRemovableDrivesList.Items.Clear()
            NotReadyList.Clear()
            AlreadyReadDrives.Clear()
        End Sub
    End Class
End Namespace
