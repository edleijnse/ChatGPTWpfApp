Imports System.IO
Imports System.Net.Http

Class MainWindow

    Private Shared _contentHistory As List(Of String) ' Shared global variable

    ' Shared constructor to initialize contehtHistory
    Shared Sub New()
        _contentHistory = New List(Of String)()
    End Sub

    Private Sub buttonAsk_Click(sender As Object, e As RoutedEventArgs)
        fillAnswer()
    End Sub

    Private Sub fillAnswer()
        Try
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