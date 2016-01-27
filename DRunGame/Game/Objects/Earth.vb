﻿Imports Microsoft.VisualBasic.GamePads
Imports Microsoft.VisualBasic.GamePads.Abstract

Public Class Earth : Inherits GraphicUnit

    Dim __getWidth As Func(Of Integer)

    Sub New(getWidth As Func(Of Integer))
        __getWidth = getWidth
    End Sub

    Public Overrides Sub Draw(ByRef g As GDIPlusDeviceHandle)
        Call g.Gr_Device.DrawLine(Pens.Black, Location, New Point(__getWidth(), Location.Y))
    End Sub

    Protected Overrides Function __getSize() As Size
        Return New Size(__getWidth(), 10)
    End Function
End Class
