﻿Imports Microsoft.VisualBasic.GamePads.Abstract
Imports Microsoft.VisualBasic.Imaging

Namespace EngineParts

    Public Class GraphicDevice : Inherits Abstract.EngineParts

        Protected Friend ReadOnly _list As New List(Of GraphicUnit)

        ''' <summary>
        ''' Graphics refresh rate, Hz.(图像的刷新频率，单位为Hz)
        ''' </summary>
        ''' <returns></returns>
        Public Property RefreshHz As Integer
            Get
                Return 1000 / _sleep
            End Get
            Set(value As Integer)
                _sleep = 1000 / value
            End Set
        End Property

        Friend _sleep As Integer

        ''' <summary>
        ''' The size of the graphics region
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property DeviceSize As Size
            Get
                Return Engine.display.Size
            End Get
        End Property

        Sub New(engine As GameEngine)
            Call MyBase.New(engine)
        End Sub

        Public Sub Updates(g As Graphics, size As Size)
            Dim source As GraphicUnit()

            SyncLock _list
                source = _list.ToArray
            End SyncLock

            For Each x As GraphicUnit In source
                Call x.Draw(g, size)
            Next

            If Not Engine.display.BackgroundImage Is Nothing Then
                Engine.display.BackgroundImage.Dispose()
            End If
        End Sub
    End Class
End Namespace