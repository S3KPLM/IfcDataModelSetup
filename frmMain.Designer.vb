<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmdIfcSelection = New System.Windows.Forms.Button()
        Me.txtIfcFullPath = New System.Windows.Forms.TextBox()
        Me.cmdAnalyze = New System.Windows.Forms.Button()
        Me.OpenIfcDialog = New System.Windows.Forms.OpenFileDialog()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.txtOutput = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'cmdIfcSelection
        '
        Me.cmdIfcSelection.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdIfcSelection.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.cmdIfcSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdIfcSelection.Location = New System.Drawing.Point(688, 2)
        Me.cmdIfcSelection.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdIfcSelection.Name = "cmdIfcSelection"
        Me.cmdIfcSelection.Size = New System.Drawing.Size(85, 27)
        Me.cmdIfcSelection.TabIndex = 11
        Me.cmdIfcSelection.Text = "Browse..."
        Me.cmdIfcSelection.UseVisualStyleBackColor = False
        '
        'txtIfcFullPath
        '
        Me.txtIfcFullPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtIfcFullPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIfcFullPath.Location = New System.Drawing.Point(4, 4)
        Me.txtIfcFullPath.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtIfcFullPath.Name = "txtIfcFullPath"
        Me.txtIfcFullPath.Size = New System.Drawing.Size(681, 23)
        Me.txtIfcFullPath.TabIndex = 10
        '
        'cmdAnalyze
        '
        Me.cmdAnalyze.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAnalyze.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.cmdAnalyze.Enabled = False
        Me.cmdAnalyze.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmdAnalyze.Location = New System.Drawing.Point(688, 505)
        Me.cmdAnalyze.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmdAnalyze.Name = "cmdAnalyze"
        Me.cmdAnalyze.Size = New System.Drawing.Size(85, 27)
        Me.cmdAnalyze.TabIndex = 13
        Me.cmdAnalyze.Text = "Analyze"
        Me.cmdAnalyze.UseVisualStyleBackColor = False
        '
        'OpenIfcDialog
        '
        Me.OpenIfcDialog.Filter = "IFC Files|*.ifc"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.ProgressBar1.Location = New System.Drawing.Point(4, 506)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(681, 25)
        Me.ProgressBar1.TabIndex = 15
        '
        'txtOutput
        '
        Me.txtOutput.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOutput.Location = New System.Drawing.Point(4, 31)
        Me.txtOutput.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtOutput.Multiline = True
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.Size = New System.Drawing.Size(769, 470)
        Me.txtOutput.TabIndex = 16
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ClientSize = New System.Drawing.Size(777, 536)
        Me.Controls.Add(Me.txtOutput)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.cmdAnalyze)
        Me.Controls.Add(Me.cmdIfcSelection)
        Me.Controls.Add(Me.txtIfcFullPath)
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IFC Preprocessor 1.0.0.0"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdIfcSelection As Button
    Friend WithEvents txtIfcFullPath As TextBox
    Friend WithEvents cmdAnalyze As Button
    Friend WithEvents OpenIfcDialog As OpenFileDialog
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents txtOutput As TextBox
End Class
