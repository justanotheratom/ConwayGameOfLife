namespace App

open Feliz
open Feliz.Router

type Components =
    /// <summary>
    /// The simplest possible React component.
    /// Shows a header with the text Hello World
    /// </summary>
    [<ReactComponent>]
    static member ConwayGameOfLife() =
        let X = 100
        let Y = 100
        let (gameStarted, setGameStarted) = React.useState(false)
        let (gameState, setGameState) = React.useStateWithUpdater(ConwayGameOfLife.initialState X Y)
        Html.div [
            prop.style [ style.textAlign.center ]
            prop.children [
                Html.h1 "Conway's Game of Life"
                if not gameStarted then
                    Html.button [
                        prop.text "Start game"
                        prop.onClick (fun _ -> setGameStarted true)
                    ]
                else
                    Html.div [
                        prop.className [
                            ConwayGameOfLife.stylesheet.["slot-container"]
                        ]
                        prop.children [
                            for y in 0..Y-1 do
                                for x in 0..X-1 do
                                    Html.div [
                                        prop.className [
                                            if ConwayGameOfLife.isCellAlive x y gameState then
                                                ConwayGameOfLife.stylesheet.["slot-alive"]
                                            else
                                                ConwayGameOfLife.stylesheet.["slot-dead"]
                                        ]
                                    ]
                        ]
                    ]
                    Html.button [
                        prop.text "Abandon game"
                        prop.onClick (
                            fun _ ->
                                setGameStarted false
                                setGameState (fun _ -> ConwayGameOfLife.initialState X Y)
                        )
                    ]
                    Html.button [
                        prop.text "Start a fresh game"
                        prop.onClick (
                            fun _ ->
                                setGameStarted false
                                setGameState (fun _ -> ConwayGameOfLife.initialState X Y)
                                setGameStarted true
                        )
                    ]
            ]
        ]