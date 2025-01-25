Imports System.Drawing
Imports System.IO
Imports System.Text.RegularExpressions

Public Class Injector
    Dim ColorArray As String() = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15"}
    Dim RLCNArray(16) As String
    Dim RLCNFullLines As String()
    Dim RLCNBlockLines As String()
    Dim RLCNPosition As Integer = 0
    Dim BasePosition As Integer = 40
    Dim LoadingDone As Boolean = False
    Dim ColorChanging As Boolean = False
    Dim FirstFileOpened As Boolean = False

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
    Public Function HexToTextBlock(HexBytes As Byte(), filename As String)

        Dim Hex As String() = Array.ConvertAll(HexBytes, Function(b) b.ToString("X2"))
        Dim Values As String = (String.Join("", Hex))
        For index As Integer = 4 * (Values.Length \ 4) To 0 Step -4
            Values = Values.Insert(index, Environment.NewLine)
        Next
        Dim clean As String = Regex.Replace(Values, "^\s+$[\r\n]*", "", RegexOptions.Multiline)
        System.IO.File.WriteAllText(filename, clean)

    End Function
    Public Function RGBtoBGR(ByVal COLORSTRING As String)

        '' adapted from this post: https://stackoverflow.com/a/75000283

        Dim rgb = ColorTranslator.FromHtml(COLORSTRING)
        ' Divide R, G and B values by 8 and shifts left by 0, 5 and 10 positions 
        ' (16 bit pixel, each color in 5 bits)
        Dim colorBGR555 = CShort((rgb.R \ 8) + ((rgb.G \ 8) << 5) + ((rgb.B \ 8) << 10))
        ' Converts to string in Hex Format (Big Endian)  
        Dim colorBGR555HexBE = colorBGR555.ToString("X2")

        ' Reverse the bytes (it's a Short value, two bytes) for its Little Endian representation
        Dim bytes = BitConverter.GetBytes(colorBGR555).Reverse().ToArray()
        Dim colorBGR555HexLE = BitConverter.ToInt16(bytes, 0).ToString("X2")
        Return colorBGR555HexLE

    End Function
    Public Function ReverseHex(ByVal COLORTEXT As String)

        Dim ColorShort As UShort = "&H" & COLORTEXT
        Dim bytes = BitConverter.GetBytes(ColorShort).Reverse().ToArray()
        Dim colorReverse = BitConverter.ToInt16(bytes, 0).ToString("X2")

        Return colorReverse

    End Function

    Public Function ConvertJASCtoPAL(ByVal SelectedLine As String)

        Dim delimiter As Char() = {" "}

        Dim SplitColor As String() = SelectedLine.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)

        Dim ConvertedJASCColor As Color = Color.FromArgb(SplitColor(0), SplitColor(1), SplitColor(2))
        Dim HTMLConvertedString As String = ColorTranslator.ToHtml(ConvertedJASCColor).ToString
        HTMLConvertedString = HTMLConvertedString.Replace("#", "")
        Return HTMLConvertedString

    End Function
    Public Function BGRtoHTML(ByVal COLOROBJECT As String)

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
    Public Function UpdateColorFromHTML(ByVal HTMLValue As String, HexTextBox As Object, panel As Object, colornumber As Integer)

        If LoadingDone = False Or ColorChanging = True Then
            Exit Function
        ElseIf HTMLValue.Length < 6 Then
            Exit Function
        ElseIf HTMLValue.Contains("#") Then
            MsgBox("Do not include # character", MsgBoxStyle.Exclamation, "Error")
            Exit Function
        End If

        Dim BGRValue As String = RGBtoBGR("#" & HTMLValue)

        If BGRValue.Length = 2 Then
            BGRValue = "00" & RGBtoBGR("#" & HTMLValue)
        ElseIf BGRValue.Length = 3 Then
            BGRValue = "0" & RGBtoBGR("#" & HTMLValue)
        ElseIf RGBtoBGR("#" & HTMLValue) = "00" Then
            BGRValue = "0000"
        ElseIf HTMLValue = "000000" Then
            BGRValue = "0000"
        End If

        HexTextBox.Text = BGRValue
        panel.BackColor = ColorTranslator.FromHtml("#" & HTMLValue)

        Dim firstbyte = BGRValue.Substring(0, BGRValue.Length / 2)
        Dim lastbyte = BGRValue.Substring(BGRValue.Length / 2, BGRValue.Length / 2)

        RLCNFullLines(40 + (colornumber * 2)) = firstbyte
        RLCNFullLines(40 + (colornumber * 2) + 1) = lastbyte

    End Function
    Public Function UpdateColorFromBGR(ByVal BGRValue As String, HTMLTextBox As Object, panel As Object, colornumber As Integer)

        Exit Function '' deprecated function
        If LoadingDone = False Or ColorChanging = True Then
            Exit Function
        End If
        Dim RGBValue As String = BGRtoHTML(BGRValue)
        HTMLTextBox.Text = RGBValue
        panel.BackColor = ColorTranslator.FromHtml("#" & RGBValue)
        Dim firstbyte = BGRValue.Substring(0, BGRValue.Length / 2)
        Dim lastbyte = BGRValue.Substring(BGRValue.Length / 2, BGRValue.Length / 2)
        RLCNFullLines(40 + (colornumber * 2)) = firstbyte
        RLCNFullLines(40 + (colornumber * 2) + 1) = lastbyte

    End Function
    Private Sub ImportPALButton_Click(sender As Object, e As EventArgs) Handles ImportPALButton.Click

        BasePosition = 40 + (PaletteSelectorCombo.SelectedIndex * 32)

        If PALBrowserDialog.ShowDialog = DialogResult.OK Then
            ''If MsgBox("Replace " & PaletteSelectorCombo.SelectedItem.ToString & " ?", MsgBoxStyle.YesNo, "Confirmation") = MsgBoxResult.No Then
            ''Exit Sub
            ''End If
            Dim PalTypeValue As String = "riff"
            Dim ReadPalBytes As Byte() = IO.File.ReadAllBytes(PALBrowserDialog.FileName.ToString)
            HexToText(ReadPalBytes, ".\tempPalette.txt")

            Dim PalLines As String()
            PalLines = System.IO.File.ReadAllLines(".\tempPalette.txt")

            If PalLines(0) = "4A" AndAlso PalLines(1) = "41" AndAlso PalLines(2) = "53" AndAlso PalLines(3) = "43" AndAlso PalLines(4) = "2D" AndAlso PalLines(5) = "50" AndAlso PalLines(6) = "41" AndAlso PalLines(7) = "4C" Then
                PalTypeValue = "jasc-pal"
                MsgBox("This .pal file appears to be in JASC-PAL Format  (Aseprite?)" & vbCrLf & vbCrLf & "Output will be in Microsoft RIFF palette (.pal) format", MsgBoxStyle.Information, "Palette format")

            ElseIf Path.GetExtension(PALBrowserDialog.FileName.ToString).ToLower() = ".act" Then
                PalTypeValue = "act"
                MsgBox("The selected file has an '.act' extension and will be treated as an Adobe Color Table palette.", MsgBoxStyle.Information, "Palette Info")
            End If

            If PalTypeValue = "riff" Then

                Dim PaletteLimit As Integer = 87

                If PalLines.Length < 87 Then
                    PaletteLimit = PalLines.Length - 1
                    For fill As Integer = 0 To 15 Step +1
                        ColorArray(fill) = "000000"
                    Next
                End If

                Dim count As Integer = 0

                For i = 24 To PaletteLimit Step +4
                    ColorArray(count) = (PalLines(i) & PalLines(i + 1) & PalLines(i + 2))
                    count = count + 1
                Next i

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

            Dim recount As Integer = 0
            Dim ConvertedColor As String = RGBtoBGR("#" & ColorArray(recount))
            Dim firstbyte As String
            Dim lastbyte As String

            For RLCNPosition As Integer = BasePosition To (BasePosition + 32) - 1 Step +2

                If RGBtoBGR("#" & ColorArray(recount)).Length = 2 Then
                    firstbyte = ("00" & RGBtoBGR("#" & ColorArray(recount))).Substring(0, ("00" & RGBtoBGR("#" & ColorArray(recount))).Length / 2)
                    lastbyte = ("00" & RGBtoBGR("#" & ColorArray(recount))).Substring(("00" & RGBtoBGR("#" & ColorArray(recount))).Length / 2, ("00" & RGBtoBGR("#" & ColorArray(recount))).Length / 2)
                ElseIf RGBtoBGR("#" & ColorArray(recount)).Length = 3 Then
                    firstbyte = ("0" & RGBtoBGR("#" & ColorArray(recount))).Substring(0, ("0" & RGBtoBGR("#" & ColorArray(recount))).Length / 2)
                    lastbyte = ("0" & RGBtoBGR("#" & ColorArray(recount))).Substring(("0" & RGBtoBGR("#" & ColorArray(recount))).Length / 2, ("0" & RGBtoBGR("#" & ColorArray(recount))).Length / 2)
                ElseIf RGBtoBGR("#" & ColorArray(recount)) = "00" Then
                    firstbyte = "00"
                    lastbyte = "00"
                Else
                    firstbyte = RGBtoBGR("#" & ColorArray(recount)).Substring(0, RGBtoBGR("#" & ColorArray(recount)).Length / 2)
                    lastbyte = RGBtoBGR("#" & ColorArray(recount)).Substring(RGBtoBGR("#" & ColorArray(recount)).Length / 2, RGBtoBGR("#" & ColorArray(recount)).Length / 2)
                End If

                If ColorArray(recount) = "000000" Then
                    RLCNFullLines(RLCNPosition) = "00"
                    RLCNFullLines(RLCNPosition + 1) = "00"
                Else
                    RLCNFullLines(RLCNPosition) = firstbyte
                    RLCNFullLines(RLCNPosition + 1) = lastbyte
                End If

                recount = recount + 1
            Next

            TextBox1.Text = ColorArray(0)
            TextBox2.Text = ColorArray(1)
            TextBox3.Text = ColorArray(2)
            TextBox4.Text = ColorArray(3)
            TextBox5.Text = ColorArray(4)
            TextBox6.Text = ColorArray(5)
            TextBox7.Text = ColorArray(6)
            TextBox8.Text = ColorArray(7)
            TextBox9.Text = ColorArray(8)
            TextBox10.Text = ColorArray(9)
            TextBox11.Text = ColorArray(10)
            TextBox12.Text = ColorArray(11)
            TextBox13.Text = ColorArray(12)
            TextBox14.Text = ColorArray(13)
            TextBox15.Text = ColorArray(14)
            TextBox16.Text = ColorArray(15)
            TextBox32.Text = RLCNFullLines(BasePosition) & RLCNFullLines(BasePosition + 1)
            TextBox31.Text = RLCNFullLines(BasePosition + 2) & RLCNFullLines(BasePosition + 3)
            TextBox30.Text = RLCNFullLines(BasePosition + 4) & RLCNFullLines(BasePosition + 5)
            TextBox29.Text = RLCNFullLines(BasePosition + 6) & RLCNFullLines(BasePosition + 7)
            TextBox28.Text = RLCNFullLines(BasePosition + 8) & RLCNFullLines(BasePosition + 9)
            TextBox27.Text = RLCNFullLines(BasePosition + 10) & RLCNFullLines(BasePosition + 11)
            TextBox26.Text = RLCNFullLines(BasePosition + 12) & RLCNFullLines(BasePosition + 13)
            TextBox25.Text = RLCNFullLines(BasePosition + 14) & RLCNFullLines(BasePosition + 15)
            TextBox24.Text = RLCNFullLines(BasePosition + 16) & RLCNFullLines(BasePosition + 17)
            TextBox23.Text = RLCNFullLines(BasePosition + 18) & RLCNFullLines(BasePosition + 19)
            TextBox22.Text = RLCNFullLines(BasePosition + 20) & RLCNFullLines(BasePosition + 21)
            TextBox21.Text = RLCNFullLines(BasePosition + 22) & RLCNFullLines(BasePosition + 23)
            TextBox20.Text = RLCNFullLines(BasePosition + 24) & RLCNFullLines(BasePosition + 25)
            TextBox19.Text = RLCNFullLines(BasePosition + 26) & RLCNFullLines(BasePosition + 27)
            TextBox18.Text = RLCNFullLines(BasePosition + 28) & RLCNFullLines(BasePosition + 29)
            TextBox17.Text = RLCNFullLines(BasePosition + 30) & RLCNFullLines(BasePosition + 31)

            For i = 0 To 15 Step +1
                RLCNArray(i) = ColorArray(i)
            Next i

            If System.IO.File.Exists(".\tempPalette.txt") Then System.IO.File.Delete(".\tempPalette.txt")

        End If

    End Sub

    Private Sub ExportRLCNButton_Click(sender As Object, e As EventArgs) Handles ExportRLCNButton.Click

        If SaveRLCNDialog.ShowDialog = DialogResult.OK Then
            If fourbppRadio.Checked = True Then
                RLCNFullLines(24) = "03"
            ElseIf EightBppButton.Checked = True Then
                RLCNFullLines(24) = "04"
            End If

            Dim PaletteCount As Integer = (RLCNFullLines.Length - 40) / 32
            If PaletteCount < 1 Then PaletteCount = 1
            Dim ColorCount As Integer = (PaletteCount * 16) * 2
            Dim colorcountbyte1 = decimalToHexLittleEndian(ColorCount, 2).Substring(0, decimalToHexLittleEndian(ColorCount, 2).Length / 2)
            Dim colorcountbyte2 = decimalToHexLittleEndian(ColorCount, 2).Substring(decimalToHexLittleEndian(ColorCount, 2).Length / 2, decimalToHexLittleEndian(ColorCount, 2).Length / 2)

            RLCNFullLines(32) = colorcountbyte1
            RLCNFullLines(33) = colorcountbyte2

            Dim FileLength = RLCNFullLines.Length
            Dim SectionLength = FileLength - 16

            Dim FileLengthbyte1 = decimalToHexLittleEndian(FileLength, 2).Substring(0, decimalToHexLittleEndian(FileLength, 2).Length / 2)
            Dim FileLengthbyte2 = decimalToHexLittleEndian(FileLength, 2).Substring(decimalToHexLittleEndian(FileLength, 2).Length / 2, decimalToHexLittleEndian(FileLength, 2).Length / 2)
            Dim SectionLengthbyte1 = decimalToHexLittleEndian(SectionLength, 2).Substring(0, decimalToHexLittleEndian(SectionLength, 2).Length / 2)
            Dim SectionLengthbyte2 = decimalToHexLittleEndian(SectionLength, 2).Substring(decimalToHexLittleEndian(SectionLength, 2).Length / 2, decimalToHexLittleEndian(SectionLength, 2).Length / 2)

            RLCNFullLines(8) = FileLengthbyte1
            RLCNFullLines(9) = FileLengthbyte2
            RLCNFullLines(20) = SectionLengthbyte1
            RLCNFullLines(21) = SectionLengthbyte2

            System.IO.File.WriteAllLines("temp-RLCN.txt", RLCNFullLines)

            Dim LineCounter As String()
            LineCounter = System.IO.File.ReadAllLines("temp-RLCN.txt")

            'Dim RLCNLines As String()
            'RLCNLines = System.IO.File.ReadAllLines("temp-RLCN-blocks.txt")
            'RLCNLines(4) = decimalToHexLittleEndian(LineCounter.Length, 2)
            'RLCNLines(10) = decimalToHexLittleEndian(LineCounter.Length - 16, 2)
            'RLCNLines(12) = 0300 for 4bpp, 0400 for 8bpp
            'RLCNLines(16) = decimalToHexLittleEndian(LineCounter.Length - 40, 2)
            'System.IO.File.WriteAllLines("temp-RLCN-blocks.txt", RLCNLines)


            Dim FinalReader As String = System.IO.File.ReadAllText("temp-RLCN.txt")
            FinalReader = FinalReader.Replace(Environment.NewLine, "")
            System.IO.File.WriteAllText("temp-RLCN.txt", FinalReader)

            Using ReaderEnc As New System.IO.BinaryReader(System.IO.File.Open("temp-RLCN.txt", IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.ASCII)
                Using WriteHexEnc As New System.IO.BinaryWriter(System.IO.File.Open(SaveRLCNDialog.FileName, IO.FileMode.Create, IO.FileAccess.Write))
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

            MsgBox(".RLCN file saved succesfully!", MsgBoxStyle.OkOnly, "Success")

        End If

    End Sub

    Private Sub OpenRLCNButton_Click(sender As Object, e As EventArgs) Handles OpenRLCNButton.Click

        If RLCNBrowserDialog.ShowDialog = vbOK Then

            If FirstFileOpened = True Then
                PaletteSelectorCombo.SelectedIndex = 0
            End If

            PaletteSelectorCombo.Items.Clear()

            LoadingDone = False
            'RLCNPosition = 40

            Dim MissingLength As Integer = 0
            Dim ReadRLCNBytes As Byte() = IO.File.ReadAllBytes(RLCNBrowserDialog.FileName.ToString)

            If ReadRLCNBytes.Length < 72 Then
                If System.IO.File.Exists("tempfix.RLCN") Then System.IO.File.Delete("tempfix.RLCN")
                System.IO.File.Copy(RLCNBrowserDialog.FileName.ToString, "tempfix.RLCN")
                Dim binarywriter As New System.IO.BinaryWriter(File.Open("tempfix.RLCN", FileMode.Open))
                MissingLength = (71 - ReadRLCNBytes.Length)
                Using binarywriter
                    For i = 0 To MissingLength / 2 Step +2
                        binarywriter.Seek(0, SeekOrigin.End)
                        binarywriter.Write(&H0)
                    Next
                End Using
                ReadRLCNBytes = IO.File.ReadAllBytes("tempfix.RLCN")
                If System.IO.File.Exists("tempfix.RLCN") Then System.IO.File.Delete("tempfix.RLCN")
            End If

            HexToText(ReadRLCNBytes, ".\tempRLCN-read.txt")
            HexToTextBlock(ReadRLCNBytes, ".\tempRLCN-blocks.txt")

            RLCNFullLines = System.IO.File.ReadAllLines(".\tempRLCN-read.txt")

            RLCNBlockLines = System.IO.File.ReadAllLines(".\tempRLCN-blocks.txt")

            If RLCNFullLines(24) = "03" Then
                fourbppRadio.Checked = True
                EightBppButton.Checked = False
            ElseIf RLCNFullLines(24) = "04" Then
                EightBppButton.Checked = True
                fourbppRadio.Checked = False
            End If

            Dim Limits As Integer = RLCNFullLines.Length
            Dim PaletteCount As Integer = (RLCNFullLines.Length - 40) / 32

            For i As Integer = 0 To PaletteCount - 1 Step +1
                PaletteSelectorCombo.Items.Add("Palette " & i)
            Next

            Dim count As Integer = 0

            For RLCNPosition As Integer = BasePosition To (BasePosition + 32) - 1 Step +2
                If RLCNPosition = RLCNFullLines.Length Then
                    Exit For
                End If
                RLCNArray(count) = BGRtoHTML(RLCNFullLines(RLCNPosition) & RLCNFullLines(RLCNPosition + 1))
                count = count + 1
                'PaletteSelectorCombo.Items.Add("Palette " & count)
            Next RLCNPosition

            Panel1.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(0))
            Panel2.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(1))
            Panel3.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(2))
            Panel4.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(3))
            Panel5.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(4))
            Panel6.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(5))
            Panel7.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(6))
            Panel8.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(7))
            Panel9.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(8))
            Panel10.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(9))
            Panel11.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(10))
            Panel12.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(11))
            Panel13.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(12))
            Panel14.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(13))
            Panel15.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(14))
            Panel16.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(15))
            PaletteSelectorCombo.SelectedIndex = 0
            BasePosition = 40 + (PaletteSelectorCombo.SelectedIndex * 32)

            TextBox1.Text = RLCNArray(0)
            TextBox2.Text = RLCNArray(1)
            TextBox3.Text = RLCNArray(2)
            TextBox4.Text = RLCNArray(3)
            TextBox5.Text = RLCNArray(4)
            TextBox6.Text = RLCNArray(5)
            TextBox7.Text = RLCNArray(6)
            TextBox8.Text = RLCNArray(7)
            TextBox9.Text = RLCNArray(8)
            TextBox10.Text = RLCNArray(9)
            TextBox11.Text = RLCNArray(10)
            TextBox12.Text = RLCNArray(11)
            TextBox13.Text = RLCNArray(12)
            TextBox14.Text = RLCNArray(13)
            TextBox15.Text = RLCNArray(14)
            TextBox16.Text = RLCNArray(15)
            TextBox32.Text = RLCNFullLines(BasePosition) & RLCNFullLines(BasePosition + 1)
            TextBox31.Text = RLCNFullLines(BasePosition + 2) & RLCNFullLines(BasePosition + 3)
            TextBox30.Text = RLCNFullLines(BasePosition + 4) & RLCNFullLines(BasePosition + 5)
            TextBox29.Text = RLCNFullLines(BasePosition + 6) & RLCNFullLines(BasePosition + 7)
            TextBox28.Text = RLCNFullLines(BasePosition + 8) & RLCNFullLines(BasePosition + 9)
            TextBox27.Text = RLCNFullLines(BasePosition + 10) & RLCNFullLines(BasePosition + 11)
            TextBox26.Text = RLCNFullLines(BasePosition + 12) & RLCNFullLines(BasePosition + 13)
            TextBox25.Text = RLCNFullLines(BasePosition + 14) & RLCNFullLines(BasePosition + 15)
            TextBox24.Text = RLCNFullLines(BasePosition + 16) & RLCNFullLines(BasePosition + 17)
            TextBox23.Text = RLCNFullLines(BasePosition + 18) & RLCNFullLines(BasePosition + 19)
            TextBox22.Text = RLCNFullLines(BasePosition + 20) & RLCNFullLines(BasePosition + 21)
            TextBox21.Text = RLCNFullLines(BasePosition + 22) & RLCNFullLines(BasePosition + 23)
            TextBox20.Text = RLCNFullLines(BasePosition + 24) & RLCNFullLines(BasePosition + 25)
            TextBox19.Text = RLCNFullLines(BasePosition + 26) & RLCNFullLines(BasePosition + 27)
            TextBox18.Text = RLCNFullLines(BasePosition + 28) & RLCNFullLines(BasePosition + 29)
            TextBox17.Text = RLCNFullLines(BasePosition + 30) & RLCNFullLines(BasePosition + 31)

            ImportPALButton.Enabled = True
            ExportRLCNButton.Enabled = True
            ExportAsPalButton.Enabled = True
            ComboNextButton.Enabled = True
            ComboPreviousButton.Enabled = True

            LoadingDone = True
            FirstFileOpened = True

            If System.IO.File.Exists("tempRLCN-read.txt") Then System.IO.File.Delete("tempRLCN-read.txt")
            If System.IO.File.Exists("tempRLCN-blocks.txt") Then System.IO.File.Delete("tempRLCN-blocks.txt")

        End If

    End Sub

    Private Sub PaletteSelectorCombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PaletteSelectorCombo.SelectedIndexChanged

        If LoadingDone = False Then
            Exit Sub
        End If

        BasePosition = 40 + (PaletteSelectorCombo.SelectedIndex * 32)

        ColorChanging = True

        Dim count As Integer = 0

        For RLCNPosition As Integer = BasePosition To (BasePosition + 32) - 1 Step +2
            If RLCNPosition > RLCNFullLines.Length Then
                Exit For
            End If
            RLCNArray(count) = BGRtoHTML(RLCNFullLines(RLCNPosition) & RLCNFullLines(RLCNPosition + 1))
            count = count + 1

        Next RLCNPosition

        Panel1.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(0))
        Panel2.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(1))
        Panel3.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(2))
        Panel4.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(3))
        Panel5.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(4))
        Panel6.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(5))
        Panel7.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(6))
        Panel8.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(7))
        Panel9.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(8))
        Panel10.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(9))
        Panel11.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(10))
        Panel12.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(11))
        Panel13.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(12))
        Panel14.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(13))
        Panel15.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(14))
        Panel16.BackColor = ColorTranslator.FromHtml("#" & RLCNArray(15))
        TextBox1.Text = RLCNArray(0)
        TextBox2.Text = RLCNArray(1)
        TextBox3.Text = RLCNArray(2)
        TextBox4.Text = RLCNArray(3)
        TextBox5.Text = RLCNArray(4)
        TextBox6.Text = RLCNArray(5)
        TextBox7.Text = RLCNArray(6)
        TextBox8.Text = RLCNArray(7)
        TextBox9.Text = RLCNArray(8)
        TextBox10.Text = RLCNArray(9)
        TextBox11.Text = RLCNArray(10)
        TextBox12.Text = RLCNArray(11)
        TextBox13.Text = RLCNArray(12)
        TextBox14.Text = RLCNArray(13)
        TextBox15.Text = RLCNArray(14)
        TextBox16.Text = RLCNArray(15)
        TextBox32.Text = RLCNFullLines(BasePosition) & RLCNFullLines(BasePosition + 1)
        TextBox31.Text = RLCNFullLines(BasePosition + 2) & RLCNFullLines(BasePosition + 3)
        TextBox30.Text = RLCNFullLines(BasePosition + 4) & RLCNFullLines(BasePosition + 5)
        TextBox29.Text = RLCNFullLines(BasePosition + 6) & RLCNFullLines(BasePosition + 7)
        TextBox28.Text = RLCNFullLines(BasePosition + 8) & RLCNFullLines(BasePosition + 9)
        TextBox27.Text = RLCNFullLines(BasePosition + 10) & RLCNFullLines(BasePosition + 11)
        TextBox26.Text = RLCNFullLines(BasePosition + 12) & RLCNFullLines(BasePosition + 13)
        TextBox25.Text = RLCNFullLines(BasePosition + 14) & RLCNFullLines(BasePosition + 15)
        TextBox24.Text = RLCNFullLines(BasePosition + 16) & RLCNFullLines(BasePosition + 17)
        TextBox23.Text = RLCNFullLines(BasePosition + 18) & RLCNFullLines(BasePosition + 19)
        TextBox22.Text = RLCNFullLines(BasePosition + 20) & RLCNFullLines(BasePosition + 21)
        TextBox21.Text = RLCNFullLines(BasePosition + 22) & RLCNFullLines(BasePosition + 23)
        TextBox20.Text = RLCNFullLines(BasePosition + 24) & RLCNFullLines(BasePosition + 25)
        TextBox19.Text = RLCNFullLines(BasePosition + 26) & RLCNFullLines(BasePosition + 27)
        TextBox18.Text = RLCNFullLines(BasePosition + 28) & RLCNFullLines(BasePosition + 29)
        TextBox17.Text = RLCNFullLines(BasePosition + 30) & RLCNFullLines(BasePosition + 31)
        ColorChanging = False

    End Sub

    Private Sub ComboPreviousButton_Click(sender As Object, e As EventArgs) Handles ComboPreviousButton.Click

        If PaletteSelectorCombo.SelectedIndex = 0 Then
            Exit Sub
        ElseIf PaletteSelectorCombo.Items.Count = 1 Then
            Exit Sub
        Else
            PaletteSelectorCombo.SelectedIndex = PaletteSelectorCombo.SelectedIndex - 1
        End If

    End Sub

    Private Sub ComboNextButton_Click(sender As Object, e As EventArgs) Handles ComboNextButton.Click

        If PaletteSelectorCombo.SelectedIndex = 15 Then
            Exit Sub
        ElseIf PaletteSelectorCombo.Items.Count = 1 Then
            Exit Sub
        Else
            PaletteSelectorCombo.SelectedIndex = PaletteSelectorCombo.SelectedIndex + 1
        End If

    End Sub

    Private Sub fourbppRadio_CheckedChanged(sender As Object, e As EventArgs) Handles fourbppRadio.CheckedChanged

        ' If EightBppButton.Checked = True Then
        'fourbppRadio.Checked = False

        'End If

    End Sub

    Private Sub EightBppButton_CheckedChanged(sender As Object, e As EventArgs) Handles EightBppButton.CheckedChanged

        'If fourbppRadio.Checked = True Then
        'EightBppButton.Checked = False

        ' End If

    End Sub

    Private Sub Injector_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ImportPALButton.Enabled = False
        ExportRLCNButton.Enabled = False
        ComboNextButton.Enabled = False
        ComboPreviousButton.Enabled = False
        ExportAsPalButton.Enabled = False
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        UpdateColorFromHTML(TextBox1.Text, TextBox32, Panel1, 0)
    End Sub

    Private Sub TextBox32_TextChanged(sender As Object, e As EventArgs) Handles TextBox32.Validating
        UpdateColorFromBGR(TextBox32.Text, TextBox1, Panel1, 0)
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        UpdateColorFromHTML(TextBox2.Text, TextBox31, Panel2, 1)
    End Sub

    Private Sub TextBox31_TextChanged(sender As Object, e As EventArgs) Handles TextBox31.Validating
        UpdateColorFromBGR(TextBox31.Text, TextBox2, Panel2, 1)
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        UpdateColorFromHTML(TextBox3.Text, TextBox30, Panel3, 2)
    End Sub

    Private Sub TextBox30_TextChanged(sender As Object, e As EventArgs) Handles TextBox30.Validating
        UpdateColorFromBGR(TextBox30.Text, TextBox3, Panel3, 2)
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        UpdateColorFromHTML(TextBox4.Text, TextBox29, Panel4, 3)
    End Sub

    Private Sub TextBox29_TextChanged(sender As Object, e As EventArgs) Handles TextBox29.Validating
        UpdateColorFromBGR(TextBox29.Text, TextBox4, Panel4, 3)
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        UpdateColorFromHTML(TextBox5.Text, TextBox28, Panel5, 4)
    End Sub

    Private Sub TextBox28_TextChanged(sender As Object, e As EventArgs) Handles TextBox28.Validating
        UpdateColorFromBGR(TextBox28.Text, TextBox5, Panel5, 4)
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        UpdateColorFromHTML(TextBox6.Text, TextBox27, Panel6, 5)
    End Sub

    Private Sub TextBox27_TextChanged(sender As Object, e As EventArgs) Handles TextBox27.Validating
        UpdateColorFromBGR(TextBox27.Text, TextBox6, Panel6, 5)
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        UpdateColorFromHTML(TextBox7.Text, TextBox26, Panel7, 6)
    End Sub

    Private Sub TextBox26_TextChanged(sender As Object, e As EventArgs) Handles TextBox26.Validating
        UpdateColorFromBGR(TextBox26.Text, TextBox7, Panel7, 6)
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        UpdateColorFromHTML(TextBox8.Text, TextBox25, Panel8, 7)
    End Sub

    Private Sub TextBox25_TextChanged(sender As Object, e As EventArgs) Handles TextBox25.Validating
        UpdateColorFromBGR(TextBox25.Text, TextBox8, Panel8, 7)
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        UpdateColorFromHTML(TextBox9.Text, TextBox24, Panel9, 8)
    End Sub

    Private Sub TextBox24_TextChanged(sender As Object, e As EventArgs) Handles TextBox24.Validating
        UpdateColorFromBGR(TextBox24.Text, TextBox9, Panel9, 8)
    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        UpdateColorFromHTML(TextBox10.Text, TextBox23, Panel10, 9)
    End Sub

    Private Sub TextBox23_TextChanged(sender As Object, e As EventArgs) Handles TextBox23.Validating
        UpdateColorFromBGR(TextBox23.Text, TextBox10, Panel10, 9)
    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        UpdateColorFromHTML(TextBox11.Text, TextBox22, Panel11, 10)
    End Sub

    Private Sub TextBox22_TextChanged(sender As Object, e As EventArgs) Handles TextBox22.Validating
        UpdateColorFromBGR(TextBox22.Text, TextBox11, Panel11, 10)
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        UpdateColorFromHTML(TextBox12.Text, TextBox21, Panel12, 11)
    End Sub

    Private Sub TextBox21_TextChanged(sender As Object, e As EventArgs) Handles TextBox21.Validating
        UpdateColorFromBGR(TextBox21.Text, TextBox12, Panel12, 11)
    End Sub

    Private Sub TextBox13_TextChanged(sender As Object, e As EventArgs) Handles TextBox13.TextChanged
        UpdateColorFromHTML(TextBox13.Text, TextBox20, Panel13, 12)
    End Sub

    Private Sub TextBox20_TextChanged(sender As Object, e As EventArgs) Handles TextBox20.Validating
        UpdateColorFromBGR(TextBox20.Text, TextBox13, Panel13, 12)
    End Sub

    Private Sub TextBox14_TextChanged(sender As Object, e As EventArgs) Handles TextBox14.TextChanged
        UpdateColorFromHTML(TextBox14.Text, TextBox19, Panel14, 13)
    End Sub

    Private Sub TextBox19_TextChanged(sender As Object, e As EventArgs) Handles TextBox19.Validating
        UpdateColorFromBGR(TextBox19.Text, TextBox14, Panel14, 13)
    End Sub

    Private Sub TextBox15_TextChanged(sender As Object, e As EventArgs) Handles TextBox15.TextChanged
        UpdateColorFromHTML(TextBox15.Text, TextBox18, Panel15, 14)
    End Sub

    Private Sub TextBox18_TextChanged(sender As Object, e As EventArgs) Handles TextBox18.Validating
        UpdateColorFromBGR(TextBox18.Text, TextBox15, Panel15, 14)
    End Sub

    Private Sub TextBox16_TextChanged(sender As Object, e As EventArgs) Handles TextBox16.TextChanged
        UpdateColorFromHTML(TextBox16.Text, TextBox17, Panel16, 15)
    End Sub

    Private Sub TextBox17_TextChanged(sender As Object, e As EventArgs) Handles TextBox17.Validating
        UpdateColorFromBGR(TextBox17.Text, TextBox16, Panel16, 15)
    End Sub

    Private Sub ConverterButton_Click(sender As Object, e As EventArgs) Handles ConverterButton.Click
        Converter.Show()
    End Sub

    Private Sub ExportAsPalButton_Click(sender As Object, e As EventArgs) Handles ExportAsPalButton.Click

        If ExportPALDialog.ShowDialog = vbOK Then
            Dim WriterEnc As New StreamWriter("temp-PAL.txt")
            ' set basic header for 16 colors PAL
            WriterEnc.Write("524946465000000050414C20646174614400000000031000")
            For i As Integer = 0 To 16 - 1 Step +1
                WriterEnc.Write(RLCNArray(i) & "00")
            Next i
            WriterEnc.Close()
            WriterEnc.Dispose()

            Using ReaderEnc As New System.IO.BinaryReader(System.IO.File.Open("temp-PAL.txt", IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.ASCII)
                Using WriteHexEnc As New System.IO.BinaryWriter(System.IO.File.Open(ExportPALDialog.FileName, IO.FileMode.Create, IO.FileAccess.Write))
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

            If System.IO.File.Exists("temp-PAL.txt") Then System.IO.File.Delete("temp-PAL.txt")

        End If

    End Sub
End Class
