module Button

open Feliz

type Button = {
  label: string
  onClick: Browser.Types.MouseEvent -> unit
}

let button = React.functionComponent("Button", fun (p: Button) -> 
  Html.button [
    prop.onClick p.onClick
    prop.text p.label
  ]
)