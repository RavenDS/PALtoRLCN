Imports System.Drawing
Imports System.IO
Imports System.Text.RegularExpressions

Public Class Converter
    Dim ColorArray As String() = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"}

    Private Sub Converter_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Panel1.Enabled = False
        Panel2.Enabled = False
        Panel3.Enabled = False
        Panel4.Enabled = False
        Panel5.Enabled = False
        Panel6.Enabled = False
        Panel7.Enabled = False
        Panel8.Enabled = False
        Panel9.Enabled = False
        Panel10.Enabled = False
        Panel11.Enabled = False
        Panel12.Enabled = False
        Panel13.Enabled = False
        Panel14.Enabled = False
        Panel15.Enabled = False
        Panel16.Enabled = False
        ExportRLCNButton.Enabled = False

    End Sub
    Public Function decimalToHexLittleEndian(ByVal _iValue As Long, ByVal _iBytes As Integer) As String

        Dim sBigEndian As String = String.Format("{0:x" & (2 * _iBytes).ToString() & "}", _iValue)
        Dim sLittleEndian = ""

        For v = _iBytes - 1 To 0 Step -1
            sLittleEndian += sBigEndian.Substring(v * 2, 2)
        Next

        Return sLittleEndian

    End Function
    Public Function HexToText(HexBytes As Byte(), filename As String)

        Dim Hex As String() = Array.ConvertAll(HexBytes, Function(b) b.ToString("X2"))
        Dim Values As String = (String.Join("", Hex))
        For index As Integer = 2 * (Values.Length \ 2) To 0 Step -2
            Values = Values.Insert(index, Environment.NewLine)
        Next
        Dim clean As String = Regex.Replace(Values, "^\s+$[\r\n]*", "", RegexOptions.Multiline)
        System.IO.File.WriteAllText(filename, clean)

    End Function
    Public Function RGBtoBGR(ByVal COLORSTRING As String)

        '' adapted from this post: https://stackoverflow.com/a/75000283

        Dim rgb = ColorTranslator.FromHtml("#" & COLORSTRING)

        ' Divide R, G and B values by 8 and shifts left by 0, 5 and 10 positions 
        ' (16 bit pixel, each color in 5 bits)
        Dim colorBGR555 = CShort((rgb.R \ 8) + ((rgb.G \ 8) << 5) + ((rgb.B \ 8) << 10))
        ' Converts to string in Hex Format (Big Endian)  
        Dim colorBGR555HexBE = colorBGR555.ToString("X2")

        ' Reverse the bytes (it's a Short value, two bytes) for its Little Endian representation
        Dim bytes = BitConverter.GetBytes(colorBGR555).Reverse().ToArray()
        Dim colorBGR555HexLE = BitConverter.ToInt16(bytes, 0).ToString("X2")
        Dim finalstring As String = colorBGR555HexLE
        If colorBGR555HexLE.Length = 2 Then
            finalstring = "00" & colorBGR555HexLE
        ElseIf colorBGR555HexLE.Length = 3 Then
            finalstring = "0" & colorBGR555HexLE
        End If
        Return finalstring

    End Function
    Public Function ReverseHex(ByVal COLORTEXT As String)

        Dim ColorShort As UShort = "&H" & COLORTEXT
        Dim bytes = BitConverter.GetBytes(ColorShort).Reverse().ToArray()
        Dim colorReverse = BitConverter.ToInt16(bytes, 0).ToString("X2")

        Return colorReverse

    End Function
    Public Function BGRtoHTML(ByVal COLOROBJECT As Object)

        Dim BigColor As Integer = "&H" & ReverseHex(COLOROBJECT)
        Dim bgrInt As Integer = Integer.Parse(BigColor, 16)
        bgrInt = Math.Min(bgrInt, Math.Pow(2, 15) - 1)

        Dim r As Long = (bgrInt And &H1F) * 8

        Dim g As Long = ((bgrInt >> 5) And &H1F) * 8

        Dim b As Long = ((bgrInt >> 10) And &H1F) * 8

        Dim rError As Long = Math.Floor(r / 32)
        Dim gError As Long = Math.Floor(g / 32)
        Dim bError As Long = Math.Floor(b / 32)

        Dim FinalColor As Color = Color.FromArgb((r + rError), (g + gError), (b + bError))
        Dim HTMLString As String = ColorTranslator.ToHtml(FinalColor).ToString
        HTMLString = HTMLString.Replace("#", "")
        Return HTMLString

    End Function
    Public Function ConvertJASCtoPAL(ByVal SelectedLine As String)

        Dim delimiter As Char() = {" "}

        Dim SplitColor As String() = SelectedLine.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)

        Dim ConvertedJASCColor As Color = Color.FromArgb(SplitColor(0), SplitColor(1), SplitColor(2))
        Dim HTMLConvertedString As String = ColorTranslator.ToHtml(ConvertedJASCColor).ToString
        HTMLConvertedString = HTMLConvertedString.Replace("#", "")
        Return HTMLConvertedString

    End Function


    Private Sub ConvertButton_Click(sender As Object, e As EventArgs) Handles ConvertButton.Click

        If RGBTextBox.Text.Length < 6 Then
            MsgBox("HTML code must be 6 characters long", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        ElseIf RGBTextBox.Text.Contains("#") Then
            MsgBox("Do not include # in the code", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If

        BGRTextBox.Text = RGBtoBGR(RGBTextBox.Text)
        SelectedColorPanel.BackColor = ColorTranslator.FromHtml("#" & BGRtoHTML(BGRTextBox.Text))

    End Sub

    Private Sub ConvertRGBButton_Click(sender As Object, e As EventArgs) Handles ConvertRGBButton.Click

        If BGRTextBox.Text.Length = 0 Then
            Exit Sub
        End If

        RGBTextBox.Text = BGRtoHTML(BGRTextBox.Text)
        SelectedColorPanel.BackColor = ColorTranslator.FromHtml("#" & BGRtoHTML(BGRTextBox.Text))

    End Sub

    Private Sub ImportPALButton_Click(sender As Object, e As EventArgs) Handles ImportPALButton.Click

        If PALBrowserDialog.ShowDialog = vbOK Then
            Panel1.Enabled = True
            Panel2.Enabled = True
            Panel3.Enabled = True
            Panel4.Enabled = True
            Panel5.Enabled = True
            Panel6.Enabled = True
            Panel7.Enabled = True
            Panel8.Enabled = True
            Panel9.Enabled = True
            Panel10.Enabled = True
            Panel11.Enabled = True
            Panel12.Enabled = True
            Panel13.Enabled = True
            Panel14.Enabled = True
            Panel15.Enabled = True
            Panel16.Enabled = True
            ExportRLCNButton.Enabled = True
            Dim PalTypeValue As String = "riff"

            Dim ReadPalBytes As Byte() = IO.File.ReadAllBytes(PALBrowserDialog.FileName.ToString)
            HexToText(ReadPalBytes, ".\tempPalette.txt")

            Dim PalLines As String()
            PalLines = System.IO.File.ReadAllLines(".\tempPalette.txt")

            If PalLines(0) = "4A" AndAlso PalLines(1) = "41" AndAlso PalLines(2) = "53" AndAlso PalLines(3) = "43" AndAlso PalLines(4) = "2D" AndAlso PalLines(5) = "50" AndAlso PalLines(6) = "41" AndAlso PalLines(7) = "4C" Then
                PalTypeValue = "jasc-pal"
                MsgBox("This .pal file appears to be in JASC-PAL Format  (Aseprite?)" & vbCrLf & vbCrLf & "The recommended format is Microsoft RIFF (.pal)", MsgBoxStyle.Information, "Palette format")

            ElseIf Path.GetExtension(PALBrowserDialog.FileName.ToString).ToLower() = ".act" Then
                PalTypeValue = "act"
                MsgBox("The selected file has an '.act' extension and will be treated as an Adobe Color Table palette.", MsgBoxStyle.Information, "Palette Info")

            End If

            If PalTypeValue = "riff" Then
                Dim count As Integer = 0
                For i = 24 To PalLines.Length - 1 Step +4
                    ColorArray(count) = (PalLines(i) & PalLines(i + 1) & PalLines(i + 2))
                    count = count + 1
                Next i

                If PalLines.Length < 88 Then
                    For z = (PalLines.Length - 24) / 4 To 15 Step +1
                        ColorArray(count) = "000000"
                        count = count + 1
                    Next
                End If

            ElseIf PalTypeValue = "jasc-pal" Then
                Dim JASCReader As String() = System.IO.File.ReadAllLines(PALBrowserDialog.FileName.ToString)

                If JASCReader(2) < 16 Then
                    For fill As Integer = 0 To 15 Step +1
                        ColorArray(fill) = "000000"
                    Next
                    Dim count As Integer = 0
                    For i = 3 To JASCReader(2) + 2 Step +1
                        ColorArray(count) = ConvertJASCtoPAL(JASCReader(i))
                        count = count + 1
                    Next

                Else
                    Dim count As Integer = 0
                    For i = 3 To 18 Step +1
                        ColorArray(count) = ConvertJASCtoPAL(JASCReader(i))
                        count = count + 1
                    Next
                End If

            ElseIf PalTypeValue = "act" Then
                For fill As Integer = 0 To 15 Step +1
                    ColorArray(fill) = "000000"
                Next

                Dim PaletteLimit As Integer = 47
                Dim count As Integer = 0

                For i = 0 To PaletteLimit Step +3
                    ColorArray(count) = (PalLines(i) & PalLines(i + 1) & PalLines(i + 2))
                    count = count + 1
                Next i
            End If

            'set real colors
            Panel1.BackColor = ColorTranslator.FromHtml("#" & ColorArray(0))
            Panel2.BackColor = ColorTranslator.FromHtml("#" & ColorArray(1))
            Panel3.BackColor = ColorTranslator.FromHtml("#" & ColorArray(2))
            Panel4.BackColor = ColorTranslator.FromHtml("#" & ColorArray(3))
            Panel5.BackColor = ColorTranslator.FromHtml("#" & ColorArray(4))
            Panel6.BackColor = ColorTranslator.FromHtml("#" & ColorArray(5))
            Panel7.BackColor = ColorTranslator.FromHtml("#" & ColorArray(6))
            Panel8.BackColor = ColorTranslator.FromHtml("#" & ColorArray(7))
            Panel9.BackColor = ColorTranslator.FromHtml("#" & ColorArray(8))
            Panel10.BackColor = ColorTranslator.FromHtml("#" & ColorArray(9))
            Panel11.BackColor = ColorTranslator.FromHtml("#" & ColorArray(10))
            Panel12.BackColor = ColorTranslator.FromHtml("#" & ColorArray(11))
            Panel13.BackColor = ColorTranslator.FromHtml("#" & ColorArray(12))
            Panel14.BackColor = ColorTranslator.FromHtml("#" & ColorArray(13))
            Panel15.BackColor = ColorTranslator.FromHtml("#" & ColorArray(14))
            Panel16.BackColor = ColorTranslator.FromHtml("#" & ColorArray(15))

            If System.IO.File.Exists("tempPalette.txt") Then
                System.IO.File.Delete("tempPalette.txt")
            End If
        End If
    End Sub

    Private Sub ExportRLCNButton_Click(sender As Object, e As EventArgs) Handles ExportRLCNButton.Click
        If ExportDialog.ShowDialog = DialogResult.OK Then

            Dim WriterEnc As New StreamWriter("temp-RLCN.txt")
            ' set basic header for 16 colors 4bpp RLCN
            WriterEnc.Write("524C434EFFFE0001FFFF00001000010054544C50FFFF00000300000000000000FFFF000010000000")
            For i As Integer = 0 To 16 - 1 Step +1
                WriterEnc.Write(RGBtoBGR(ColorArray(i)))
            Next i
            WriterEnc.Close()
            WriterEnc.Dispose()

            Dim TemporaryStream As String = System.IO.File.ReadAllText("temp-RLCN.txt")
            For index As Integer = 4 * (TemporaryStream.Length \ 4) To 0 Step -4
                TemporaryStream = TemporaryStream.Insert(index, Environment.NewLine)
            Next
            Dim cleanTemporaryStream As String = Regex.Replace(TemporaryStream, "^\s+$[\r\n]*", "", RegexOptions.Multiline)
            System.IO.File.WriteAllText("temp-RLCN-blocks.txt", cleanTemporaryStream)

            Dim RecountStream As String = System.IO.File.ReadAllText("temp-RLCN.txt")
            For index As Integer = 2 * (RecountStream.Length \ 2) To 0 Step -2
                RecountStream = RecountStream.Insert(index, Environment.NewLine)
            Next
            Dim cleanRecountStream As String = Regex.Replace(RecountStream, "^\s+$[\r\n]*", "", RegexOptions.Multiline)
            System.IO.File.WriteAllText("temp-RLCN.txt", cleanRecountStream)


            Dim LineCounter As String()
            LineCounter = System.IO.File.ReadAllLines("temp-RLCN.txt")

            Dim RLCNLines As String()
            RLCNLines = System.IO.File.ReadAllLines("temp-RLCN-blocks.txt")
            RLCNLines(4) = decimalToHexLittleEndian(LineCounter.Length, 2)
            RLCNLines(10) = decimalToHexLittleEndian(LineCounter.Length - 16, 2)

            If fourBppRadio.Checked = True Then
                RLCNLines(12) = "0300"
            ElseIf eightBppRadio.Checked = True Then
                RLCNLines(12) = "0400"
            End If

            RLCNLines(16) = decimalToHexLittleEndian(LineCounter.Length - 40, 2)
            System.IO.File.WriteAllLines("temp-RLCN-blocks.txt", RLCNLines)

            Dim FinalReader As String = System.IO.File.ReadAllText("temp-RLCN-blocks.txt")
            FinalReader = FinalReader.Replace(Environment.NewLine, "")
            System.IO.File.WriteAllText("temp-RLCN-blocks.txt", FinalReader)

            Using ReaderEnc As New System.IO.BinaryReader(System.IO.File.Open("temp-RLCN-blocks.txt", IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.ASCII)
                Using WriteHexEnc As New System.IO.BinaryWriter(System.IO.File.Open(ExportDialog.FileName, IO.FileMode.Create, IO.FileAccess.Write))
                    Dim by As Byte = 2

                    While ReaderEnc.BaseStream.Position < ReaderEnc.BaseStream.Length 'read each character from the source
                        Dim b As Byte = 0 'temp variable to build byte value
                        Dim c As Char = ReaderEnc.ReadChar 'current character in stream
                        If c = " "c Then c = ReaderEnc.ReadChar 'skip current if it is a space
                        b = Convert.ToByte(c, 16) << 4 'interpret character as most significant hex digit
                        c = ReaderEnc.ReadChar 'read next character
                        b = (b Or Convert.ToByte(c, 16)) 'interpret character as least significant hex digit
                        WriteHexEnc.Write(b) 'write byte to stream
                    End While
                End Using
            End Using

            If System.IO.File.Exists("temp-RLCN.txt") Then System.IO.File.Delete("temp-RLCN.txt")
            If System.IO.File.Exists("temp-RLCN-blocks.txt") Then System.IO.File.Delete("temp-RLCN-blocks.txt")

        End If

    End Sub

    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles Panel1.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel1.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel2.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub

    Private Sub Panel3_Click(sender As Object, e As EventArgs) Handles Panel3.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel3.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub
    Private Sub Panel4_Click(sender As Object, e As EventArgs) Handles Panel4.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel4.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub
    Private Sub Panel5_Click(sender As Object, e As EventArgs) Handles Panel5.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel5.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub

    Private Sub Panel6_Click(sender As Object, e As EventArgs) Handles Panel6.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel6.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub
    Private Sub Panel7_Click(sender As Object, e As EventArgs) Handles Panel7.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel7.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub
    Private Sub Panel8_Click(sender As Object, e As EventArgs) Handles Panel8.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel8.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub
    Private Sub Panel9_Click(sender As Object, e As EventArgs) Handles Panel9.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel9.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub
    Private Sub Panel10_Click(sender As Object, e As EventArgs) Handles Panel10.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel10.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub
    Private Sub Panel11_Click(sender As Object, e As EventArgs) Handles Panel11.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel11.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub
    Private Sub Panel12_Click(sender As Object, e As EventArgs) Handles Panel12.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel12.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub
    Private Sub Panel13_Click(sender As Object, e As EventArgs) Handles Panel13.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel13.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub
    Private Sub Panel14_Click(sender As Object, e As EventArgs) Handles Panel14.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel14.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub
    Private Sub Panel15_Click(sender As Object, e As EventArgs) Handles Panel15.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel15.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub
    Private Sub Panel16_Click(sender As Object, e As EventArgs) Handles Panel16.Click
        RGBTextBox.Text = ColorTranslator.ToHtml(Panel16.BackColor).Replace("#", "")
        ConvertButton.PerformClick()
    End Sub

    Private Sub Convert256PalButton_Click(sender As Object, e As EventArgs) Handles Convert256PalButton.Click
        MsgBox("Make sure you're selecting a 256-colors palette." & vbCrLf & vbCrLf & "Otherwise, it will probably crash", MsgBoxStyle.Information, "Select 256-colors pal")
    End Sub
End Class
