Imports System.IO
Imports System.Xml
Imports System.Collections
Imports System.Data
Imports System.Text

Public Class XMLUtility



    Public Shared Function ConvertXMLToDataSet(ByVal xmlData As String) As DataSet
        Dim stream As StringReader
        Dim reader As XmlTextReader = Nothing
        Dim dsTmpDataset As DataSet
        Try
            Dim xmlDS As New DataSet
            stream = New StringReader(xmlData)
            reader = New XmlTextReader(stream)
            xmlDS.ReadXml(reader)
            dsTmpDataset = xmlDS
        Catch ex As Exception
            dsTmpDataset = Nothing
        Finally
            If (Not reader Is Nothing) Then
                reader.Close()
            End If
        End Try
        Return dsTmpDataset
    End Function


    Public Shared Function ConvertDataSetToXML(ByVal xmlDS As DataSet) As String

        Dim stream As MemoryStream
        Dim writer As XmlTextWriter = Nothing
        Dim xmlString As String = String.Empty

        Try
            stream = New MemoryStream
            writer = New XmlTextWriter(stream, Encoding.Unicode)
            xmlDS.WriteXml(writer)
            Dim count As Integer = CInt(stream.Length)
            Dim arr As Byte() = New Byte(count - 1) {}
            stream.Seek(0, SeekOrigin.Begin)
            stream.Read(arr, 0, count)
            Dim utf As New UnicodeEncoding
            xmlString = utf.GetString(arr).Trim
        Catch ex As Exception
            xmlString = String.Empty
        Finally
            If (Not writer Is Nothing) Then
                writer.Close()
            End If
        End Try
        Return xmlString





    End Function


    Public Shared Function ReadXmlNode(ByVal xml As String, ByVal node As String) As String

        'Dim xmldoc1 As New XmlDataDocument
        Dim xmldoc As New XmlDocument

        Dim xmlnode As XmlNodeList

        Dim sr As New System.IO.StringReader(xml)

        xmldoc.Load(sr)
        xmlnode = xmldoc.GetElementsByTagName(node)

        Return xmlnode(0).ChildNodes.Item(0).InnerText.Trim()


    End Function





    'nothing below this line

    Public Shared Function CreateXMLTest() As String

        'write results to session xml
        Dim settings As New XmlWriterSettings
        With settings
            .CloseOutput = True
            .Encoding = Encoding.UTF8
            .Indent = False
            .OmitXmlDeclaration = True
        End With
        Dim sw As New System.IO.StringWriter
        Using xw As XmlWriter = XmlWriter.Create(sw, settings)
            With xw
                .WriteStartDocument()
                .WriteStartElement("TestInfo")

                .WriteStartElement("TestID")
                .WriteValue("this is my test id number")
                .WriteEndElement()

                .WriteStartElement("TestName")
                .WriteValue("this is my testname")
                .WriteEndElement()


                .WriteEndElement()
                .WriteEndDocument()
            End With
        End Using

        Return sw.ToString

    End Function

    Public Shared Function LookupConfigEntry(ByVal myvalue As String, ByVal myconfigentry As String) As Integer

        Try
            'NEED TO RESOLVE HOW TO GET TO THE CONFIG FILE
            'Could do something like below and then pass into here.
            'the myvalue would be the xmlstring
            'configFileName = strAppPath & "\web.config"
            'XML = New XmlDocument
            'XML.Load(configFileName)



            'Dim reader As XmlTextReader = New XmlTextReader(ConfigurationManager.AppSettings(myconfigentry))

            Dim reader As XmlTextReader = New XmlTextReader(myconfigentry)
                Do While (reader.Read())
                Select Case reader.NodeType
                    Case XmlNodeType.Element ' The node is an Element
                        'Write  reader.Name)
                        While (reader.MoveToNextAttribute()) ' Read attributes
                            If reader.Value = myvalue Then
                                reader.Close()
                                Return True
                            End If
                        End While

                    Case XmlNodeType.DocumentType ' The node is a DocumentType
                        'you access the information (NodeType & "<" & reader.Name & ">" & reader.Value)

                End Select
            Loop

            Return False

        Catch ex As Exception
            Return False

        End Try

    End Function


End Class
