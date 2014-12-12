Imports System.IO
Imports System.Text.RegularExpressions


Module Module1
#Region "Stuff"
    Private Function CleanText(input As String, val As String) As String
        Return Regex.Replace(input, "\s{2,}", vbCrLf).Replace(vbCrLf, val)
    End Function

    Private Function SplitOnText(input As String, val As String) As String()
        Return CleanText(input, val).ToString().Split(New String() {val}, StringSplitOptions.RemoveEmptyEntries)
    End Function

#End Region
    Dim _script As String = ""
    Dim Variables As New Dictionary(Of String, Integer)

    Sub Main()
        Console.WriteLine("Please specify what script you'd like to load(Dont include.bin):")
        Dostuff(Console.ReadLine())
        Dim e() As String = _script.Split("---")
        For Each i As String In e
            If (i = String.Empty) Then
            Else
                Dim SpaceFinder() = i.Split(" ")
                Select Case (SpaceFinder(0).ToLower)
                    Case "set"
                        Variables.Add(SpaceFinder(1).Replace(",", ""), SpaceFinder(2))
                    Case "add"
                        Variables(SpaceFinder(1).Replace(",", "")) += Variables(SpaceFinder(2))
                    Case "print"
                        Console.WriteLine(Variables(SpaceFinder(1)))
                    Case "sub"
                        Variables(SpaceFinder(1).Replace(",", "")) -= Variables(SpaceFinder(2))
                    Case "mul"
                        Variables(SpaceFinder(1).Replace(",", "")) *= Variables(SpaceFinder(2))
                    Case "div"
                        Variables(SpaceFinder(1).Replace(",", "")) /= Variables(SpaceFinder(2))
                End Select
            End If
        Next
        Console.ReadLine()
    End Sub
    Sub Dostuff(text As String)
        Dim value As String = ""
        Using Read As New BinaryReader(File.Open(text & ".bin", FileMode.Open))
            Dim pos As Integer = 0
            Dim length As Integer = Read.BaseStream.Length
            While pos < length - 7
                Try
                    value += Read.ReadString()
                    _script = value
                Catch
                    Exit Sub
                End Try
                pos += 7
            End While
        End Using
    End Sub
End Module
