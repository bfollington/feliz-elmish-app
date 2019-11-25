module ReducerTimer

open Feliz
open Fable.Core.JS
open Disposer
open Button

type State = {
  value: int
}

let init: State = {
  value = 0
}

type Msg = 
  | Tick
  | Reset

let reducer (state: State) =
  function
  | Tick -> { state with value = state.value + 1 }
  | Reset -> { state with value = 0}

let timer = React.functionComponent("Timer", fun () -> 
  let (state, dispatch) = React.useReducer(reducer, init)

  let subscribeToTimer() =
        let subscriptionId = setInterval (fun _ -> dispatch Tick) 1000
        disposer (fun () -> clearTimeout(subscriptionId))

  React.useEffect(subscribeToTimer, [| box dispatch |])

  Html.div [
    prop.children [
      Html.label [
        prop.text state.value
      ]

      button { label = "Reset"; onClick = fun _ -> dispatch Reset}
    ]
  ]
)