module App

open Feliz
open Elmish

open Button
open ReducerTimer

type State = { Count: int }

type Msg =
    | Increment
    | Decrement

let init() = { Count = 0 }, Cmd.none

let update (msg: Msg) (state: State) =
    match msg with
    | Increment -> { state with Count = state.Count + 1 }, Cmd.none
    | Decrement -> { state with Count = state.Count - 1 }, Cmd.none

let render (state: State) (dispatch: Msg -> unit) =
    Html.div [
        button { label = "Increment"; onClick = (fun _ -> dispatch Increment) }
        button { label = "Decrement"; onClick = (fun _ -> dispatch Decrement) }

        Html.h1 state.Count

        Html.hr []

        timer ()

        Html.hr []

        ElmishInput.elmishInput
    ]