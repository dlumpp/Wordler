namespace Wordler.Core

open System

module Say =
    let hello name =
         $"Hello {name}"

module Mixer =

    type Clue = {
        Letter: char
        At: int
        NotAt: int[]
    }

    let mix (pool:seq<Clue>) size :seq<seq<Nullable<char>>> =
        //seq { Seq.map (fun i -> Nullable()) [1..size] }
        let wordIndexes = [0..size-1]

        let fits (spot:int, clue:Clue) =
            match clue with
            | { At = spot } -> true
            //| { NotAt = spot } -> false
            | _ -> false
        
        // distinct results from this need added to the pad by caller to capture all possibilites
        // I think this can just map pool rather than concatting things
        let place (wordIndex:int, pool:Clue list, pad:char option list):char option list =
            let clue = pool.Head
            if fits(wordIndex, pool.Head) then 
                pad @ [Some clue.Letter]
            else 
                pad @ [None]

        let noneToNull (char: char option) :Nullable<char> = if char.IsSome then char.Value |> Nullable else Nullable()
        Seq.map (fun i -> place(i, pool |> Seq.toList, []) |> Seq.map noneToNull ) wordIndexes
    //let board = [for i in [0..5] do yield "_" ]
    //let choose spot pool =
    //    List.choose fits pool 
    //    //for each spot -> try pool.head
    //    // if it fits, place it and recur w new board and pool.tail
