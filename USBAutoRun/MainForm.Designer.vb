Imports System

Namespace USBAutoRun
    Partial Class MainForm
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <paramname="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            components = New ComponentModel.Container()
            Dim resources As ComponentModel.ComponentResourceManager = New ComponentModel.ComponentResourceManager(GetType(MainForm))
            backgroundWorker1 = New ComponentModel.BackgroundWorker()
            notifyIcon1 = New Windows.Forms.NotifyIcon(components)
            RemovableDrivesList = New Windows.Forms.ListView()
            DriveName = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            Label = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            Type = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            Format = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            Inf = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            NonRemovableDrivesList = New Windows.Forms.ListView()
            columnHeader1 = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            columnHeader2 = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            columnHeader3 = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            columnHeader5 = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            columnHeader4 = CType((New Windows.Forms.ColumnHeader()), Windows.Forms.ColumnHeader)
            groupBox1 = New Windows.Forms.GroupBox()
            groupBox2 = New Windows.Forms.GroupBox()
            nonremovabletoo = New Windows.Forms.CheckBox()
            LogBox = New Windows.Forms.TextBox()
            ReDetectDrives = New Windows.Forms.Button()
            groupBox1.SuspendLayout()
            groupBox2.SuspendLayout()
            SuspendLayout()
            ' 
            ' backgroundWorker1
            ' 
            backgroundWorker1.WorkerReportsProgress = True
            backgroundWorker1.WorkerSupportsCancellation = True
            AddHandler backgroundWorker1.DoWork, New ComponentModel.DoWorkEventHandler(AddressOf backgroundWorker1_DoWork)
            ' 
            ' notifyIcon1
            ' 
            notifyIcon1.Icon = CType(resources.GetObject("notifyIcon1.Icon"), Drawing.Icon)
            notifyIcon1.Text = "Autorun_Win10"
            notifyIcon1.Visible = True
            AddHandler notifyIcon1.DoubleClick, New EventHandler(AddressOf notifyIcon1_DoubleClick)
            ' 
            ' RemovableDrivesList
            ' 
            RemovableDrivesList.Columns.AddRange(New Windows.Forms.ColumnHeader() {DriveName, Label, Type, Format, Inf})
            RemovableDrivesList.HideSelection = False
            RemovableDrivesList.Location = New Drawing.Point(8, 19)
            RemovableDrivesList.Name = "RemovableDrivesList"
            RemovableDrivesList.Size = New Drawing.Size(382, 113)
            RemovableDrivesList.TabIndex = 0
            RemovableDrivesList.UseCompatibleStateImageBehavior = False
            RemovableDrivesList.View = Windows.Forms.View.Details
            ' 
            ' DriveName
            ' 
            DriveName.Text = "Name"
            ' 
            ' Label
            ' 
            Label.Text = "Label"
            Label.Width = 131
            ' 
            ' Type
            ' 
            Type.Text = "Type"
            ' 
            ' Format
            ' 
            Format.Text = "Format"
            ' 
            ' Inf
            ' 
            Inf.Text = "Inf"
            ' 
            ' NonRemovableDrivesList
            ' 
            NonRemovableDrivesList.Columns.AddRange(New Windows.Forms.ColumnHeader() {columnHeader1, columnHeader2, columnHeader3, columnHeader5, columnHeader4})
            NonRemovableDrivesList.HideSelection = False
            NonRemovableDrivesList.Location = New Drawing.Point(6, 19)
            NonRemovableDrivesList.Name = "NonRemovableDrivesList"
            NonRemovableDrivesList.Size = New Drawing.Size(384, 113)
            NonRemovableDrivesList.TabIndex = 1
            NonRemovableDrivesList.UseCompatibleStateImageBehavior = False
            NonRemovableDrivesList.View = Windows.Forms.View.Details
            ' 
            ' columnHeader1
            ' 
            columnHeader1.Text = "Name"
            ' 
            ' columnHeader2
            ' 
            columnHeader2.Text = "Label"
            columnHeader2.Width = 137
            ' 
            ' columnHeader3
            ' 
            columnHeader3.Text = "Type"
            ' 
            ' columnHeader5
            ' 
            columnHeader5.Text = "Format"
            ' 
            ' columnHeader4
            ' 
            columnHeader4.Text = "Inf"
            ' 
            ' groupBox1
            ' 
            groupBox1.Controls.Add(RemovableDrivesList)
            groupBox1.Location = New Drawing.Point(12, 36)
            groupBox1.Name = "groupBox1"
            groupBox1.Size = New Drawing.Size(398, 140)
            groupBox1.TabIndex = 2
            groupBox1.TabStop = False
            groupBox1.Text = "Removable Drives"
            ' 
            ' groupBox2
            ' 
            groupBox2.Controls.Add(NonRemovableDrivesList)
            groupBox2.Location = New Drawing.Point(12, 182)
            groupBox2.Name = "groupBox2"
            groupBox2.Size = New Drawing.Size(398, 141)
            groupBox2.TabIndex = 3
            groupBox2.TabStop = False
            groupBox2.Text = "Non-Removable Drives"
            ' 
            ' nonremovabletoo
            ' 
            nonremovabletoo.AutoSize = True
            nonremovabletoo.Location = New Drawing.Point(20, 13)
            nonremovabletoo.Name = "nonremovabletoo"
            nonremovabletoo.Size = New Drawing.Size(205, 17)
            nonremovabletoo.TabIndex = 4
            nonremovabletoo.Text = "Detect In Non-Removable Drives Too"
            nonremovabletoo.UseVisualStyleBackColor = True
            AddHandler nonremovabletoo.CheckedChanged, New EventHandler(AddressOf nonremovabletoo_CheckedChanged)
            ' 
            ' LogBox
            ' 
            LogBox.Location = New Drawing.Point(13, 329)
            LogBox.Multiline = True
            LogBox.Name = "LogBox"
            LogBox.ReadOnly = True
            LogBox.ScrollBars = Windows.Forms.ScrollBars.Vertical
            LogBox.Size = New Drawing.Size(397, 118)
            LogBox.TabIndex = 5
            ' 
            ' ReDetectDrives
            ' 
            ReDetectDrives.Location = New Drawing.Point(310, 9)
            ReDetectDrives.Name = "ReDetectDrives"
            ReDetectDrives.Size = New Drawing.Size(100, 23)
            ReDetectDrives.TabIndex = 6
            ReDetectDrives.Text = "Re-Detect Drives"
            ReDetectDrives.UseVisualStyleBackColor = True
            AddHandler ReDetectDrives.Click, New EventHandler(AddressOf ReDetectDrives_Click)
            ' 
            ' MainForm
            ' 
            AutoScaleDimensions = New Drawing.SizeF(6.0F, 13.0F)
            AutoScaleMode = Windows.Forms.AutoScaleMode.Font
            ClientSize = New Drawing.Size(422, 459)
            Controls.Add(ReDetectDrives)
            Controls.Add(LogBox)
            Controls.Add(nonremovabletoo)
            Controls.Add(groupBox2)
            Controls.Add(groupBox1)
            FormBorderStyle = Windows.Forms.FormBorderStyle.Fixed3D
            MaximizeBox = False
            Name = "MainForm"
            ShowIcon = False
            SizeGripStyle = Windows.Forms.SizeGripStyle.Hide
            Text = "Autorun"
            AddHandler FormClosing, New Windows.Forms.FormClosingEventHandler(AddressOf MainForm_FormClosing)
            AddHandler Load, New EventHandler(AddressOf Form1_Load)
            AddHandler Shown, New EventHandler(AddressOf MainForm_Shown)
            AddHandler Resize, New EventHandler(AddressOf MainForm_Resize)
            groupBox1.ResumeLayout(False)
            groupBox2.ResumeLayout(False)
            ResumeLayout(False)
            PerformLayout()

        End Sub

#End Region

        Private backgroundWorker1 As ComponentModel.BackgroundWorker
        Private notifyIcon1 As Windows.Forms.NotifyIcon
        Private RemovableDrivesList As Windows.Forms.ListView
        Private DriveName As Windows.Forms.ColumnHeader
        Private Label As Windows.Forms.ColumnHeader
        Private Type As Windows.Forms.ColumnHeader
        Private Format As Windows.Forms.ColumnHeader
        Private NonRemovableDrivesList As Windows.Forms.ListView
        Private columnHeader1 As Windows.Forms.ColumnHeader
        Private columnHeader2 As Windows.Forms.ColumnHeader
        Private columnHeader3 As Windows.Forms.ColumnHeader
        Private columnHeader5 As Windows.Forms.ColumnHeader
        Private groupBox1 As Windows.Forms.GroupBox
        Private groupBox2 As Windows.Forms.GroupBox
        Private nonremovabletoo As Windows.Forms.CheckBox
        Private LogBox As Windows.Forms.TextBox
        Private ReDetectDrives As Windows.Forms.Button
        Private Inf As Windows.Forms.ColumnHeader
        Private columnHeader4 As Windows.Forms.ColumnHeader
    End Class
End Namespace
