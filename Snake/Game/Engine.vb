﻿Imports Microsoft.VisualBasic.GamePads
Imports Microsoft.VisualBasic.GamePads.Abstract
Imports Microsoft.VisualBasic.GamePads.Commons
Imports Microsoft.VisualBasic.GamePads.EngineParts
Imports Microsoft.VisualBasic.GamePads.SoundDriver

Public Class GameEngine : Inherits Engine

    Dim _snake As Snake = New Snake

    Sub New(device As DisplayPort)
        Call MyBase.New(device)
    End Sub

    Public Overrides Sub ClickObject(pos As Point, x As GraphicUnit)
        If TypeOf x Is Button Then
            Call Score.Reset
            Call Reset()
        End If
    End Sub

    Public Overrides Sub Invoke(control As Controls, raw As Char)
        Select Case control
            Case Controls.Up, Controls.Right, Controls.Left, Controls.Down
                _snake.Direction = control
            Case Controls.Ok
                Call Restart()
            Case Controls.Pause
                Call Pause()
            Case Controls.Menu
        End Select
    End Sub

    Protected Overrides Sub __worldReacts()
        If _snake.Head.IntersectsWith(food.Region) Then
            Call Me.Remove(food)
            Call AddFood()
            _snake.Append()
            Call Beep()
        End If

        If Not GraphicRegion.Contains(_snake.Head.Location) OrElse
            _snake.EatSelf Then            ' 撞墙或者吃掉自己的身体，Game Over

            Call Pause()

            Dim g = _innerDevice.BackgroundImage.GdiFromImage
            Dim l As Point = New Point((g.Width - My.Resources.Restart.Width) / 2, (g.Height - My.Resources.Restart.Height) / 2)
            Dim button As New Button(My.Resources.Restart) With {.Location = l}

            Call Me.Add(button)
            Call button.Draw(g)

            _innerDevice.BackgroundImage = g.ImageResource
        End If
    End Sub

    Protected Overrides Sub __GraphicsDeviceResize()

    End Sub

    Dim food As Food

    Private Sub AddFood()
        food = New Food With {.Location = New Point(GraphicRegion.Width * RandomDouble(), GraphicRegion.Height * RandomDouble())}
        Call Me.Add(food)
        Score.Score += 1
    End Sub

    Public Overrides Function Init() As Boolean
        Call ControlMaps.DefaultMaps(Me.ControlsMap.ControlMaps)

        _snake.Location = Me.GraphicRegion.Center
        _snake.MoveSpeed = 105

        Dim score As New Score With {.Location = New Point(10, 10)}

        Call Me.Add(score)

        Me.Score = score

        Call _snake.init()
        Call Me.Add(_snake)
        Call AddFood()

        Call WinMM.PlaySound(App.HOME & "/title.wma")

        Return True
    End Function

    Public Overrides Sub __reset()
        _snake.Location = Me.GraphicRegion.Center
        Call _snake.init()
        Call Me.Remove((From x In Me Where TypeOf x Is Food Select x).FirstOrDefault)
        Call AddFood()

        Call Me.Remove((From x In Me Where TypeOf x Is Button Select x).FirstOrDefault)
        Call WinMM.PlaySound(App.HOME & "/title.wma")
    End Sub

    Public Overrides Sub __restart()

    End Sub
End Class