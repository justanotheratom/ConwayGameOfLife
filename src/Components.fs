namespace App

open System
open Feliz
open Fable.Core.JS

type Components =
    /// <summary>
    /// The simplest possible React component.
    /// Shows a header with the text Hello World
    /// </summary>
    [<ReactComponent>]
    static member ConwayGameOfLife() =

        let X = 50
        let Y = X

        let (gameStarted, setGameStarted) = React.useState(false)
        let (gameState, setGameState) = React.useStateWithUpdater(ConwayGameOfLife.initialState X Y)

        let subscribeToTimer() =
            let subscriptionId =
                setInterval (fun _ -> setGameState (fun prevState -> ConwayGameOfLife.updateState prevState)) 500
            React.createDisposable (fun _ -> clearTimeout subscriptionId)

        React.useEffect(subscribeToTimer, [| |])

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
                        prop.style [
                            style.display.flex
                            style.flexWrap.wrap
                        ]
                        prop.children [
                            for y in 0..Y-1 do
                                Html.div [
                                    for x in 0..X-1 do
                                        Html.div [
                                            prop.style [
                                                style.width 10
                                                style.height 10
                                                if ConwayGameOfLife.isCellAlive x y gameState then
                                                    style.backgroundColor "black"
                                                else
                                                    style.backgroundColor "white"
                                            ]
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