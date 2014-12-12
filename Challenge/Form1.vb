Imports System.Text.RegularExpressions
Imports System.IO

Public Class Form1
#Region "Stuff"
    Private Function CleanText(input As String, val As String) As String
        Return Regex.Replace(input, "\s{2,}", vbCrLf).Replace(vbCrLf, val)
    End Function

    Private Function SplitOnText(input As String, val As String) As String()
        Return CleanText(input, val).ToString().Split(New String() {val}, StringSplitOptions.RemoveEmptyEntries)
    End Function

#End Region
    Sub compile()
        Dim textt As String = ""
        For Each item As String In SplitOnText(TextBox1.Text, "####")
            textt += item & "---"
        Next
        MsgBox(textt)
        Using Writer As BinaryWriter = New BinaryWriter(File.Open("Script.bin", FileMode.Create))
            Writer.Write(textt)
        End Using
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        compile()
    End Sub
End Class
