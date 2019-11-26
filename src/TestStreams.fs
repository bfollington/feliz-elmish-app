module TestStreams

open FSharp.Control

type Msg = 
  | Ping
  | Pong

let isPing = function | Ping _ -> true | _ -> false

let printHandler = function
  | Ping _ -> printfn "Ping!"
  | Pong _ -> printfn "Pong!"

let testSubscribe () =
  let (post, obs) = Stream.create<Msg>()

  obs
  |> AsyncRx.filter isPing
  |> Stream.subscribe (function
    | Ping _ -> 
      post Pong |> Async.StartImmediate
    | Pong _ -> ()
  )

  obs
  |> Stream.subscribe (function
    | Ping _ -> printfn "Ping!"
    | Pong _ -> printfn "Pong!"
  )

  post Ping |> Async.StartImmediate