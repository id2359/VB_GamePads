﻿Imports Microsoft.VisualBasic.GamePads
Imports Microsoft.VisualBasic.GamePads.Abstract

Public Class Cloud : Inherits GraphicUnit

    Dim seed As Integer = 20 * RandomDouble()

    Public Overrides Sub Draw(ByRef g As GDIPlusDeviceHandle)
        Call g.Gr_Device.DrawImageUnscaled(My.Resources.cloud, Location)
        Location = New Point(Location.X - seed, Location.Y)
    End Sub

    Protected Overrides Function __getSize() As Size
        Return My.Resources.cloud.Size
    End Function
End Class
