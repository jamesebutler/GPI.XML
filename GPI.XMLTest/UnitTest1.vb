Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports System.Diagnostics
Imports System.Xml
Imports System.IO
Imports GPI.XML


<TestClass()> Public Class UnitTest1



    <TestMethod()>
    Public Sub CreateXML()

        Dim returnXML As String = XMLUtility.CreateXMLTest()
        Debug.WriteLine(returnXML)

    End Sub

    <TestMethod()>
    Public Sub ReadNodeFromXML()

        Dim returnXML As String = XMLUtility.CreateXMLTest()
        Dim myNode = XMLUtility.ReadXmlNode(returnXML, "TestName")
        Debug.WriteLine(myNode)

    End Sub

    <TestMethod()>
    Public Sub ReadNodeFromConfigXML()

        Dim configFileName As String
        Dim strAppPath As String = ""
        Dim strResults As String = ""
        Dim xml As XmlDocument
        Dim NCredentials As XmlElement
        Dim NForms As XmlElement

        Dim appPath As String
        appPath = My.Application.Info.DirectoryPath
        configFileName = appPath & "\app.config"

        xml = New XmlDocument
        Try
            xml.Load(configFileName)
            NForms = xml.SelectSingleNode("appSettings")

            If Not NForms Is Nothing Then

                NCredentials = NForms.SelectSingleNode("credentials")

                If Not NCredentials Is Nothing Then

                    strResults = "nothing found"


                End If

            End If


        Catch ex As Exception

            strResults = ex.Message.ToString

        End Try






    End Sub

End Class