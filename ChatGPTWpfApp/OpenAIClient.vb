Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports System.Web
Imports OpenAI
Imports OpenAI.Chat


Public Class OpenAIClient
    Public Shared Function EscapeHtml(input As String) As String
        ' Escape standard HTML characters
        Dim escaped As String = HttpUtility.HtmlEncode(input)

        ' Optionally replace special characters if needed
        escaped = escaped.Replace("Ü", "Ü").Replace("ü", "ü")

        Return escaped
    End Function

    Public Function ReadApiKey() As String
        ' Get the current working directory
        Dim currentDir As String = Directory.GetCurrentDirectory()
        ' Get the directory one level above the current working directory
        Dim parentDir As String = Directory.GetParent(currentDir).FullName
        ' Read the API key file
        Dim keyBytes As Byte() = File.ReadAllBytes(Path.Combine(parentDir, "api_key"))
        ' Convert to string and remove the end of line characters
        Return Encoding.UTF8.GetString(keyBytes).Trim()
    End Function

    Public Function InitOpenAIClient() As HttpClient
        Return New HttpClient()
    End Function

    Public Function GetOpenAIResponseGpt4(model As String, inputText As String, contentHistory As List(Of String), apiKey As String) As String
        Dim openAiclient As New ChatClient(model:="gpt-4o", apiKey:=apiKey)
        ' Combine inputText and contentHistory into a single input
        Dim combinedInput As String = String.Join(Environment.NewLine, contentHistory) & Environment.NewLine & inputText
        Dim completion As ChatCompletion = openAiclient.CompleteChat(combinedInput)
        return completion.Content(0).Text  
    End Function
End Class