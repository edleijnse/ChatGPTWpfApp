Imports System.IO
Imports System.Net.Http

Class MainWindow

    Private Shared _contentHistory As List(Of String) ' Shared global variable

    ' Shared constructor to initialize contehtHistory
    Shared Sub New()
        _contentHistory = New List(Of String)()
    End Sub

    Private Sub buttonAsk_Click(sender As Object, e As RoutedEventArgs)
        Mouse.OverrideCursor = Cursors.Wait
        fillAnswer()
        Mouse.OverrideCursor = Nothing
    End Sub
    Private Sub buttonNext_Click(sender As Object, e As RoutedEventArgs)
        textQuestion.Text = ""
    End Sub
    Private Sub buttonCopyAnswer_Click(sender As Object, e As RoutedEventArgs)
        If Not String.IsNullOrEmpty(textAnswer.Text) Then
            Clipboard.SetText(textAnswer.Text)
            MsgBox ("Answer copied in clipboard, use CTL-V to get it")
        End If
    End Sub
    Private Sub buttonCopyChat_Click(sender As Object, e As RoutedEventArgs)
        If Not String.IsNullOrEmpty(textHistory.Text) Then
            Clipboard.SetText(textHistory.Text)
            MsgBox ("complete chhat copied in clipboard, use CTL-V to get it")
        End If
    End Sub
    Private Sub buttonClearChat_Click(sender As Object, e As RoutedEventArgs)
        textQuestion.Text = ""
        If Not String.IsNullOrEmpty(textHistory.Text) Then
            textHistory.Text = ""
            textAnswer.Text = ""
        End If
    End Sub
    Private Sub buttonHelp_Click(sender As Object, e As RoutedEventArgs) Handles buttonHelp.Click
        Dim pdfPath As String = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ChatGPT_HELP.pdf")
        Try
            Process.Start(New ProcessStartInfo(pdfPath) With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show("Unable to open help document.")
        End Try
    End Sub

    Private Sub fillAnswer()
        Try
            textAnswer.Text = ""
            Dim aiClient As New OpenAIClient()
            Dim apiKey As String = aiClient.ReadApiKey()

            Dim inputText As String = textQuestion.Text
            Dim myModel As String = ""

            ' Example model assignment and interface updates
            myModel = "gpt-4o"
        
            ' Use the shared contentHistory here
            ' Dim getOpenAIResponseGpt4Task As Task(Of String) = aiClient.GetOpenAIResponseGpt4(myModel, inputText, contehtHistory, client, apiKey)
            ' getOpenAIResponseGpt4Task.Wait()
            Dim myAnswer As String = aiClient.GetOpenAIResponseGpt4(myModel, inputText, _contentHistory, apiKey)
            ' Add myAnswer to contehtHistory
            _contentHistory.Add(myAnswer)
            textAnswer.Text = myAnswer
            If (Len(textHistory.Text) = 0) Then
                textHistory.Text = TextHistory.Text &
                               inputText & Environment.NewLine &  myAnswer
            Else 
                textHistory.Text = TextHistory.Text & Environment.NewLine & Environment.NewLine &
                                   inputText & Environment.NewLine &  myAnswer
            End If

        Catch ex As IOException
            ' Handle exceptions
            Console.WriteLine("Error: " & ex.Message)
        End Try
    End Sub

End Class