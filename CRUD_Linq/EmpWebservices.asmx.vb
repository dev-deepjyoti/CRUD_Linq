
Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Collections.Generic
Imports System.Web.Services.Protocols

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<ScriptService()>
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class EmpWebservices
    Inherits WebService

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod(ResponseFormat:=System.Web.Script.Services.ResponseFormat.Json)>
    Public Function GetEmployee(EmpID As String) As Object
        Try
            Using db As New MyCrudtestEntities()
                Dim employee = (From e In db.employees Where e.Emp_ID = EmpID Select New With
                        {.EmpID = e.Emp_ID,
                         .EmpName = e.Emp_Name,
                         .EmpAge = e.Age,
                          .EmpPhone = e.PhoneNo,
                          .EmpSex = e.Sex}).FirstOrDefault()
                Return employee
            End Using
        Catch ex As Exception
            Dim xMsg = ex.Message
            Throw New Exception("Error retrieving employees: " & xMsg, ex)
        End Try
    End Function

    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod(ResponseFormat:=System.Web.Script.Services.ResponseFormat.Json)>
    Public Function GetAllEmployees() As List(Of Object)
        Try
            Using db As New MyCrudtestEntities()

                Dim allEmpData = (From e In db.employees Order By e.Emp_Name Select New With
        {.EmpID = e.Emp_ID, .EmpName = e.Emp_Name, .EmpAge = e.Age, .EmpPhone = e.PhoneNo, .EmpSex = e.Sex}).ToList()

                Return allEmpData.Cast(Of Object).ToList()
            End Using
        Catch ex As Exception
            Dim xMsg = ex.Message
            Throw New Exception("Error retrieving employees: " & xMsg, ex)
        End Try
    End Function

    '================= Save & update not implemented =======================

    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod(ResponseFormat:=System.Web.Script.Services.ResponseFormat.Json)>
    Public Function SaveUpdateEmployee(EmpID As String,
                                       EmpName As String,
                                       Age As String,
                                       Phone As String,
                                       Sex As String) As Boolean
        Dim Retval = False
        'INSERT

        If String.IsNullOrWhiteSpace(EmpID) Then
            Try
                Using db As New MyCrudtestEntities()
                    EmpID = Guid.NewGuid().ToString()

                    Dim newEmp As New employee() With {
                        .Emp_ID = EmpID,
                        .Emp_Name = EmpName,
                        .Age = Age,
                        .PhoneNo = Phone,
                        .Sex = Sex
                    }
                    db.employees.Add(newEmp)
                    db.SaveChanges()
                    Retval = True
                End Using
            Catch ex As Exception
                Dim xMsg = ex.Message
                Throw New Exception("Error retrieving employees: " & xMsg, ex)
            End Try
        Else
            Try
                'UPDATE
                Using db As New MyCrudtestEntities()

                    Dim updateEmp = db.employees.FirstOrDefault(Function(e) e.Emp_ID = EmpID)

                    If updateEmp IsNot Nothing Then
                        updateEmp.Emp_Name = EmpName
                        updateEmp.Age = Age
                        updateEmp.PhoneNo = Phone
                        updateEmp.Sex = Sex
                        db.SaveChanges()
                        Retval = True
                    End If
                End Using
            Catch ex As Exception
                Dim xMsg = ex.Message
                Throw New Exception("Error updating employees: " & xMsg, ex)
            End Try

        End If
        Return Retval
    End Function

    '=========================================

    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod(ResponseFormat:=System.Web.Script.Services.ResponseFormat.Json)>
    Public Function SaveEmployee(EmpID As String,
                                       EmpName As String,
                                       Age As String,
                                       Phone As String,
                                       Sex As String) As Boolean
        Dim Retval = False

        If String.IsNullOrWhiteSpace(EmpID) Then
            Try
                Using db As New MyCrudtestEntities()
                    EmpID = Guid.NewGuid().ToString()

                    Dim newEmp As New employee() With {
                        .Emp_ID = EmpID,
                        .Emp_Name = EmpName,
                        .Age = Age,
                        .PhoneNo = Phone,
                        .Sex = Sex
                    }
                    db.employees.Add(newEmp)
                    db.SaveChanges()
                    Retval = True
                End Using
            Catch ex As Exception
                Dim xMsg = ex.Message
                Throw New Exception("Error retrieving employees: " & xMsg, ex)
            End Try
        End If
        Return Retval
    End Function

    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod(ResponseFormat:=System.Web.Script.Services.ResponseFormat.Json)>
    Public Function UpdateEmployee(EmpID As String,
                                      EmpName As String,
                                      Age As String,
                                      Phone As String,
                                      Sex As String) As Boolean
        Dim Retval = False

        If Not String.IsNullOrWhiteSpace(EmpID) Then
            Try
                Using db As New MyCrudtestEntities()

                    Dim updateEmp = db.employees.FirstOrDefault(Function(e) e.Emp_ID = EmpID)

                    If updateEmp IsNot Nothing Then
                        updateEmp.Emp_Name = EmpName
                        updateEmp.Age = Age
                        updateEmp.PhoneNo = Phone
                        updateEmp.Sex = Sex
                        db.SaveChanges()
                        Retval = True
                    End If
                End Using
            Catch ex As Exception
                Dim xMsg = ex.Message
                Throw New Exception("Error updating employees: " & xMsg, ex)
            End Try
        End If
        Return Retval
    End Function

    <WebMethod()>
    <System.Web.Script.Services.ScriptMethod(ResponseFormat:=System.Web.Script.Services.ResponseFormat.Json)>
    Public Function DeleteEmployee(EmpID As String) As Boolean
        Dim Retval = False

        If Not String.IsNullOrWhiteSpace(EmpID) Then
            Try
                Using db As New MyCrudtestEntities()

                    Dim deleteEmp = db.employees.FirstOrDefault(Function(e) e.Emp_ID = EmpID)

                    If deleteEmp IsNot Nothing Then
                        db.employees.Remove(deleteEmp)
                        db.SaveChanges()
                        Retval = True
                    End If
                End Using
            Catch ex As Exception
                Dim xMsg = ex.Message
                Throw New Exception("Error updating employees: " & xMsg, ex)
            End Try
        End If
        Return Retval
    End Function



End Class