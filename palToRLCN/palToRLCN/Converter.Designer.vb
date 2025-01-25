<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Converter
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Converter))
        Me.ConvertButton = New System.Windows.Forms.Button()
        Me.RGBTextBox = New System.Windows.Forms.TextBox()
        Me.BGRTextBox = New System.Windows.Forms.TextBox()
        Me.ConvertRGBButton = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ImportPALButton = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.PALBrowserDialog = New System.Windows.Forms.OpenFileDialog()
        Me.ExportRLCNButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ExportDialog = New System.Windows.Forms.SaveFileDialog()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.fourBppRadio = New System.Windows.Forms.RadioButton()
        Me.eightBppRadio = New System.Windows.Forms.RadioButton()
        Me.SelectedColorPanel = New System.Windows.Forms.Panel()
        Me.Convert256PalButton = New System.Windows.Forms.Button()
        Me.Convert256RLCNButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ConvertButton
        '
        Me.ConvertButton.Location = New System.Drawing.Point(581, 217)
        Me.ConvertButton.Name = "ConvertButton"
        Me.ConvertButton.Size = New System.Drawing.Size(150, 46)
        Me.ConvertButton.TabIndex = 0
        Me.ConvertButton.Text = "RGB to BGR"
        Me.ConvertButton.UseVisualStyleBackColor = True
        '
        'RGBTextBox
        '
        Me.RGBTextBox.Location = New System.Drawing.Point(357, 221)
        Me.RGBTextBox.MaxLength = 6
        Me.RGBTextBox.Name = "RGBTextBox"
        Me.RGBTextBox.Size = New System.Drawing.Size(200, 39)
        Me.RGBTextBox.TabIndex = 1
        '
        'BGRTextBox
        '
        Me.BGRTextBox.Location = New System.Drawing.Point(357, 276)
        Me.BGRTextBox.MaxLength = 4
        Me.BGRTextBox.Name = "BGRTextBox"
        Me.BGRTextBox.Size = New System.Drawing.Size(200, 39)
        Me.BGRTextBox.TabIndex = 2
        '
        'ConvertRGBButton
        '
        Me.ConvertRGBButton.Location = New System.Drawing.Point(581, 276)
        Me.ConvertRGBButton.Name = "ConvertRGBButton"
        Me.ConvertRGBButton.Size = New System.Drawing.Size(150, 46)
        Me.ConvertRGBButton.TabIndex = 3
        Me.ConvertRGBButton.Text = "BGR to RGB"
        Me.ConvertRGBButton.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel1.Location = New System.Drawing.Point(35, 96)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(40, 40)
        Me.Panel1.TabIndex = 4
        '
        'ImportPALButton
        '
        Me.ImportPALButton.Location = New System.Drawing.Point(873, 15)
        Me.ImportPALButton.Name = "ImportPALButton"
        Me.ImportPALButton.Size = New System.Drawing.Size(160, 52)
        Me.ImportPALButton.TabIndex = 5
        Me.ImportPALButton.Text = "Import pal"
        Me.ImportPALButton.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel2.Location = New System.Drawing.Point(105, 96)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(40, 40)
        Me.Panel2.TabIndex = 5
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel3.Location = New System.Drawing.Point(175, 96)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(40, 40)
        Me.Panel3.TabIndex = 5
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel4.Location = New System.Drawing.Point(249, 96)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(40, 40)
        Me.Panel4.TabIndex = 5
        '
        'Panel5
        '
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel5.Location = New System.Drawing.Point(327, 96)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(40, 40)
        Me.Panel5.TabIndex = 5
        '
        'Panel6
        '
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel6.Location = New System.Drawing.Point(399, 96)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(40, 40)
        Me.Panel6.TabIndex = 5
        '
        'Panel7
        '
        Me.Panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel7.Location = New System.Drawing.Point(473, 96)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(40, 40)
        Me.Panel7.TabIndex = 5
        '
        'Panel8
        '
        Me.Panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel8.Location = New System.Drawing.Point(547, 96)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(40, 40)
        Me.Panel8.TabIndex = 5
        '
        'Panel9
        '
        Me.Panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel9.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel9.Location = New System.Drawing.Point(616, 96)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(40, 40)
        Me.Panel9.TabIndex = 5
        '
        'Panel10
        '
        Me.Panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel10.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel10.Location = New System.Drawing.Point(689, 96)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(40, 40)
        Me.Panel10.TabIndex = 5
        '
        'Panel11
        '
        Me.Panel11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel11.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel11.Location = New System.Drawing.Point(763, 96)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(40, 40)
        Me.Panel11.TabIndex = 6
        '
        'Panel12
        '
        Me.Panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel12.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel12.Location = New System.Drawing.Point(839, 96)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(40, 40)
        Me.Panel12.TabIndex = 5
        '
        'Panel13
        '
        Me.Panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel13.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel13.Location = New System.Drawing.Point(919, 96)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(40, 40)
        Me.Panel13.TabIndex = 5
        '
        'Panel14
        '
        Me.Panel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel14.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel14.Location = New System.Drawing.Point(993, 96)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(40, 40)
        Me.Panel14.TabIndex = 5
        '
        'Panel15
        '
        Me.Panel15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel15.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel15.Location = New System.Drawing.Point(1065, 96)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(40, 40)
        Me.Panel15.TabIndex = 5
        '
        'Panel16
        '
        Me.Panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel16.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Panel16.Location = New System.Drawing.Point(1137, 96)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(40, 40)
        Me.Panel16.TabIndex = 6
        '
        'PALBrowserDialog
        '
        Me.PALBrowserDialog.FileName = "OpenFileDialog1"
        '
        'ExportRLCNButton
        '
        Me.ExportRLCNButton.Location = New System.Drawing.Point(1044, 15)
        Me.ExportRLCNButton.Name = "ExportRLCNButton"
        Me.ExportRLCNButton.Size = New System.Drawing.Size(160, 52)
        Me.ExportRLCNButton.TabIndex = 7
        Me.ExportRLCNButton.Text = "Export RLCN"
        Me.ExportRLCNButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(398, 45)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "16-colors .PAL converter:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(329, 224)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 32)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "#"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 10.125!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Label3.Location = New System.Drawing.Point(25, 160)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(370, 37)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "RGB to BGR-555 Converter:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(185, 221)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(133, 32)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "RGB (html):"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(206, 279)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 32)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "BGR-555:"
        '
        'ExportDialog
        '
        Me.ExportDialog.DefaultExt = "RLCN"
        Me.ExportDialog.Filter = "NitroColor File (*.RLCN, *.NCLR)|*.RLCN;*.NCLR"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(525, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 32)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Mode:"
        '
        'fourBppRadio
        '
        Me.fourBppRadio.AutoSize = True
        Me.fourBppRadio.Checked = True
        Me.fourBppRadio.Location = New System.Drawing.Point(631, 24)
        Me.fourBppRadio.Name = "fourBppRadio"
        Me.fourBppRadio.Size = New System.Drawing.Size(100, 36)
        Me.fourBppRadio.TabIndex = 14
        Me.fourBppRadio.TabStop = True
        Me.fourBppRadio.Text = "4bpp"
        Me.fourBppRadio.UseVisualStyleBackColor = True
        '
        'eightBppRadio
        '
        Me.eightBppRadio.AutoSize = True
        Me.eightBppRadio.Location = New System.Drawing.Point(752, 24)
        Me.eightBppRadio.Name = "eightBppRadio"
        Me.eightBppRadio.Size = New System.Drawing.Size(100, 36)
        Me.eightBppRadio.TabIndex = 15
        Me.eightBppRadio.Text = "8bpp"
        Me.eightBppRadio.UseVisualStyleBackColor = True
        '
        'SelectedColorPanel
        '
        Me.SelectedColorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SelectedColorPanel.Location = New System.Drawing.Point(35, 217)
        Me.SelectedColorPanel.Name = "SelectedColorPanel"
        Me.SelectedColorPanel.Size = New System.Drawing.Size(105, 105)
        Me.SelectedColorPanel.TabIndex = 16
        '
        'Convert256PalButton
        '
        Me.Convert256PalButton.Enabled = False
        Me.Convert256PalButton.Location = New System.Drawing.Point(906, 186)
        Me.Convert256PalButton.Name = "Convert256PalButton"
        Me.Convert256PalButton.Size = New System.Drawing.Size(285, 65)
        Me.Convert256PalButton.TabIndex = 17
        Me.Convert256PalButton.Text = "Convert 256 colors .pal"
        Me.Convert256PalButton.UseVisualStyleBackColor = True
        '
        'Convert256RLCNButton
        '
        Me.Convert256RLCNButton.Enabled = False
        Me.Convert256RLCNButton.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Convert256RLCNButton.Location = New System.Drawing.Point(906, 257)
        Me.Convert256RLCNButton.Name = "Convert256RLCNButton"
        Me.Convert256RLCNButton.Size = New System.Drawing.Size(285, 65)
        Me.Convert256RLCNButton.TabIndex = 18
        Me.Convert256RLCNButton.Text = "Convert 256 colors RLCN"
        Me.Convert256RLCNButton.UseVisualStyleBackColor = True
        '
        'Converter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 32.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1216, 346)
        Me.Controls.Add(Me.Convert256RLCNButton)
        Me.Controls.Add(Me.Convert256PalButton)
        Me.Controls.Add(Me.SelectedColorPanel)
        Me.Controls.Add(Me.eightBppRadio)
        Me.Controls.Add(Me.fourBppRadio)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ExportRLCNButton)
        Me.Controls.Add(Me.Panel16)
        Me.Controls.Add(Me.Panel15)
        Me.Controls.Add(Me.Panel14)
        Me.Controls.Add(Me.Panel13)
        Me.Controls.Add(Me.Panel12)
        Me.Controls.Add(Me.Panel11)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.ImportPALButton)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ConvertRGBButton)
        Me.Controls.Add(Me.BGRTextBox)
        Me.Controls.Add(Me.RGBTextBox)
        Me.Controls.Add(Me.ConvertButton)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Converter"
        Me.Text = "palToRLCN (converter)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ConvertButton As Button
    Friend WithEvents RGBTextBox As TextBox
    Friend WithEvents BGRTextBox As TextBox
    Friend WithEvents ConvertRGBButton As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ImportPALButton As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Panel9 As Panel
    Friend WithEvents Panel10 As Panel
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Panel12 As Panel
    Friend WithEvents Panel13 As Panel
    Friend WithEvents Panel14 As Panel
    Friend WithEvents Panel15 As Panel
    Friend WithEvents Panel16 As Panel
    Friend WithEvents PALBrowserDialog As OpenFileDialog
    Friend WithEvents ExportRLCNButton As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents ExportDialog As SaveFileDialog
    Friend WithEvents Label6 As Label
    Friend WithEvents fourBppRadio As RadioButton
    Friend WithEvents eightBppRadio As RadioButton
    Friend WithEvents SelectedColorPanel As Panel
    Friend WithEvents Convert256PalButton As Button
    Friend WithEvents Convert256RLCNButton As Button
End Class
