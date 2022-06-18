module ConwayGameOfLife

open System

type Cell =
    | Dead
    | Alive

type GameState =
    {
        Width: int
        Height: int
        Grid: Cell list
    }

let toXY index gameState =
    let x = index % gameState.Width
    let y = index / gameState.Width
    (x, y)

let toIndex x y gameState =
    y * gameState.Width + x

let random = Random()

let isCellAlive x y gameState =
    match gameState.Grid[toIndex x y gameState] with
    | Dead -> false
    | Alive -> true

let countLiveNeighbors x y gameState =
    gameState.Grid
    |> List.indexed
    |> List.filter (
        fun (i, _) ->
            let (x', y') = toXY i gameState
            abs(x' - x) <= 1 && abs(y' - y) <= 1
    )
    |> List.sumBy (fun (_, cell) ->
        match cell with
        | Alive -> 1
        | Dead -> 0
    )

let initialState width height =
    {
        Width = width
        Height = height
        Grid = [
            for y in 0..height-1 do
                for x in 0..width-1 do
                    if random.Next(0, 2) = 0 then
                        Dead
                    else
                        Alive
        ]
    }

let updateState gameState =
    {
        gameState with
            Grid = [
                for y in 0..gameState.Height-1 do
                    for x in 0..gameState.Width-1 do
                        let liveNeighbors = countLiveNeighbors x y gameState
                        match (isCellAlive x y gameState, liveNeighbors) with
                        | (true, 2) | (true, 3) | (false, 3) -> Alive
                        | _ -> Dead
            ]
    }